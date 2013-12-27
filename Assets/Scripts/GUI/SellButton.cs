using UnityEngine;
using System.Collections;

public class SellButton : MonoBehaviour {
	
	private Item item;
	private tk2dUIItem itemUI;
	
	void Start () {
		ItemHolder holder = transform.parent.GetComponent<ItemHolder>();
		item = holder.item;
		itemUI = GetComponent<tk2dUIItem>();
		itemUI.OnClick += OnClick;
	}
	
	void OnClick() {
		GameSaveController.instance.player.inventory.Remove(item);
		GameSaveController.instance.player.gold += item.gold;
		InventoryController.instance.refreshItemList();
		TownController.instance.updateTexts();
		GameSaveController.instance.saveGame();
	}
}
