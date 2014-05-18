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
	
	// Purchased with market
	void OnItemPurchased(PurchasableVirtualItem item) {
		// TODO: Fill
		TownController.instance.updateTexts();
		refreshStoreLists();
		GameSaveController.instance.saveGame();
	}
	
	// Purchased with virtual good
	public void OnItemPurchasedInGame(StoreGood good) {
		Player player = GameSaveController.instance.getPlayer();
		switch (good.category) {
			case "item":
			Item item = XmlParse.instance.getItem(good.realName);
			player.inventory.Add(item);
			InventoryController.instance.refreshItemList();
			break;
			case "spell":
			player.spellList.Add(Globals.instance.getSpell(good.realName, player));
			SpellListController.instance.refreshSpellList();
			break;
			case "combo":
			player.comboSpells.Add(Globals.instance.getSpell(good.realName, player));
			SpellListController.instance.refreshComboList();
			break;
			case "power":
			player.powers.Add(Globals.instance.getPower(good.realName, player));
			SpellListController.instance.refreshPowerList();
			break;
		}
		refreshStoreLists();
		TownController.instance.updateTexts();
		GameSaveController.instance.saveGame();
	}
	
	public void refreshStoreLists() {
		StoreCategoryController[] controllers = FindObjectsOfType(typeof(StoreCategoryController)) as StoreCategoryController[];
		foreach (StoreCategoryController controller in controllers) {
			controller.refrestItemList();
		}
		StoreCurrencyPackListController[] packControllers = FindObjectsOfType(typeof(StoreCurrencyPackListController)) as StoreCurrencyPackListController[];
		foreach (StoreCurrencyPackListController packController in packControllers) {
			packController.refrestItemList();
		}
	}
	
}
