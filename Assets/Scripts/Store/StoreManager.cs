using UnityEngine;
using System.Collections;
using Soomla.Implementation;
using Soomla;
using System.Collections.Generic;
using System.Linq;

public class StoreManager : MonoBehaviour {
	
	public static class Local {
		public static List<StoreGood> storeGoods;
		public static List<StoreCurrencyPack> storeCurrencyPacks;
		
		public static void init() {
			storeGoods = XmlParse.instance.getStoreGoods();
			storeCurrencyPacks = XmlParse.instance.getStoreCurrencyPacks();
		}
		
		public static List<StoreGood> getGoodsInCategory(string category) {
			return storeGoods.Where(g => g.category == category).ToList();
		}
	}

	public static StoreManager instance;
	
	public IStoreAssets GameStore {get; private set;}

	void Awake() {
		instance = this;
	}
	
	void Start() {
		GameStore = new Store();
		initializeListeners();
		StoreController.Initialize(GameStore);
		Local.init();
	}
	
	private void initializeListeners() {
		StoreEvents.OnItemPurchased += OnItemPurchased;
	}
	
	void OnItemPurchased(PurchasableVirtualItem item) {
		
	}
	
	public void refreshStoreLists() {
		StoreCategoryController[] controllers = FindObjectsOfType(typeof(StoreCategoryController)) as StoreCategoryController[];
		foreach (StoreCategoryController controller in controllers) {
			controller.refrestItemList();
		}
	}
	
}
