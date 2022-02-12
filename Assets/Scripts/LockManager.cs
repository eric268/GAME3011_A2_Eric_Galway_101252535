using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LockManager : MonoBehaviour
{
    private Animator animator;
    private LockPickMovementScript lockPick;
    private UnlockAttributes unlockAttributes;
    private KeyMovement keyMovement;
    private readonly int blendHashValue = Animator.StringToHash("Blend");
    private float rotationCounter;
    public float rotationSpeed;
    private bool tryToOpenLock;
    private bool gameWon;
    Action<bool> FreezeLockPickRotation;
    Action<float> UpdateKeyRotation;

    // Start is called before the first frame update

    private void Awake()
    {
        animator = GetComponent<Animator>();
        unlockAttributes = GetComponent<UnlockAttributes>();
    }

    private void Start()
    {
        keyMovement = GameObject.FindObjectOfType<KeyMovement>();
        lockPick = GameObject.FindObjectOfType<LockPickMovementScript>();
        FreezeLockPickRotation = lockPick.FreeLockPickRotation;
        UpdateKeyRotation = keyMovement.SetKeyRotation;
    }

    public void SetLockBlendValue(float percentage)
    {
        animator.SetFloat(blendHashValue, percentage);
    }

    private void Update()
    {
        SetIfLockShouldOpen();
        MoveLock();
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

    void MoveLock()
    {
        if (tryToOpenLock)
        {
            if (CanLockTurn())
            {
                OpenLock();
            }
        }
        else if (rotationCounter > 0)
        {
            CloseLock();
        }
        if (rotationCounter == 0.0f)
        {
            FreezeLockPickRotation(false);
        }
        rotationCounter = Mathf.Clamp(rotationCounter, 0.0f, 1.0f);
    }
    void OpenLock()
    {
        rotationCounter += rotationSpeed * Time.deltaTime;
        SetLockBlendValue(rotationCounter);
        UpdateKeyRotation(rotationCounter);
        if (rotationCounter >= 1.0f)
        {
            gameWon = true;
            print("Game Won!");
        }
    }
    void CloseLock()
    {
        rotationCounter -= rotationSpeed * Time.deltaTime;
        SetLockBlendValue(rotationCounter);
        UpdateKeyRotation(rotationCounter);
    }

    bool CanLockTurn()
    {
        float maxTurnAmount = CalculateLockMaxTurnAmount();
        return rotationCounter <= maxTurnAmount;
    }

    float CalculateLockMaxTurnAmount()
    {
        float currentPickPosition = lockPick.animator.GetFloat(lockPick.pickBlendHashValue);
        float perfectPickPostion = (unlockAttributes.maxUnlockSpot + unlockAttributes.minUnlockSpot) / 2.0f;
        float distanceToUnlockedRotation = Mathf.Abs(perfectPickPostion - currentPickPosition) - (unlockAttributes.currentDifficultyRange / 2.0f);
        
       return (100.0f - (distanceToUnlockedRotation / 0.05f * unlockAttributes.currentLockTurnRatio)) / 100.0f;
    }
}
