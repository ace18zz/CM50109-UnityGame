using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot : MonoBehaviour
{
	
	static Sprite nosprite;
	public ItemHandler.Item heldItem = new ItemHandler.Item("empty", nosprite);
	public bool isSelected = false;
	
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
