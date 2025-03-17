using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public KeyCode moveUpKey = KeyCode.W;
    public KeyCode moveDownKey = KeyCode.S;
    public KeyCode moveLeftKey = KeyCode.A;
    public KeyCode moveRightKey = KeyCode.D;

    public bool isIdle = true;
    public bool isWalk = false;

    public enum lastDirection { Left, Right, Up, Down };

    private void Update()
    {
        CheckMovementInput();
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
        if(Input.GetKey(moveUpKey)|| Input.GetKey(moveDownKey)|| Input.GetKey(moveLeftKey)|| Input.GetKey(moveRightKey))
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
}
