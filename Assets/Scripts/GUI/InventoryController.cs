using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryController : MonoBehaviour {
	public static InventoryController instance;

	public GameObject prefabItem;
	public GameObject inventoryListScrollArea;
	public GameObject inventoryList;
	public GameObject gearList;

	void Start () {
		instance = this;
		inventoryListScrollArea = transform.Find("InventoryList").gameObject;
		inventoryList = transform.Find("InventoryList").Find("Content").gameObject;
		gearList = GameObject.Find("GearList");
		refreshItemList();
		refreshGearList();
	}

	public void refreshItemList() {
		foreach(Transform child in inventoryList.transform) {
			Destroy(child.gameObject);
		}
		List<Item> inv = GameSaveController.instance.player.inventory;
		int i = 0;
		foreach (Item item in inv) {
			GameObject itemEntry = Instantiate(prefabItem) as GameObject;
			itemEntry.transform.parent = inventoryList.transform;
			itemEntry.transform.localPosition = new Vector3(-8.7f, 8.3f - (4.3f * i), 0f);
			ItemHolder holder = itemEntry.GetComponent<ItemHolder>();
			holder.setItem(item);
			i++;
		}
		tk2dUIScrollableArea sa = inventoryListScrollArea.GetComponent<tk2dUIScrollableArea>();
		sa.ContentLength = sa.MeasureContentLength();
	}

	public void refreshGearList() {
		ItemHolder blueHolder = gearList.transform.Find("Blue").GetComponent<ItemHolder>();
		ItemHolder greenHolder = gearList.transform.Find("Green").GetComponent<ItemHolder>();
		ItemHolder redHolder = gearList.transform.Find("Red").GetComponent<ItemHolder>();
		if (GameSaveController.instance.player.blueStone != null && GameSaveController.instance.player.blueStone.type != null)
			blueHolder.setItem(GameSaveController.instance.player.blueStone);
		if (GameSaveController.instance.player.greenStone != null && GameSaveController.instance.player.greenStone.type != null)
			greenHolder.setItem(GameSaveController.instance.player.greenStone);
		if (GameSaveController.instance.player.redStone != null && GameSaveController.instance.player.redStone.type != null)
			redHolder.setItem(GameSaveController.instance.player.redStone);
	}
}
