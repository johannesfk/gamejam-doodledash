using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ObjectAnimator : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayTheThing()
    {
        animator.SetTrigger("Paper Fall");
    }
}
