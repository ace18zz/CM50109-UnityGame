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
        if (PlayerLevel.playerLevel == 3 || PlayerLevel.playerLevel == 5 || PlayerLevel.playerLevel == 8 || PlayerLevel.playerLevel == 10)
        {
            SceneManager.LoadScene("Scenes/CraftingUI", LoadSceneMode.Single);
        }
        else if (PlayerLevel.playerLevel == 2)
        {
            //load level 2
        }
        else if (PlayerLevel.playerLevel == 4)
        {
            //load level 4
        }
        else if (PlayerLevel.playerLevel == 6)
        {
            //load level 6
        }
        else if (PlayerLevel.playerLevel == 7)
        {
            //load level 7
        }
        else if (PlayerLevel.playerLevel == 9)
        {
            //load level 9
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
