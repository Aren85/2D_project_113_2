using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float playerMoveSpeed = 5f;
    private PlayerController playerController;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movement = playerController.GetMovementInput(); // ���o���ʤ�V
        Move(movement); // ���沾��
    }

    public void Move(Vector2 direction)
    {
        transform.Translate(direction * playerMoveSpeed * Time.deltaTime);
    }
}
