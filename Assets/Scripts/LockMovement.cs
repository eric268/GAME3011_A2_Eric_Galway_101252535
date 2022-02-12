using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockMovement : MonoBehaviour
{
    private Animator animator;
    private readonly int blendHashValue = Animator.StringToHash("Blend");
    // Start is called before the first frame update

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetUnlockPercentage(float percentage)
    {
        animator.SetFloat(blendHashValue, percentage);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            SetUnlockPercentage(0.65f);
    }
}
