using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public InputReader inputReader;

    public float playerMoveSpeed = 5f;
    private PlayerController playerController;

    Vector2 moveInput;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void Start()
    {
        inputReader.Move += direction => moveInput = direction;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(moveInput);
        //Vector2 movement = playerController.GetMovementInput(); // 取得移動方向
        //Move(movement); // 執行移動
    }

    public void Move(Vector2 direction)
    {
        transform.Translate(direction * playerMoveSpeed * Time.deltaTime);
    }
}
