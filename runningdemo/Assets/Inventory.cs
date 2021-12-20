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
        new ItemHandler.Item("Spider Legs", Resources.Load<Sprite>("Spider Legs")),
        new ItemHandler.Item("Spider Legs", Resources.Load<Sprite>("Spider Legs")),
        new ItemHandler.Item("Spider Web Sac", Resources.Load<Sprite>("Spider Web Sac")),
        new ItemHandler.Item("Spider Mandibles", Resources.Load<Sprite>("Spider Mandibles")),
        new ItemHandler.Item("Spider Mandibles", Resources.Load<Sprite>("Spider Mandibles")),
        new ItemHandler.Item("Slimey Slime", Resources.Load<Sprite>("Slimey Slime")),
        new ItemHandler.Item("Slimey Slime", Resources.Load<Sprite>("Slimey Slime")),
        new ItemHandler.Item("Toxic Slime", Resources.Load<Sprite>("Toxic Slime")),
        new ItemHandler.Item("Boxing Gloves", Resources.Load<Sprite>("Boxing Gloves")),
        new ItemHandler.Item("Dragon Head", Resources.Load<Sprite>("Dragon Head"))
    };

    public static void startingInventory()
    {
        playerInventory.Add(new ItemHandler.Item("Werewolf Teeth", Resources.Load<Sprite>("Werewolf Teeth")));
        playerInventory.Add(new ItemHandler.Item("Werewolf Teeth", Resources.Load<Sprite>("Werewolf Teeth")));
        playerInventory.Add(new ItemHandler.Item("Werewolf Fur", Resources.Load<Sprite>("Werewolf Fur")));
        playerInventory.Add(new ItemHandler.Item("Werewolf Fur", Resources.Load<Sprite>("Werewolf Fur")));
    }
}
