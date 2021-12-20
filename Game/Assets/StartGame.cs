using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void selectDifficulty()
    {
        SceneManager.LoadScene("Scenes/DifficultySelect", LoadSceneMode.Single);
    }
    public void beginGameEasy()
    {
        SceneManager.LoadScene("Scenes/CraftingUI", LoadSceneMode.Single);
        Difficulty.difficulty = 0;
    }
    public void beginGameRegular()
    {
        SceneManager.LoadScene("Scenes/CraftingUI", LoadSceneMode.Single);
        Difficulty.difficulty = 1;
    }
    public void beginGameHard()
    {
        SceneManager.LoadScene("Scenes/CraftingUI", LoadSceneMode.Single);
        Difficulty.difficulty = 2;
    }
}