using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{
    public static int currentLevel;
    public int startingLevel;
    public int maxLevel;

    private void Awake()
    {
        currentLevel = Random.Range(startingLevel, maxLevel);
    }

    public void RandomizePlayerLevel()
    {
        currentLevel = Random.Range(startingLevel, maxLevel);
    }
}
