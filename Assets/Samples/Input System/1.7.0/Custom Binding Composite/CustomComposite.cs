using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

#if UNITY_EDITOR
using UnityEditor;
using UnityEngine.InputSystem.Editor;
using UnityEngine.UIElements;
#endif

// 假設我們想要建立一個組合輸入（composite），它會接收一個軸向輸入（axis），並使用它的值
// 來乘以搖桿輸出的向量長度。這樣的用途，例如可以讓遊戲手把上的右觸發鍵（Right Trigger）
// 作為左搖桿輸出值的強度乘數（strength multiplier）。
//
// 我們從建立一個繼承自 InputBindingComposite<> 的類別開始。
// 泛型中指定的類型，就是我們最終要計算並回傳的值類型。
// 在這個例子中，我們會使用搖桿提供的 Vector2 資料，所以回傳類型就是 Vector2。
//
// 注意：透過宣告我們所回傳的類型，我們同時也讓輸入系統可以依照輸入行為（action）的
//       類型來篩選是否適用這個 composite。舉例來說，如果某個行為被設定為 “Value” 類型，
//       而它的控制類型（Control Type）是 “Axis”，那麼我們的 composite 就不會顯示出來，
//       因為我們回傳的是 Vector2，和 Axis 的 float 類型不相容。
//
// 此外，我們也需要向輸入系統註冊我們的 composite。
// 並且要以一種可以讓這個 composite 出現在輸入系統的動作編輯器中的方式來註冊。
//
// 要達成這件事，我們需要在啟動過程中某個時間點呼叫 InputSystem.RegisterBindingComposite。
// 我們可以透過在編輯器中使用 [InitializeOnLoad]，以及在遊戲運行時使用
// [RuntimeInitializeOnLoadMethod] 屬性來實現這個行為。
#if UNITY_EDITOR
[InitializeOnLoad]
#endif
// 我們可以透過加上 DisplayStringFormatAttribute 屬性，來自訂這個 composite 顯示字串的格式。
// 這個格式字串基本上是一串文字，其中可以包含要被替換的元素，這些元素會用大括號包起來。
// 大括號外的文字會照原樣顯示，而大括號裡的部分會根據對應的 composite 部位名稱（part name）
// 來做替換。每一個這樣的元素，會被對應部位的顯示文字所取代。
[DisplayStringFormat("{multiplier}*{stick}")]
public class CustomComposite : InputBindingComposite<Vector2>
{
    // 在編輯器中，因為有使用 [InitializeOnLoad] 屬性，這個靜態類別的建構子（static constructor）
    // 會在 Unity 啟動時自動被呼叫。
#if UNITY_EDITOR
    static CustomComposite()
    {
        // 在編輯器中觸發我們的 RegisterBindingComposite 程式碼。
        Initialize();
    }

#endif

    // 在遊戲執行時，[RuntimeInitializeOnLoadMethod] 屬性會確保我們的初始化程式碼
    // 在啟動時被呼叫。
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize()
    {
        // 這行程式碼會將自訂的 composite 註冊進輸入系統。
        // 呼叫這個方法之後，我們就可以在輸入綁定中引用這個 composite，
        // 並且它也會出現在輸入動作編輯器（Action Editor）中。
        //
        // 注意：這裡我們並沒有手動提供 composite 的名稱。
        // 預設的邏輯會使用類別的名稱（在這個例子中是 "CustomComposite"），
        // 並且如果類名的結尾是 "Composite"，就會自動把這個後綴移除，
        // 最後使用剩下的名稱作為 composite 的註冊名稱。
        // 所以在這個例子中，我們註冊的 composite 名稱會是 "Custom"。
        //
        // 假設我們要使用 AddCompositeBinding API 來加入這個 composite，
        // 範例如下：
        //
        // myAction.AddCompositeBinding("Custom")
        //     .With("Stick", "<Gamepad>/leftStick")
        //     .With("Multiplier", "<Gamepad>/rightTrigger");
        InputSystem.RegisterBindingComposite<CustomComposite>();
    }

    // 所以，對於這個 composite，我們需要兩個「部分（part）」：
    // 一個提供搖桿（stick）的值，另一個提供軸向（axis）的乘數值（multiplier）。
    // 請注意，每個部分都可能綁定多個控制器（controls）。
    // 輸入系統會幫我們處理這一點，會給我們一個整數識別碼（ID）
    // 來代表該部分所綁定的值（無論是幾個 control）
    //
    // 以我們的例子來說，這可以讓我們將「multiplier」部分
    // 同時綁定到 gamepad 上的左觸發和右觸發。

    // 為了讓輸入系統知道我們 composite 需要的「部分」是什麼，
    // 我們要新增一個 `public int` 欄位，並用 [InputControl] 屬性標註它。
    // 並且要透過 `layout` 屬性告訴系統我們預期這個部分要綁定哪種類型的控制器。

    // 注意：這些部分（part）的欄位**必須是 public 欄位**，
    // 否則輸入系統無法找到它們。

    // 下面的範例是引入一個名為 "multiplier" 的部分，
    // 預期綁定的控制器類型是 "Axis"。
    // 輸入系統會幫我們設定這個欄位的值，
    // 這個值會是 composite 內部唯一的數字 ID，
    // 我們可以用這個 ID 搭配 InputBindingCompositeContext.ReadValue 方法，
    // 來讀取該部分綁定的實際數值。
    [InputControl(layout = "Axis")]
    public int multiplier;

    // 我們還需要另一個部分來處理搖桿（stick）。
    //
    // 注意：我們可以在這裡使用 "Stick" 這個 layout，
    // 但使用 "Vector2" 會比較沒那麼侷限。
    [InputControl(layout = "Vector2")]
    public int stick;

    // 我們也可以在 composite 上公開「參數（parameters）」。
    // 這些參數可以在「動作編輯器（action editor）」中圖形化地設定，
    // 也可以透過 AddCompositeBinding 程式碼方式來指定。
    //
    // 比方說，我們希望讓使用者可以自訂一個額外的縮放比例（scale factor），
    // 用來套用在 multiplier 的數值上。
    // 這時，我們只要新增一個 `float` 型別的 public 欄位即可。
    // 任何沒有標註 [InputControl] 的 public 欄位，
    // 都會被視為是可設定的參數（parameter）。
    //
    // 假設我們用 AddCompositeBinding 建立 composite 綁定，
    // 我們可以這樣設定參數：
    //
    //     myAction.AddCompositeBinding("Custom(scaleFactor=0.5)")
    //         .With("Multiplier", "<Gamepad>/rightTrigger")
    //         .With("Stick", "<Gamepad>/leftStick");
    public float scaleFactor = 1;

    // 好的，現在我們已經完成了所有的設定部分。
    // 最後一塊拼圖，就是撰寫實際的邏輯，
    // 這段邏輯會從 "multiplier" 和 "stick" 讀取輸入值，
    // 並計算出最終的輸入結果。
    //
    // 我們可以透過定義一個 ReadValue 方法來完成這個目的，
    // 這個方法就是我們的 composite 實際執行邏輯的核心所在。
    public override Vector2 ReadValue(ref InputBindingCompositeContext context)
    {
        // 我們透過簡單地提供 input system 設定好的部件 ID，
        // 來讀取我們需要的輸入值。
        //
        // 注意：Vector2 比起像 int 或 float 這類基本數值型態稍微不直觀。
        //       如果有多個控制項綁定到 "stick" 這個部件，我們需要告訴
        //       input system 該選擇哪一個。
        //       我們可以透過提供一個 IComparer 來解決這個問題。
        //       在這個例子中，我們選擇使用 Vector2MagnitudeComparer，
        //       來選擇擁有最大長度（即最遠距離）的 Vector2。
        var stickValue = context.ReadValue<Vector2, Vector2MagnitudeComparer>(stick);
        var multiplierValue = context.ReadValue<float>(multiplier);

        // 剩下的部分很簡單。 
        // 我們只需要將讀取到的向量乘上軸的倍率，並應用我們的縮放因子。
        return stickValue * (multiplierValue * scaleFactor);
    }
}

// 我們的自訂 composite 已經完成並且完全運作。
// 我們可以停在這裡，完成整個過程。然而，為了示範，
// 假設我們也想自訂如何編輯 composite 的參數。
// 我們有 "scaleFactor"，所以假設我們想將預設的 float 編輯器
// 替換成一個滑動條（slider）。
//
// 我們可以通過繼承一個自訂的 InputParameterEditor 來替換預設的 UI，
#if UNITY_EDITOR
public class CustomCompositeEditor : InputParameterEditor<CustomComposite>
{
    public override void OnGUI()
    {
        // Using the 'target' property, we can access an instance of our composite.
        // 使用 'target' 屬性，我們可以訪問我們的 composite 實例。
        var currentValue = target.scaleFactor;

        // 最簡單的 UI 布局方式是使用 EditorGUILayout。
        // 我們只需將更改後的值重新賦回 'target' 物件。
        // 輸入系統會自動檢測到值的變更。
        target.scaleFactor = EditorGUILayout.Slider(m_ScaleFactorLabel, currentValue, 0, 2);
    }

#if UNITY_INPUT_SYSTEM_UI_TK_ASSET_EDITOR
    public override void OnDrawVisualElements(VisualElement root, Action onChangedCallback)
    {
        var slider = new Slider(m_ScaleFactorLabel.text, 0, 2)
        {
            value = target.scaleFactor,
            showInputField = true
        };

// 注意：截至 2022 年 2 月，對於 UIToolkit 的滑動條，我們無法直接在滑動條上註冊鼠標釋放事件，
// 因為滑動條內部的某個元素會捕獲該事件。解決方法是將事件註冊到滑動條的容器上。
// 這個問題將會在未來的 UIToolkit 版本中修復。
        slider.Q("unity-drag-container").RegisterCallback<MouseUpEvent>(evt =>
        {
            target.scaleFactor = slider.value;
            onChangedCallback?.Invoke();
        });

        root.Add(slider);
    }

#endif

    private GUIContent m_ScaleFactorLabel = new GUIContent("Scale Factor");
}
#endif
