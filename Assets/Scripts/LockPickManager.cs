using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockPickManager : MonoBehaviour
{
    public Animator animator;
    private GameObject keyHole;
    private bool freezeLockPickRotation;
    private float currentMouseXPos;
    private float prevMouseXPos;
    public float rotationSpeed;
    float pickRotationCount;
    private bool mouseDown;
    private Vector3 startingPos;
    public int pickBlendHashValue = Animator.StringToHash("Blend");

    PickBrokenScript[] lockPickParts;

    public int lockPickStartingHealth;
    public int lockPickCurrentHealth;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        lockPickParts = GetComponentsInChildren<PickBrokenScript>();
    }

    // Start is called before the first frame update
    void Start()
    {
        keyHole = GameObject.FindGameObjectWithTag("KeyHole");
        lockPickCurrentHealth = lockPickStartingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        MovePickWithLock();
        SetMouseDown();
        RotatePick();
    }

    public void FreeLockPickRotation(bool val)
    {
        freezeLockPickRotation = val;
    }

    public void MovePickWithLock()
    {
        //The lock pick position is frozen when trying to open the lock
        if (freezeLockPickRotation)
        {
            transform.position = keyHole.transform.position;
        }
    }

    void SetMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            prevMouseXPos = Input.mousePosition.x;
            mouseDown = true;
        }
        else if (Input.GetMouseButtonUp(0))
            mouseDown = false;
    }

    void RotatePick()
    {
        if (mouseDown && !freezeLockPickRotation)
        {
            currentMouseXPos = Input.mousePosition.x;
            float distance = currentMouseXPos - prevMouseXPos;
            pickRotationCount += distance * rotationSpeed;
            pickRotationCount = Mathf.Clamp(pickRotationCount, 0.0f, 1.0f);
            animator.SetFloat(pickBlendHashValue, pickRotationCount);
            prevMouseXPos = currentMouseXPos;
        }
    }

    public void LockPickBroke()
    {
        SoundEffectManager.PlaySound("PickBroke");
        foreach (var pick in lockPickParts)
            pick.PickBroke();
        animator.SetFloat(pickBlendHashValue, 0.0f);
        lockPickCurrentHealth = lockPickStartingHealth;
        Invoke("ResetLockPick", 2.5f);
    }

    public void ResetLockPick()
    {
        foreach (var pick in lockPickParts)
            pick.ResetPick();
        transform.position = keyHole.transform.position;
        pickRotationCount = 0;
       //animator.SetFloat(pickBlendHashValue, pickRotationCount);
        lockPickCurrentHealth = lockPickStartingHealth;
    }

    public void ResetPressed()
    {
        //foreach (var pick in lockPickParts)
        //    pick.PickBroke();
        Invoke("ResetLockPick", 0.1f);
        animator.SetFloat(pickBlendHashValue, 0.0f);
        lockPickCurrentHealth = lockPickStartingHealth;

    }
}
