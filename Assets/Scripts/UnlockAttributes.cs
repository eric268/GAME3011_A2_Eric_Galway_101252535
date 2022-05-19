using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockAttributes : MonoBehaviour
{
    [SerializeField]
    public static LockDifficulty lockDifficulty;
    public float easyUnlockRange = 0.2f;
    public float mediumUnlockRange = 0.1f;
    public float hardUnlockRange = 0.05f;

    public float easyLockTurnRatio = 10.0f;
    public float mediumLockTurnRatio = 20.0f;
    public float hardLockTurnRatio = 30.0f;

    public float currentLockTurnRatio;
    public float currentDifficultyRange;
    public float minUnlockSpot;
    public float maxUnlockSpot;

    public float playerLevelEffectOnUnlockRange;

    private void Awake()
    {
        SetUnlockAttributes();
    }

    public void SetUnlockAttributes()
    {
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
        currentDifficultyRange = range + PlayerAttributes.currentLevel * playerLevelEffectOnUnlockRange;
        minUnlockSpot = Random.Range(0.0f, 1.0f - currentDifficultyRange);
        maxUnlockSpot = minUnlockSpot + currentDifficultyRange;
    }

    public void ResetUnlockAttributes()
    {
        SetUnlockAttributes();
    }
}
