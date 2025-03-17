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

    // ���o���ʤ�V
    public Vector2 GetMovementInput()
    {
        float moveX = 0f;
        float moveY = 0f;

        if (Input.GetKey(moveUpKey)) moveY += 1;  // ���U W �V�W����
        if (Input.GetKey(moveDownKey)) moveY -= 1; // ���U S �V�U����
        if (Input.GetKey(moveLeftKey)) moveX -= 1; // ���U A �V������
        if (Input.GetKey(moveRightKey)) moveX += 1; // ���U D �V�k����

        return new Vector2(moveX, moveY).normalized; // ���W�ơA�T�O�﨤���ʤ��|�ܧ�
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
