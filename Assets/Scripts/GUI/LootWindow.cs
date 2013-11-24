using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LootWindow : MonoBehaviour {
	public static LootWindow instance;
	tk2dTextMesh textXP;
	tk2dTextMesh textGold;
	ItemHolder item1;
	ItemHolder item2;
	ItemHolder item3;
	ItemHolder item4;

	void Start () {
		instance = this;
		textXP = transform.Find("XP").GetComponent<tk2dTextMesh>();
		textGold = transform.Find("Gold").GetComponent<tk2dTextMesh>();
		item1 = transform.Find("Item 1").GetComponent<ItemHolder>();
		item2 = transform.Find("Item 2").GetComponent<ItemHolder>();
		item3 = transform.Find("Item 3").GetComponent<ItemHolder>();
		item4 = transform.Find("Item 4").GetComponent<ItemHolder>();
	}

	// Prepare loot window information using current creature.
	public void prepare() {
		Creature creature = DungeonController.instance.currentCreature;
		List<Item> items = creature.droppedItems;
		textXP.text = "XP: 0"; // TODO: Calculate XP.
		textXP.Commit();
		textGold.text = "Gold: " + creature.gold;
		textGold.Commit();
		if (items.Count >= 1) {
			item1.gameObject.SetActive(true);
			item1.setItem(items[0]);
		} else {
			item1.gameObject.SetActive(false);
		}
		if (items.Count >= 2) {
			item2.gameObject.SetActive(true);
			item2.setItem(items[1]);
		} else {
			item2.gameObject.SetActive(false);
		}
		if (items.Count >= 3) {
			item3.gameObject.SetActive(true);
			item3.setItem(items[2]);
		} else {
			item3.gameObject.SetActive(false);
		}
		if (items.Count >= 4) {
			item4.gameObject.SetActive(true);
			item4.setItem(items[3]);
		} else {
			item4.gameObject.SetActive(false);
		}
	}
}
