using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DefeatScreen : MonoBehaviour
{
    public void retry()
    {
        PlayerLevel.playerLevel = 0;
        Inventory.startingInventory();
        SceneManager.LoadScene("Scenes/CraftingUI", LoadSceneMode.Single);
    }

    public void quit()
    {
        Application.Quit();
    }

    public void mute()
    {
        AudioSource music = GameObject.Find("Music").GetComponent<AudioSource>();
        music.mute = !music.mute;
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
