using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryScreen : MonoBehaviour
{
    public void nextLevel()
    {
        PlayerLevel.playerLevel++;
        if (PlayerLevel.playerLevel == 3 || PlayerLevel.playerLevel == 6 || PlayerLevel.playerLevel == 9 || PlayerLevel.playerLevel == 14 || PlayerLevel.playerLevel == 16 || PlayerLevel.playerLevel == 20 || PlayerLevel.playerLevel == 23 || PlayerLevel.playerLevel == 25)
        {
            SceneManager.LoadScene("Scenes/CraftingUI", LoadSceneMode.Single);
        }
        else if (PlayerLevel.playerLevel == 1)
        {
            SceneManager.LoadScene("Scenes/Levels/Level1", LoadSceneMode.Single);
        }
        else if (PlayerLevel.playerLevel == 2)
        {
            SceneManager.LoadScene("Scenes/Levels/Level2", LoadSceneMode.Single);
        }
        else if (PlayerLevel.playerLevel == 4)
        {
            SceneManager.LoadScene("Scenes/Levels/Level4", LoadSceneMode.Single);
        }
        else if (PlayerLevel.playerLevel == 5)
        {
            SceneManager.LoadScene("Scenes/Levels/Level5", LoadSceneMode.Single);
        }
        else if (PlayerLevel.playerLevel == 7)
        {
            SceneManager.LoadScene("Scenes/Levels/Level7", LoadSceneMode.Single);
        }
        else if (PlayerLevel.playerLevel == 8)
        {
            SceneManager.LoadScene("Scenes/Levels/Level8", LoadSceneMode.Single);
        }
        else if (PlayerLevel.playerLevel == 10)
        {
            SceneManager.LoadScene("Scenes/Levels/Level10", LoadSceneMode.Single);
        }
        else if (PlayerLevel.playerLevel == 11)
        {
            SceneManager.LoadScene("Scenes/Levels/Level11", LoadSceneMode.Single);
        }
        else if (PlayerLevel.playerLevel == 12)
        {
            SceneManager.LoadScene("Scenes/Levels/Level12", LoadSceneMode.Single);
        }
        else if (PlayerLevel.playerLevel == 13)
        {
            SceneManager.LoadScene("Scenes/Levels/Level13", LoadSceneMode.Single);
        }
        else if (PlayerLevel.playerLevel == 15)
        {
            SceneManager.LoadScene("Scenes/Levels/Level15", LoadSceneMode.Single);
        }
        else if (PlayerLevel.playerLevel == 17)
        {
            SceneManager.LoadScene("Scenes/Levels/Level17", LoadSceneMode.Single);
        }
        else if (PlayerLevel.playerLevel == 18)
        {
            SceneManager.LoadScene("Scenes/Levels/Level18", LoadSceneMode.Single);
        }
        else if (PlayerLevel.playerLevel == 19)
        {
            SceneManager.LoadScene("Scenes/Levels/Level19", LoadSceneMode.Single);
        }
        else if (PlayerLevel.playerLevel == 21)
        {
            SceneManager.LoadScene("Scenes/Levels/Level21", LoadSceneMode.Single);
        }
        else if (PlayerLevel.playerLevel == 22)
        {
            SceneManager.LoadScene("Scenes/Levels/Level22", LoadSceneMode.Single);
        }
        else if (PlayerLevel.playerLevel == 24)
        {
            SceneManager.LoadScene("Scenes/Levels/Level24", LoadSceneMode.Single);
        }
    }

    public void quit()
    {
        Application.Quit();
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Retry button").GetComponentInChildren<Text>().text = "Next Level";
        GameObject.Find("Quit button").GetComponentInChildren<Text>().text = "Quit";
    }

    // Update is called once per frame
    void Update()
    {

    }
}
