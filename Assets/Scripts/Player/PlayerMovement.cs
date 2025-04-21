using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public InputReader inputReader;

    public float playerMoveSpeed = 5f;
    private PlayerController playerController;

    private void OnEnable()
    {
        inputReader.MoveEvent += EventMove;
    }

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Vector2 movement = playerController.GetMovementInput(); // 取得移動方向
        //Move(movement); // 執行移動
    }

    public void EventMove(Vector2 direction)
    {
        transform.Translate(new Vector2(direction.x, direction.y) * Time.deltaTime);
    }

    public void Move(Vector2 direction)
    {
        transform.Translate(direction * playerMoveSpeed * Time.deltaTime);
    }
}
