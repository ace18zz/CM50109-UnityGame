using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnCounterUI : MonoBehaviour
{
    public static int turnNumber = 1;
    Text turnCounter;

    // Start is called before the first frame update
    void Start()
    {
        turnCounter = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        turnCounter.text = "Turn " + turnNumber;
    }
}
