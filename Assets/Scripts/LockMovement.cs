using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LockMovement : MonoBehaviour
{
    private Animator animator;
    private LockPickMovementScript lockPick;
    private readonly int blendHashValue = Animator.StringToHash("Blend");
    private float lockRotationCount;
    public float lockRotationSpeed;
    private bool tryToOpenLock;
    private bool gameWon;
    Action<bool> FreezeLockPickRotation;
    // Start is called before the first frame update

    private void Awake()
    {
        animator = GetComponent<Animator>();

    }

    private void Start()
    {
        lockPick = GameObject.FindObjectOfType<LockPickMovementScript>();
        FreezeLockPickRotation = lockPick.FreeLockPickRotation;
    }

    public void SetUnlockPercentage(float percentage)
    {
        animator.SetFloat(blendHashValue, percentage);
    }

    private void Update()
    {
        SetIfLockShouldOpen();
        UpdateLockMovement();
    }

    void SetIfLockShouldOpen()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            tryToOpenLock = true;
            FreezeLockPickRotation(true);

        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            tryToOpenLock = false;
        }
    }

    void UpdateLockMovement()
    {
        if (tryToOpenLock)
        {
            lockRotationCount += lockRotationSpeed * Time.deltaTime;
            SetUnlockPercentage(lockRotationCount);
            if (lockRotationCount >= 1.0f)
            {
                gameWon = true;
                print("Game Won!");
            }
        }
        else if (lockRotationCount > 0)
        {
            lockRotationCount -= lockRotationSpeed * Time.deltaTime;
            SetUnlockPercentage(lockRotationCount);
        }
        lockRotationCount = Mathf.Clamp(lockRotationCount, 0.0f, 1.0f);
        if (lockRotationCount == 0.0f)
        {
            FreezeLockPickRotation(false);
        }
    }
}
