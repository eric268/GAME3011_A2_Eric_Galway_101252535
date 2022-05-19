using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainUIManager : MonoBehaviour
{
    public TMP_Dropdown difficultyDropDown;
    public TMP_Dropdown levelDropDown;

    private void Start()
    {
        difficultyDropDown.onValueChanged.AddListener(delegate
        {
            OnDifficultyLevelSelected();
        });

        levelDropDown.onValueChanged.AddListener(delegate
        {
            OnPlayerLevelSelected();
        });

    }

    public void OnLockPickingPressed()
    {
        SceneManager.LoadScene("LockPickingMiniGame");
    }

    public void OnInstructionsPressed()
    {
        SceneManager.LoadScene("InstructionsScene");
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }

    public void OnDifficultyLevelSelected()
    {
        UnlockAttributes.lockDifficulty = (LockDifficulty)difficultyDropDown.value;
        Debug.Log(UnlockAttributes.lockDifficulty);
    }

    public void OnPlayerLevelSelected()
    {
        PlayerAttributes.currentLevel = levelDropDown.value + 1;
        Debug.Log(PlayerAttributes.currentLevel);
    }
}
