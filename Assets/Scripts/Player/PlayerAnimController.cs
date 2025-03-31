using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    private PlayerController playerController;
    private Animator animator;
    private Vector2 movement;
    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
        animator = GetComponent<Animator>();

    }
    private void Start()
    {
        FormatDirection();
    }

    private void Update()
    {
        movement = playerController.GetMovementInput(); // ���o���ʤ�V
        SetIdleAnimations();
        SetWalkAnimations();
        SetAttackAnimations();
    }

    void FormatDirection()
    {
        animator.SetFloat("PosX", 0);
        animator.SetFloat("PosY", -1);
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

    void SetAttackAnimations()
    {
        if (Input.GetKey(playerController.normalAttack)) // ���������
        {
            string newAnimation = "";

            if (playerController.attackUp) newAnimation = "Player_Attack_Up";
            else if (playerController.attackDown) newAnimation = "Player_Attack_Down";
            else if (playerController.attackLeft) newAnimation = "Player_Attack_Left";
            else if (playerController.attackRight) newAnimation = "Player_Attack_Right";

            if (!string.IsNullOrEmpty(newAnimation) && !animator.GetCurrentAnimatorStateInfo(0).IsName(newAnimation))
            {
                animator.CrossFade(newAnimation, 0.2f); // �u�b�ʵe���P�ɤ����A�קK���Ƽ���ۦP�ʵe
            }
        }
        else if (Input.GetKeyUp(playerController.normalAttack)) // ��}������
        {
            animator.CrossFade("Player_Idle", 0.2f); // �����^�ݾ��ʵe
        }
    }
}
