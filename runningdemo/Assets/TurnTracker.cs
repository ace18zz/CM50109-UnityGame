using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnTracker : MonoBehaviour
{
    SpriteRenderer sprite;
    
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (TurnHandler.isPlayerTurn)
        {
            sprite.color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            sprite.color = new Color(0.575f, 0.2f, 0.2f, 1f);
        }
    }
}
