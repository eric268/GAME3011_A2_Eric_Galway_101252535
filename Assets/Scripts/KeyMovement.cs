using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyMovement : MonoBehaviour
{
    private Animator animator;
    private readonly int animBlendHash = Animator.StringToHash("Blend");

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetKeyRotation(float val)
    {
        animator.SetFloat(animBlendHash, val);
    }
}
