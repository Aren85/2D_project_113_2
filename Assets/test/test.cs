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

    public float lineLength = 3f; // 線條長度


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
        // 預設所有方向為 false，確保每幀都正確更新
        right = false;
        up = false;
        down = false;
        left = false;

        if (clickWorldPosition.x >= 0)
        {
            right = worldAngle >= -45 && worldAngle < 45;
        }
        else // x < 0 的時候，才可能是左
        {
            left = worldAngle > 135 || worldAngle < -135;
        }

        if (clickWorldPosition.y >= 0)
        {
            up = worldAngle >= 45 && worldAngle < 135;
        }
        else // y < 0 的時候，才可能是下
        {
            down = worldAngle > -135 && worldAngle < -45;
        }
    }

    void OnDrawGizmos()
    {
        Vector2 position = transform.position; // 角色位置

        // 設定線條顏色
        Gizmos.color = Color.red;
        Gizmos.DrawLine(position, position + new Vector2(lineLength, 0)); // 右

        Gizmos.color = Color.green;
        Gizmos.DrawLine(position, position + new Vector2(lineLength, lineLength)); // 右上

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(position, position + new Vector2(0, lineLength)); // 上

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(position, position + new Vector2(-lineLength, lineLength)); // 左上

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(position, position + new Vector2(-lineLength, 0)); // 左

        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(position, position + new Vector2(-lineLength, -lineLength)); // 左下

        Gizmos.color = Color.white;
        Gizmos.DrawLine(position, position + new Vector2(0, -lineLength)); // 下

        Gizmos.color = Color.gray;
        Gizmos.DrawLine(position, position + new Vector2(lineLength, -lineLength)); // 右下
    }

    void Line()
    {
        Vector2 position = transform.position; // 角色位置

        // 右（0°）
        Debug.DrawLine(position, position + new Vector2(lineLength, 0), Color.red);
        // 右上（45°）
        Debug.DrawLine(position, position + new Vector2(lineLength, lineLength), Color.green);
        // 上（90°）
        Debug.DrawLine(position, position + new Vector2(0, lineLength), Color.blue);
        // 左上（135°）
        Debug.DrawLine(position, position + new Vector2(-lineLength, lineLength), Color.cyan);
        // 左（180° 或 -180°）
        Debug.DrawLine(position, position + new Vector2(-lineLength, 0), Color.yellow);
        // 左下（-135°）
        Debug.DrawLine(position, position + new Vector2(-lineLength, -lineLength), Color.magenta);
        // 下（-90°）
        Debug.DrawLine(position, position + new Vector2(0, -lineLength), Color.white);
        // 右下（-45°）
        Debug.DrawLine(position, position + new Vector2(lineLength, -lineLength), Color.gray);
    }
}

