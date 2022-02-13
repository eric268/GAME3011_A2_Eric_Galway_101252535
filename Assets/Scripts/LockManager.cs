using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LockManager : MonoBehaviour
{
    private Animator animator;
    private LockPickManager lockPick;
    private UnlockAttributes unlockAttributes;
    private KeyMovement keyMovement;
    private readonly int blendHashValue = Animator.StringToHash("Blend");
    private float rotationCounter;
    private float pickMovementAmountOnDamage;
    private bool attemptToOpenLock;
    private bool lockPickTakingDamage;
    private bool lockPickBroken;
    Action<bool> FreezeLockPickRotation;
    Action<float> UpdateKeyRotation;
    Action PlayBrokenPickAnim;


    public float rotationSpeed;
    public float minimumLockMovement;
    public float timeUntilDamageReoccurs;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        unlockAttributes = GetComponent<UnlockAttributes>();
    }

    private void Start()
    {
        keyMovement = GameObject.FindObjectOfType<KeyMovement>();
        lockPick = GameObject.FindObjectOfType<LockPickManager>();
        FreezeLockPickRotation = lockPick.FreeLockPickRotation;
        UpdateKeyRotation = keyMovement.SetKeyRotation;
        PlayBrokenPickAnim = lockPick.LockPickBroke;

        pickMovementAmountOnDamage = minimumLockMovement;
    }

    public void SetLockBlendValue(float percentage)
    {
        animator.SetFloat(blendHashValue, percentage);
    }

    private void Update()
    {
        SetAttemptOpenLock();
        MoveLock();
    }

    void SetAttemptOpenLock()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            attemptToOpenLock = true;
            FreezeLockPickRotation(true);

        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            attemptToOpenLock = false;
        }
    }

    void MoveLock()
    {
        if (lockPickBroken)
            attemptToOpenLock = false;

        if (attemptToOpenLock)
        {
            if (CanLockTurn())
            {
                TurnLock();
            }
            else
            {
                //If the lock pick is already taking damage we don't need to keep invoking function since its repeating
                if (!lockPickTakingDamage)
                {
                    lockPickTakingDamage = true;
                    InvokeRepeating("DamageLockPick", 0.0f,2.0f);
                }

            }
        }
        //Lock should move to starting position if not trying to open it
        else if (rotationCounter > 0)
        {
            CloseLock();
            CancelInvoke("DamageLockPick");
            lockPickTakingDamage = false;
        }
        if (rotationCounter == 0.0f)
        {
            //We cant move lock pick when opening lock so reset bool when we are no longer moving the lock
            FreezeLockPickRotation(false);
            lockPickBroken = false;
        }
        rotationCounter = Mathf.Clamp(rotationCounter, 0.0f, 1.0f);
    }

    void TurnLock()
    {
        rotationCounter += rotationSpeed * Time.deltaTime;
        SetLockBlendValue(rotationCounter);
        UpdateKeyRotation(rotationCounter);
        if (rotationCounter >= 1.0f)
        {
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
        return rotationCounter <= CalculateLockMaxTurnAmount(); ;
    }

    float CalculateLockMaxTurnAmount()
    {
        float currentPickPosition = lockPick.animator.GetFloat(lockPick.pickBlendHashValue);
        float perfectPickPostion = (unlockAttributes.maxUnlockSpot + unlockAttributes.minUnlockSpot) / 2.0f;
        float distanceToUnlockedRotation = Mathf.Abs(perfectPickPostion - currentPickPosition) - (unlockAttributes.currentDifficultyRange / 2.0f);
        float rotationAmount = (100.0f - (distanceToUnlockedRotation / 0.05f * unlockAttributes.currentLockTurnRatio)) / 100.0f;

        return (rotationAmount <= minimumLockMovement) ? minimumLockMovement : rotationAmount;
    }

    void DamageLockPick()
    {
        rotationCounter -= pickMovementAmountOnDamage;
        SetLockBlendValue(rotationCounter);
        lockPick.lockPickCurrentHealth--;
        if (lockPick.lockPickCurrentHealth <= 0)
        {
            lockPickBroken = true;
            CancelInvoke("DamageLockPick");
            PlayBrokenPickAnim();
        }
        //Play Audio
    }
}
