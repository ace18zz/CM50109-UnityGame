using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : ScriptableObject
{
    static public List<ItemHandler.Item> playerInventory = new List<ItemHandler.Item>()
    {
        new ItemHandler.Item("Werewolf Teeth", Resources.Load<Sprite>("Werewolf Teeth")),
        new ItemHandler.Item("Werewolf Teeth", Resources.Load<Sprite>("Werewolf Teeth")),
        new ItemHandler.Item("Werewolf Fur", Resources.Load<Sprite>("Werewolf Fur")),
        new ItemHandler.Item("Werewolf Fur", Resources.Load<Sprite>("Werewolf Fur")),
        new ItemHandler.Item("Werewolf Paw", Resources.Load<Sprite>("Werewolf Paw")),
        new ItemHandler.Item("Werewolf Paw", Resources.Load<Sprite>("Werewolf Paw"))
    };
}
