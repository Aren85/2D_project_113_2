using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private PlayerController playerController;
    private Animator animator;
    private Vector2 lastDirection;
    private Vector2 movement;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerController = GetComponentInParent<PlayerController>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        movement = playerController.GetMovementInput(); // 取得移動方向
        SetIdleAnimations();
        SetWalkAnimations();
    }
    void SetIdleAnimations()
    {
        animator.SetBool("isIdle", playerController.isIdle);
    }
    void SetWalkAnimations()
    {
        animator.SetBool("isWalk", playerController.isWalk);
        if (playerController.isWalk)
        {
            if (movement.x >= 1)
            {
                animator.SetFloat("PosX", 1);
                animator.SetFloat("PosY", 0);
            }
            else if (movement.x <= -1)
            {
                animator.SetFloat("PosX", -1);
                animator.SetFloat("PosY", 0);
            }
            else if (movement.y >= 1)
            {
                animator.SetFloat("PosX", 0);
                animator.SetFloat("PosY", 1);
            }
            else if (movement.y <= -1)
            {
                animator.SetFloat("PosX", 0);
                animator.SetFloat("PosY", -1);
            }
        }
    }
}
