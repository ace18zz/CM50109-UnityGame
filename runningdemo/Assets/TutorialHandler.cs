using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialHandler : MonoBehaviour
{
    //Background from "https://www.flickr.com/photos/sunsetsailor/8388546928"

    public static bool isInTutorial = false;

    public void closeTutorial()
    {
        if (isInTutorial)
        {
            SceneManager.UnloadSceneAsync("Scenes/Tutorial");
            InputEnabled.isInputEnabled = true;
            isInTutorial = false;
        }
        
    }
    public void openTutorial()
    {
        if (!isInTutorial)
        {
            SceneManager.LoadScene("Scenes/Tutorial", LoadSceneMode.Additive);
            InputEnabled.isInputEnabled = false;
            isInTutorial = true;
        }
        
    }

    int currentPage = 1;

    public void flipPage()
    {
        GameObject page1 = GameObject.Find("Page1");
        GameObject page2 = GameObject.Find("Page2");
        GameObject panel = GameObject.Find("Panel");
        if (currentPage == 1)
        {
            panel.transform.SetSiblingIndex(1);
            page1.transform.SetSiblingIndex(2);
            page2.transform.SetSiblingIndex(3);
            currentPage = 2;
        }
        else if (currentPage == 2)
        {
            panel.transform.SetSiblingIndex(1);
            page1.transform.SetSiblingIndex(3);
            page2.transform.SetSiblingIndex(2);
            currentPage = 1;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
