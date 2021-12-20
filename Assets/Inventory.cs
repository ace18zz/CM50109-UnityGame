using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : ScriptableObject
{
    static public List<ItemHandler.Item> playerInventory = new List<ItemHandler.Item>();

    public static void startingInventory()
    {
        playerInventory.Add(new ItemHandler.Item("Werewolf Teeth", Resources.Load<Sprite>("Werewolf Teeth")));
        playerInventory.Add(new ItemHandler.Item("Werewolf Teeth", Resources.Load<Sprite>("Werewolf Teeth")));
        playerInventory.Add(new ItemHandler.Item("Werewolf Fur", Resources.Load<Sprite>("Werewolf Fur")));
        playerInventory.Add(new ItemHandler.Item("Werewolf Fur", Resources.Load<Sprite>("Werewolf Fur")));
    }
}
