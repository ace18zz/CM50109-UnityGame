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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
