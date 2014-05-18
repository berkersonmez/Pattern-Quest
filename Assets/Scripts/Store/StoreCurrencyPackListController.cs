using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StoreCurrencyPackListController : MonoBehaviour {
	
	public string packType;
	public GameObject prefabStoreItem;
	
	[HideInInspector]
	public tk2dUIScrollableArea listScrollArea;
	[HideInInspector]
	public GameObject listContent;
	
	void Start() {
		listScrollArea = transform.Find("ShopGoodsList").GetComponent<tk2dUIScrollableArea>();
		listContent = listScrollArea.transform.Find("Content").gameObject;
	}
	
	public void refrestItemList() {
		foreach(Transform child in listContent.transform) {
			Destroy(child.gameObject);
		}
		List<StoreCurrencyPack> currencyPacks = StoreManager.Local.storeCurrencyPacks;
		int i = 0;
		foreach (StoreCurrencyPack currencyPack in currencyPacks) {
			GameObject itemEntry = Instantiate(prefabStoreItem) as GameObject;
			itemEntry.transform.parent = listContent.transform;
			itemEntry.transform.localPosition = new Vector3(-8.7f, 13.4f - (4.3f * i), 0f);
			StoreCurrencyPackHolder holder = itemEntry.GetComponent<StoreCurrencyPackHolder>();
			holder.setCurrencyPack(currencyPack);
			i++;
		}
		if (i != 0) {
			listScrollArea.ContentLength = listScrollArea.MeasureContentLength();
		}
	}
	
}
