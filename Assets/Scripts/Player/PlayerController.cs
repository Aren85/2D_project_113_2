using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region 按鍵設定
    public KeyCode moveUpKey = KeyCode.W;
    public KeyCode moveDownKey = KeyCode.S;
    public KeyCode moveLeftKey = KeyCode.A;
    public KeyCode moveRightKey = KeyCode.D;
    public KeyCode normalAttack = KeyCode.Mouse0;
    public KeyCode strongAttack = KeyCode.Mouse1;
    public KeyCode dash = KeyCode.LeftShift;
    #endregion

    #region 滑鼠位置
    public Vector3 mousePostion;
    public Vector3 mouseWorldPostion;

    public Vector2 clickPosition;
    public Vector2 clickWorldPosition;

    public float angle;
    public float worldAngle;
    #endregion

    #region 玩家狀態
    public bool isIdle = true;
    public bool isWalk = false;
    #endregion

    #region 面向方向
    public enum moveDirection { Left, Right, Up, Down };
    public bool attackUp;
    public bool attackDown;
    public bool attackLeft;
    public bool attackRight;
    #endregion
    private void Update()
    {
        CheckMovementInput();
        GetMouseWorldPosition();
        GetClickWorldPosition(GetMouseWorldPosition());
        SetDirection();
        GetWorldAngle();
        //NormalAttack();
        //StrongAttack();
        Dash();
        AttackFaceDirection();
    }

    // 取得移動方向
    public Vector2 GetMovementInput()
    {
        float moveX = 0f;
        float moveY = 0f;

        if (Input.GetKey(moveUpKey)) moveY += 1;  // 按下 W 向上移動
        if (Input.GetKey(moveDownKey)) moveY -= 1; // 按下 S 向下移動
        if (Input.GetKey(moveLeftKey)) moveX -= 1; // 按下 A 向左移動
        if (Input.GetKey(moveRightKey)) moveX += 1; // 按下 D 向右移動

        return new Vector2(moveX, moveY).normalized; // 正規化，確保對角移動不會變快
    }

    public void CheckMovementInput()
    {
        if (Input.GetKey(moveUpKey) || Input.GetKey(moveDownKey) || Input.GetKey(moveLeftKey) || Input.GetKey(moveRightKey))
        {
            isWalk = true;
            isIdle = false;
        }
        else
        {
            isWalk = false;
            isIdle = true;
        }
    }

    public void NormalAttack()
    {
        if (Input.GetKey(normalAttack))
        {
            Debug.Log("一般攻擊");
        }
    }

    public void AttackFaceDirection()
    {
        Vector2 mousePosition;

        if(Input.GetKey(normalAttack))
        {
            mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            //Debug.Log(mousePosition);
        }
    }

    public void StrongAttack()
    {
        if (Input.GetKey(strongAttack))
        {
            //Debug.Log("強力攻擊");
        }
    }

    public void Dash()
    {
        if (Input.GetKey(dash))
        {
            //Debug.Log("衝刺");
        }
    }

    Vector2 GetMousePosition()
    {
        mousePostion = Input.mousePosition;
        //Debug.Log("mousePostion " + mousePostion);
        return mousePostion;

    }

    Vector2 GetMouseWorldPosition()
    {
        mousePostion = Input.mousePosition;

        mouseWorldPostion = Camera.main.ScreenToWorldPoint(mousePostion);
        //Debug.Log("mousePostion " + mousePostion);
        return mouseWorldPostion;

    }

    void GetClickPosition(Vector2 targetPosition)
    {
        clickPosition = (targetPosition - (Vector2)transform.position).normalized;
        //Debug.Log("clickPosition " + clickPosition);
    }

    void GetClickWorldPosition(Vector2 targetPosition)
    {
        clickWorldPosition = (targetPosition - (Vector2)transform.position).normalized;
        //Debug.Log("clickPosition " + clickPosition);
    }

    void GetAngle()
    {
        angle = Mathf.Atan2(clickPosition.x, clickPosition.y) * Mathf.Rad2Deg;
    }

    void GetWorldAngle()
    {
        worldAngle = Mathf.Atan2(clickWorldPosition.y, clickWorldPosition.x) * Mathf.Rad2Deg;
    }

    void SetDirection()
    {
        // 預設所有方向為 false，確保每幀都正確更新
        attackRight = false;
        attackUp = false;
        attackDown = false;
        attackLeft = false;

        if (clickWorldPosition.x >= 0)
        {
            attackRight = worldAngle >= -45 && worldAngle < 45;
        }
        else // x < 0 的時候，才可能是左
        {
            attackLeft = worldAngle > 135 || worldAngle < -135;
        }

        if (clickWorldPosition.y >= 0)
        {
            attackUp = worldAngle >= 45 && worldAngle < 135;
        }
        else // y < 0 的時候，才可能是下
        {
            attackDown = worldAngle > -135 && worldAngle < -45;
        }
    }
}
