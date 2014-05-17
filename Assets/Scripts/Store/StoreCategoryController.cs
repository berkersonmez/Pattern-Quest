using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StoreCategoryController : MonoBehaviour {

	public string categoryName;
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
		List<StoreGood> goods = StoreManager.Local.getGoodsInCategory(categoryName);
		int i = 0;
		foreach (StoreGood good in goods) {
			GameObject itemEntry = Instantiate(prefabStoreItem) as GameObject;
			itemEntry.transform.parent = listContent.transform;
			itemEntry.transform.localPosition = new Vector3(-8.7f, 13.4f - (4.3f * i), 0f);
			StoreItemHolder holder = itemEntry.GetComponent<StoreItemHolder>();
			holder.setGood(good);
			i++;
		}
		if (i != 0) {
			listScrollArea.ContentLength = listScrollArea.MeasureContentLength();
		}
	}
	
}
