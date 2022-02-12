using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockAttributes : MonoBehaviour
{
    [SerializeField]
    public LockDifficulty lockDifficulty;
    public float easyUnlockRange = 0.2f;
    public float mediumUnlockRange = 0.1f;
    public float hardUnlockRange = 0.05f;

    public float easyLockTurnRatio = 0.1f;
    public float mediumLockTurnRatio = 0.2f;
    public float hardLockTurnRatio = 0.3f;

    public float currentLockTurnRatio;
    public float currentDifficultyRange;
    public float minUnlockSpot;
    public float maxUnlockSpot;

    private void Start()
    {
        SetUnlockAttributes();
    }

    public void SetUnlockAttributes()
    {
        lockDifficulty = RandomlySelectLockDifficulty();

        switch (lockDifficulty)
        {
            case LockDifficulty.Easy:
                GenerateMinMaxUnlockRange(easyUnlockRange);
                currentLockTurnRatio = easyLockTurnRatio;
                break;
            case LockDifficulty.Medium:
                GenerateMinMaxUnlockRange(mediumUnlockRange);
                currentLockTurnRatio = mediumLockTurnRatio;
                break;
            case LockDifficulty.Hard:
                GenerateMinMaxUnlockRange(hardUnlockRange);
                currentLockTurnRatio = hardLockTurnRatio;
                break;
        }
    }

    void GenerateMinMaxUnlockRange(float range)
    {
        currentDifficultyRange = range;
        minUnlockSpot = Random.Range(0.0f, 1.0f - currentDifficultyRange);
        maxUnlockSpot = minUnlockSpot + currentDifficultyRange;
    }

    LockDifficulty RandomlySelectLockDifficulty()
    {
        return (LockDifficulty)Random.Range(0, (int)LockDifficulty.Number_Of_Lock_Difficulties);
    }
}
