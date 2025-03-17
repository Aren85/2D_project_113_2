using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private PlayerController playerController;
    private Animator animator;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerController = GetComponentInParent<PlayerController>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //SetFlipX();

        SetIdleAnimations();
    }
    void SetFlipX()
    {
        if(Input.GetKey(playerController.moveLeftKey))
        {
            spriteRenderer.flipX = true;
        }
        if (Input.GetKey(playerController.moveRightKey))
        {
            spriteRenderer.flipX = false;
        }
    }

    void SetPos()
    {
        Vector2 movement = playerController.GetMovementInput(); // 取得移動方向
        animator.SetFloat("PosX", movement.x);
        animator.SetFloat("PosY", movement.y);
    }

    void SetIdleAnimations()
    {
        Vector2 movement = playerController.GetMovementInput(); // 取得移動方向

        if(movement.x >= 1)
        {
            animator.SetFloat("PosX", 1);
            animator.SetFloat("PosY", 0);
        }else if(movement.x <= -1)
        {
            animator.SetFloat("PosX", -1);
            animator.SetFloat("PosY", 0);
        }else if(movement.y >= 1)
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
