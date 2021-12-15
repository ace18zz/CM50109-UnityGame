using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicHandler : MonoBehaviour
{
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GameObject.Find("Music").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerLevel.playerLevel == 3 || PlayerLevel.playerLevel == 8 || PlayerLevel.playerLevel == 9 || PlayerLevel.playerLevel == 16 || PlayerLevel.playerLevel == 23)
        {
            audioSource.pitch = -1;
        }
        else
        {
            audioSource.pitch = 1;
        }
        
    }
}
