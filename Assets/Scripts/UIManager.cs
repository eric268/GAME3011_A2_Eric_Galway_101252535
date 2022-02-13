using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    private bool gameRunning;
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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { 

    }

    void StartGame()
    {
        gameRunning = true;
        InvokeRepeating("DecrementTime", 0.0f, 1.0f);
    }

    void OnResetGamePressed()
    {

    }

    void OnGameWon()
    {
        gameRunning = false;
    }

    void OnGameLost()
    {
        gameRunning = true;
    }

    void DecrementTime()
    {
        secondsRemaining--;
        timeDisplayText.text = secondsRemaining + " seconds remaining";

        if (secondsRemaining <= 0)
        {
            gameRunning = false;
            CancelInvoke("DecrementTime");
        }
    }

}
