using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterList : ScriptableObject
{
    GameObject monsterPrefab;

    static public List<GameObject> monsterList = new List<GameObject>();
    
    static public void addMonster(GameObject monster)
    {
        monsterList.Add(monster);
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
