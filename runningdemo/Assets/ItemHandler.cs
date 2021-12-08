using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ItemHandler : MonoBehaviour
{
	//Fake item class as we need it for this script
	public class Item {
		public string ItemName;
		public Sprite ItemSprite;
		
		public Item(string name, Sprite sprite){
			ItemName = name;
			ItemSprite = sprite;
		}
	}

	//Fake inventory whilst we wait for you to make it
	
	public GameObject ItemSlot;
	//load a texture from a map
	//itemid 
	//get sprite which picks texture based on itemid
	
	//dump down 56 prefabs which just look like empty slots and put them in a list
	//for i in length of inventory
	//for each item, assign the relevant prefab that item
	
	//whenever i open the inventory show all the things in there
	
	GameObject selectedSlot;
	
	//List of all x coordinates for our squares (for now)
	List<int> xcoords = new List<int>(){
		200,
		350,
		500,
		650,
		800,
		950,
		1100
	};
	
	//List of all y coordinates for our squares (for now)
	List<int> ycoords = new List<int>(){
		500,
		350,
		200,
		50,
		-100,
		-250,
		-400,
		-550
	};
	
	//List of all the coordinates for slots in our inventory
	public List<Vector2> slotCoordinates = new List<Vector2>();
	
	//Unity hates bare for loops so this is a function that fills in all of the coordinates and puts them in the list
	void generateCoordinates(){
		foreach (int y in ycoords){
			foreach (int x in xcoords){
				Vector2 currentCoordinate = new Vector2(x - 13, y + 25);
				slotCoordinates.Add(currentCoordinate);
			}	
		}
	}
	
	//This is a list of a bunch of "Empty Item Slot" things idk really
	public List<GameObject> emptySlotsList = new List<GameObject>();
	
	//This makes a bunch of itemslot items and puts them in the right place
	void generateItemSlots(){
		foreach (Vector2 coord in slotCoordinates){
			GameObject currentSlot = Instantiate(ItemSlot, coord, Quaternion.identity);
			currentSlot.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
			emptySlotsList.Add(currentSlot);
		}
	}
	
	void fillInventory(){
		for (int i = 0; i < Inventory.playerInventory.Count; i++){
			Item currentItem = Inventory.playerInventory[i];
			GameObject currentSlot = emptySlotsList[i];
			currentSlot.GetComponent<Image>().sprite = currentItem.ItemSprite;
			currentSlot.GetComponent<ItemSlot>().heldItem = currentItem;
			//Debug.Log(currentItem.ItemSprite);
		}
	}
	
	void detectClicks(){
		if (Input.GetMouseButtonDown(0)){
			Debug.Log(Input.mousePosition);
		    Vector3 worldposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		    foreach (GameObject slot in emptySlotsList){
			    if (worldposition.x < slot.transform.position.x + 0.5 && worldposition.x > slot.transform.position.x - 0.5 && worldposition.y < slot.transform.position.y + 0.5 && worldposition.y > slot.transform.position.y - 0.5){
					if (!slot.GetComponent<ItemSlot>().isSelected && slot.GetComponent<ItemSlot>().heldItem.ItemName != "empty" && selectedList.Count < craftingSlots.Count){
						selectedList.Add(slot);
						slot.GetComponent<ItemSlot>().isSelected = true;
						resetCrafting();
						fillCrafting();
					}
					else if (slot.GetComponent<ItemSlot>().isSelected){
						slot.GetComponent<ItemSlot>().isSelected = false;
						selectedList.Remove(slot);
						resetCrafting();
						fillCrafting();
					}
			   }
		   }
		   foreach (GameObject a in selectedList){
						Debug.Log(a.GetComponent<ItemSlot>().heldItem.ItemName);
					}
	   }
	}

	//list of items
	//each time you click if it is not "empty" and less than 3:
	//if selected remove
	//if not selected add
	//then if two items that are in our quick fix dictionary are there
	//spit out a monster

	public List<GameObject> selectedList = new List<GameObject>();
	
	public static List<GameObject> craftingSlots = new List<GameObject>();
	
	void fillCrafting(){
		for (int i = 0; i < selectedList.Count; i++){
			GameObject currentSelectedSlot = selectedList[i];
			GameObject currentCraftingSlot = craftingSlots[i];
			currentCraftingSlot.GetComponent<Image>().sprite = currentSelectedSlot.GetComponent<ItemSlot>().heldItem.ItemSprite;
		}
	}
	
	void resetCrafting(){
		foreach (GameObject slot in craftingSlots){
			if (slot != null)
            {
				slot.GetComponent<Image>().sprite = null;
            }
			
		}
	}

	public void finishCrafting()
	{
		if (craftingSlots[craftingSlots.Count - 1].GetComponent<Image>().sprite != null)
		{
			GameObject clone = Instantiate(monsterPrefab, new Vector2(50000, 50000), Quaternion.identity);
			foreach (GameObject slot in selectedList)
			{
				if (slot.GetComponent<ItemSlot>().heldItem.ItemName == "Werewolf Teeth")
				{
					clone.GetComponent<MonsterHandler>().monsterDamage += 5;
				}
				else if (slot.GetComponent<ItemSlot>().heldItem.ItemName == "Werewolf Fur")
				{
					clone.GetComponent<MonsterHandler>().monsterHealth += 10;
				}
				else if (slot.GetComponent<ItemSlot>().heldItem.ItemName == "Spider Legs")
				{
					clone.GetComponent<MonsterHandler>().maxMovement ++;
					clone.GetComponent<MonsterHandler>().currentMovement ++;
				}
				else if (slot.GetComponent<ItemSlot>().heldItem.ItemName == "Spider Mandibles")
				{
					clone.GetComponent<MonsterHandler>().maxActions ++;
					clone.GetComponent<MonsterHandler>().currentActions++;
				}
				else if (slot.GetComponent<ItemSlot>().heldItem.ItemName == "Spider Web Sac")
				{
					clone.GetComponent<MonsterHandler>().spiderSpecial = true;
				}
				else if (slot.GetComponent<ItemSlot>().heldItem.ItemName == "Slimey Slime")
				{
					clone.GetComponent<MonsterHandler>().monsterHealth += 10;
					clone.GetComponent<MonsterHandler>().monsterDamage += 5;
					clone.GetComponent<MonsterHandler>().maxMovement--;
					clone.GetComponent<MonsterHandler>().currentMovement--;
				}
				else if (slot.GetComponent<ItemSlot>().heldItem.ItemName == "Toxic Slime")
				{
					clone.GetComponent<MonsterHandler>().slimeSpecial = true;
				}
				else if (slot.GetComponent<ItemSlot>().heldItem.ItemName == "Boxing Gloves")
				{
					clone.GetComponent<MonsterHandler>().kangarooSpecial = true;
				}
				else if (slot.GetComponent<ItemSlot>().heldItem.ItemName == "Dragon Head")
				{
					clone.GetComponent<MonsterHandler>().dragonSpecial = true;
				}
				Inventory.playerInventory.Remove(slot.GetComponent<ItemSlot>().heldItem);
			}

			MonsterList.addMonster(clone);
			craftingSlots.Clear();
			selectedList.Clear();

			if (PlayerLevel.playerLevel == 0)
            {
				SceneManager.LoadScene("Scenes/Levels/Level0", LoadSceneMode.Single);
            }
			else if (PlayerLevel.playerLevel == 3)
            {
				SceneManager.LoadScene("Scenes/Levels/Level3", LoadSceneMode.Single);
			}
			else if (PlayerLevel.playerLevel == 6)
			{
				SceneManager.LoadScene("Scenes/Levels/Level6", LoadSceneMode.Single);
			}
			else if (PlayerLevel.playerLevel == 9)
			{
				SceneManager.LoadScene("Scenes/Levels/Level9", LoadSceneMode.Single);
			}
			else if (PlayerLevel.playerLevel == 14)
			{
				SceneManager.LoadScene("Scenes/Levels/Level14", LoadSceneMode.Single);
			}
			else if (PlayerLevel.playerLevel == 16)
			{
				SceneManager.LoadScene("Scenes/Levels/Level16", LoadSceneMode.Single);
			}
			else if (PlayerLevel.playerLevel == 20)
			{
				SceneManager.LoadScene("Scenes/Levels/Level20", LoadSceneMode.Single);
			}
			else if (PlayerLevel.playerLevel == 23)
			{
				SceneManager.LoadScene("Scenes/Levels/Level23", LoadSceneMode.Single);
			}
			else if (PlayerLevel.playerLevel == 25)
			{
				SceneManager.LoadScene("Scenes/Levels/Level25", LoadSceneMode.Single);
			}

		}
	}
	
	public GameObject monsterPrefab;
	
	void instantiateCraftingSlots(){
		GameObject craftingSlot1 = Instantiate(ItemSlot, new Vector2(325 - 1280, 750 - 720), Quaternion.identity);
		craftingSlot1.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
		craftingSlots.Add(craftingSlot1);
	
		GameObject craftingSlot2 = Instantiate(ItemSlot, new Vector2(925 - 1280, 750 - 720), Quaternion.identity);
		craftingSlot2.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
		craftingSlots.Add(craftingSlot2);

		GameObject craftingSlot3 = Instantiate(ItemSlot, new Vector2(325 - 1280, 350 - 720), Quaternion.identity);
		craftingSlot3.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);

		GameObject craftingSlot4 = Instantiate(ItemSlot, new Vector2(925 - 1280, 350 - 720), Quaternion.identity);
		craftingSlot4.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
		
		if (PlayerLevel.playerLevel >= 5)
        {
			craftingSlots.Add(craftingSlot3);
        }
        else
        {
			craftingSlot3.GetComponent<Image>().color = new Color(0.8f, 0.8f, 0.8f, 1f);
        }
		
		if (PlayerLevel.playerLevel >= 10)
		{
			craftingSlots.Add(craftingSlot4);
		}
        else
        {
			craftingSlot4.GetComponent<Image>().color = new Color(0.8f, 0.8f, 0.8f, 1f);
		}
	}
	
    // Start is called before the first frame update
    void Start()
    {
		GameObject.Find("Craft Monster Button").GetComponentInChildren<Text>().text = "Create monster!";
		instantiateCraftingSlots();
		generateCoordinates();
		generateItemSlots();
		fillInventory();
    }

    // Update is called once per frame
    void Update()
    {
       detectClicks();
    }
}
