using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region ����]�w
    public KeyCode moveUpKey = KeyCode.W;
    public KeyCode moveDownKey = KeyCode.S;
    public KeyCode moveLeftKey = KeyCode.A;
    public KeyCode moveRightKey = KeyCode.D;
    public KeyCode normalAttack = KeyCode.Mouse0;
    public KeyCode strongAttack = KeyCode.Mouse1;
    public KeyCode dash = KeyCode.LeftShift;
    #endregion

    #region �ƹ���m
    public Vector3 mousePostion;
    public Vector3 mouseWorldPostion;

    public Vector2 clickPosition;
    public Vector2 clickWorldPosition;

    public float angle;
    public float worldAngle;
    #endregion

    #region ���a���A
    public bool isIdle = true;
    public bool isWalk = false;
    #endregion

    #region ���V��V
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
            Debug.Log("�@�����");
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
            //Debug.Log("�j�O����");
        }
    }

    public void Dash()
    {
        if (Input.GetKey(dash))
        {
            //Debug.Log("�Ĩ�");
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
        // �w�]�Ҧ���V�� false�A�T�O�C�V�����T��s
        attackRight = false;
        attackUp = false;
        attackDown = false;
        attackLeft = false;

        if (clickWorldPosition.x >= 0)
        {
            attackRight = worldAngle >= -45 && worldAngle < 45;
        }
        else // x < 0 ���ɭԡA�~�i��O��
        {
            attackLeft = worldAngle > 135 || worldAngle < -135;
        }

        if (clickWorldPosition.y >= 0)
        {
            attackUp = worldAngle >= 45 && worldAngle < 135;
        }
        else // y < 0 ���ɭԡA�~�i��O�U
        {
            attackDown = worldAngle > -135 && worldAngle < -45;
        }
    }
}
