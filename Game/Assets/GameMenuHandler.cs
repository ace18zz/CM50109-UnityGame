using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuHandler : MonoBehaviour
{
    public static bool isInMenu = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isInMenu)
            {
                isInMenu = true;
                InputEnabled.isInputEnabled = false;
                SceneManager.LoadScene("Scenes/GameMenu", LoadSceneMode.Additive);
            }
            else
            {
                isInMenu = false;
                InputEnabled.isInputEnabled = true;
                SceneManager.UnloadSceneAsync("Scenes/GameMenu");
            }

        }
    }
}
