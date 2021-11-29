using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DefeatScreenHandler : MonoBehaviour
{
    public void retry()
    {
        SceneManager.LoadScene("Scenes/CraftingUI", LoadSceneMode.Single);
    }

    public void quit()
    {
        Application.Quit();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Retry button").GetComponentInChildren<Text>().text = "Retry";
        GameObject.Find("Quit button").GetComponentInChildren<Text>().text = "Quit";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
