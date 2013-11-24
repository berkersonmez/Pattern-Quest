using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LootWindow : MonoBehaviour {
	public static LootWindow instance;
	tk2dTextMesh textXP;
	tk2dTextMesh textGold;
	tk2dSprite spriteItem1;
	tk2dSprite spriteItem2;
	tk2dSprite spriteItem3;
	tk2dSprite spriteItem4;

	void Start () {
		instance = this;
		textXP = transform.Find("XP").GetComponent<tk2dTextMesh>();
		textGold = transform.Find("Gold").GetComponent<tk2dTextMesh>();
		spriteItem1 = transform.Find("Item 1").GetComponent<tk2dSprite>();
		spriteItem2 = transform.Find("Item 2").GetComponent<tk2dSprite>();
		spriteItem3 = transform.Find("Item 3").GetComponent<tk2dSprite>();
		spriteItem4 = transform.Find("Item 4").GetComponent<tk2dSprite>();
	}

	public void prepare() {
		Creature creature = DungeonController.instance.currentCreature;
		List<Item> items = creature.droppedItems;
		textXP.text = "XP: 0"; // TODO: Calculate XP.
		textXP.Commit();
		textGold.text = "Gold: " + creature.gold;
		textGold.Commit();
		if (items.Count >= 1) {
			spriteItem1.gameObject.SetActive(true);
			spriteItem1.SetSprite(spriteItem1.GetSpriteIdByName(items[0].spriteName));
			tk2dTextMesh itemText = spriteItem1.transform.Find("Text").GetComponent<tk2dTextMesh>();
			itemText.text = items[0].name;
			itemText.Commit();
		} else {
			spriteItem1.gameObject.SetActive(false);
		}
		if (items.Count >= 2) {
			spriteItem2.gameObject.SetActive(true);
			spriteItem2.SetSprite(spriteItem2.GetSpriteIdByName(items[1].spriteName));
			tk2dTextMesh itemText = spriteItem2.transform.Find("Text").GetComponent<tk2dTextMesh>();
			itemText.text = items[1].name;
			itemText.Commit();
		} else {
			spriteItem2.gameObject.SetActive(false);
		}
		if (items.Count >= 3) {
			spriteItem3.gameObject.SetActive(true);
			spriteItem3.SetSprite(spriteItem3.GetSpriteIdByName(items[2].spriteName));
			tk2dTextMesh itemText = spriteItem3.transform.Find("Text").GetComponent<tk2dTextMesh>();
			itemText.text = items[2].name;
			itemText.Commit();
		} else {
			spriteItem3.gameObject.SetActive(false);
		}
		if (items.Count >= 4) {
			spriteItem4.gameObject.SetActive(true);
			spriteItem4.SetSprite(spriteItem4.GetSpriteIdByName(items[3].spriteName));
			tk2dTextMesh itemText = spriteItem4.transform.Find("Text").GetComponent<tk2dTextMesh>();
			itemText.text = items[3].name;
			itemText.Commit();
		} else {
			spriteItem4.gameObject.SetActive(false);
		}
	}
}
