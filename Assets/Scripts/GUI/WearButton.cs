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
		// Basic level check
		if (item.level > GameSaveController.instance.getPlayer().level) {
			Notification.activate("Your character level is too low to wear this item.", null);
		}
		if (item.type == "consumable") {
			GameSaveController.instance.player.consumeItem(item);
		} else {
			GameSaveController.instance.player.wearItem(item);
		}
		InventoryController.instance.refreshGearList();
		InventoryController.instance.refreshItemList();
		GameSaveController.instance.saveGame();
	}
}
