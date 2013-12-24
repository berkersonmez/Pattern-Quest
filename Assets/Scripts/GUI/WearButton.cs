using UnityEngine;
using System.Collections;

public class WearButton : MonoBehaviour {

	private Item item;
	private tk2dUIItem itemUI;

	void Start () {
		ItemHolder holder = transform.parent.GetComponent<ItemHolder>();
		item = holder.item;
		itemUI = GetComponent<tk2dUIItem>();
		itemUI.OnClick += OnClick;
	}
	
	void OnClick() {
		// TODO: Item level requirement control
		Item swapped = null;
		if (item.type == "blue") {
			swapped = GameSaveController.instance.player.blueStone;
			GameSaveController.instance.player.blueStone = item;
		} else if (item.type == "green") {
			swapped = GameSaveController.instance.player.greenStone;
			GameSaveController.instance.player.greenStone = item;
		} else if (item.type == "red") {
			swapped = GameSaveController.instance.player.redStone;
			GameSaveController.instance.player.redStone = item;
		}
		GameSaveController.instance.player.inventory.Remove(item);
		if (swapped != null && swapped.type != null) {
			GameSaveController.instance.player.inventory.Add(swapped);
			GameSaveController.instance.player.unwearItem(swapped);
		}
		GameSaveController.instance.player.wearItem(item);

		InventoryController.instance.refreshGearList();
		InventoryController.instance.refreshItemList();
		GameSaveController.instance.saveGame();
	}
}
