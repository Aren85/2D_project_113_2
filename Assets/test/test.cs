using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class test : MonoBehaviour
{
    public Vector3 mousePostion;
    public Vector3 mouseWorldPostion;

    public Vector2 clickPosition;
    public Vector2 clickWorldPosition;

    public float angle;
    public float worldAngle;

    public bool up;
    public bool down;
    public bool left;
    public bool right;

    public float lineLength = 3f; // �u������


    void Update()
    {
        Line();
        SetDirection();

        GetMousePosition();
        GetMouseWorldPosition();

        GetClickPosition(GetMousePosition());
        GetClickWorldPosition(GetMouseWorldPosition());

        GetAngle();
        GetWorldAngle();
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
        right = false;
        up = false;
        down = false;
        left = false;

        if (clickWorldPosition.x >= 0)
        {
            right = worldAngle >= -45 && worldAngle < 45;
        }
        else // x < 0 ���ɭԡA�~�i��O��
        {
            left = worldAngle > 135 || worldAngle < -135;
        }

        if (clickWorldPosition.y >= 0)
        {
            up = worldAngle >= 45 && worldAngle < 135;
        }
        else // y < 0 ���ɭԡA�~�i��O�U
        {
            down = worldAngle > -135 && worldAngle < -45;
        }
    }

    void OnDrawGizmos()
    {
        Vector2 position = transform.position; // �����m

        // �]�w�u���C��
        Gizmos.color = Color.red;
        Gizmos.DrawLine(position, position + new Vector2(lineLength, 0)); // �k

        Gizmos.color = Color.green;
        Gizmos.DrawLine(position, position + new Vector2(lineLength, lineLength)); // �k�W

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(position, position + new Vector2(0, lineLength)); // �W

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(position, position + new Vector2(-lineLength, lineLength)); // ���W

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(position, position + new Vector2(-lineLength, 0)); // ��

        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(position, position + new Vector2(-lineLength, -lineLength)); // ���U

        Gizmos.color = Color.white;
        Gizmos.DrawLine(position, position + new Vector2(0, -lineLength)); // �U

        Gizmos.color = Color.gray;
        Gizmos.DrawLine(position, position + new Vector2(lineLength, -lineLength)); // �k�U
    }

    void Line()
    {
        Vector2 position = transform.position; // �����m

        // �k�]0�X�^
        Debug.DrawLine(position, position + new Vector2(lineLength, 0), Color.red);
        // �k�W�]45�X�^
        Debug.DrawLine(position, position + new Vector2(lineLength, lineLength), Color.green);
        // �W�]90�X�^
        Debug.DrawLine(position, position + new Vector2(0, lineLength), Color.blue);
        // ���W�]135�X�^
        Debug.DrawLine(position, position + new Vector2(-lineLength, lineLength), Color.cyan);
        // ���]180�X �� -180�X�^
        Debug.DrawLine(position, position + new Vector2(-lineLength, 0), Color.yellow);
        // ���U�]-135�X�^
        Debug.DrawLine(position, position + new Vector2(-lineLength, -lineLength), Color.magenta);
        // �U�]-90�X�^
        Debug.DrawLine(position, position + new Vector2(0, -lineLength), Color.white);
        // �k�U�]-45�X�^
        Debug.DrawLine(position, position + new Vector2(lineLength, -lineLength), Color.gray);
    }
}

