using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animtest : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        
    }


    void Update()
    {
        
    }


    void PlayerWalk()
    {

    }

    void PlayerAttack()
    {
        if(Input.GetKey(KeyCode.Mouse0))
        {
            //animator.
        }
    }
}
