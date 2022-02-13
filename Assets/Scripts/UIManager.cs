using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private int secondsRemaining;

    public TextMeshProUGUI playerLevelText;
    public TextMeshProUGUI successRateText;
    public TextMeshProUGUI lockDifficultyText;
    public TextMeshProUGUI winLossMessageText;
    public TextMeshProUGUI timeDisplayText;

    public int startingTime;
    public static int successRate;
    public static int playerLevel;
    public static LockDifficulty lockDifficulty;

    private Action ResetGame;
    private KeyMovement keyMovement;
    private LockManager lockManager;
    private LockPickManager lockPickManager;
    private PlayerAttributes playerAttributes;
    private UnlockAttributes unlockAttributes;

    private void Awake()
    {
        keyMovement = FindObjectOfType<KeyMovement>();
        lockManager = FindObjectOfType<LockManager>();
        lockPickManager = FindObjectOfType<LockPickManager>();
        playerAttributes = FindObjectOfType<PlayerAttributes>();
        unlockAttributes = FindObjectOfType<UnlockAttributes>();


        ResetGame = keyMovement.ResetKeyPosition;
        ResetGame += lockManager.ResetLock;
        ResetGame += playerAttributes.RandomizePlayerLevel;
        ResetGame += unlockAttributes.ResetUnlockAttributes;
        ResetGame += lockPickManager.ResetPressed;
    }

    void Start()
    {
        StartGame();
    }

    void StartGame()
    {
        secondsRemaining = startingTime;
        SetUIText();
        if (!IsInvoking("DecrementTime"))
            InvokeRepeating("DecrementTime", 0.0f, 1.0f);
    }

    public void OnQuitButtonPressed()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void OnResetButtonPressed()
    {
        ResetGame();
        StartGame();
    }

    public void GameOver()
    {
        CancelInvoke("DecrementTime");
        if (lockManager.gameWon)
        {
            winLossMessageText.color = Color.green;
            winLossMessageText.text = "You're in!";
        }
        else
        {
            winLossMessageText.color = Color.red;
            winLossMessageText.text = "You got caught";
        }
    }

    void DecrementTime()
    {
        secondsRemaining--;
        timeDisplayText.text = "Timer: " + secondsRemaining;
        if (secondsRemaining <= 0)
        {
            GameOver();
        }
    }

    void SetUIText()
    {
        playerLevelText.text = "Player Level: " + PlayerAttributes.currentLevel;
        successRateText.text = "Success Rate: %" + (unlockAttributes.currentDifficultyRange * 100.0f);
        lockDifficultyText.text = "Lock Difficulty : " + unlockAttributes.lockDifficulty;
        winLossMessageText.text = "";
        timeDisplayText.text = "Timer: " + secondsRemaining;
    }
}
