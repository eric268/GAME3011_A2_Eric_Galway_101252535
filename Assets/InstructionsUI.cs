using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class InstructionsUI : MonoBehaviour
{
    public void OnMainMenuPressed()
    {
        SceneManager.LoadScene("MainScene");
    }
}
