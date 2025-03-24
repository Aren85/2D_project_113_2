using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public KeyCode moveUpKey = KeyCode.W;
    public KeyCode moveDownKey = KeyCode.S;
    public KeyCode moveLeftKey = KeyCode.A;
    public KeyCode moveRightKey = KeyCode.D;
    public KeyCode normalAttack = KeyCode.Mouse0;
    public KeyCode strongAttack = KeyCode.Mouse1;
    public KeyCode dash = KeyCode.LeftShift;

    public bool isIdle = true;
    public bool isWalk = false;

    public enum lastDirection { Left, Right, Up, Down };

    private void Update()
    {
        CheckMovementInput();
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
            Debug.Log(mousePosition);
        }
    }

    public void StrongAttack()
    {
        if (Input.GetKey(strongAttack))
        {
            Debug.Log("強力攻擊");
        }
    }

    public void Dash()
    {
        if (Input.GetKey(dash))
        {
            Debug.Log("衝刺");
        }
    }
}
