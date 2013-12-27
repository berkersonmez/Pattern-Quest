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
		GameSaveController.instance.player.wearItem(item);

		InventoryController.instance.refreshGearList();
		InventoryController.instance.refreshItemList();
		GameSaveController.instance.saveGame();
	}
}
