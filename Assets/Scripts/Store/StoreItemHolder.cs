using UnityEngine;
using System.Collections;
using Soomla;
using System.Linq;

// Item slots in loot window and inventory.
public class StoreItemHolder : MonoBehaviour {
	
	public StoreGood good;
	public tk2dSprite avatar; // Assign using inspector
	public tk2dTextMesh text; // Assign using inspector
	public tk2dTextMesh t_buyButton; // Assign using inspector
	public tk2dSlicedSprite s_buyButton; // Assign using inspector
	public tk2dSprite s_buyButtonSign; // Assign using inspector
	public tk2dUIItem b_buyButton; // Assign using inspector
	public Color c_buyable; // Assign using inspector
	public Color c_unaffordable; // Assign using inspector
	public Color c_alreadyBought; // Assign using inspector
	
	private tk2dUIItem itemUI;
	private string tooltipText;
	private bool buyable;
	
	void Start () {
		itemUI = GetComponent<tk2dUIItem>();
		itemUI.OnDown += OnDown;
		itemUI.OnRelease += OnRelease;
		b_buyButton.OnClick += OnBuy;
	}
	
	// Sets good of the good slot.
	public void setGood(StoreGood good) {
		buyable = false;
		this.good = good;
		if (avatar != null) {
			avatar.SetSprite(good.spriteName);
		}
		if (text != null) {
			text.text = good.name;
			text.Commit();
		}
		if (t_buyButton != null) {
			t_buyButton.text = good.costAmount.ToString();
			t_buyButton.Commit();
		}
		if (s_buyButtonSign != null) {
			s_buyButtonSign.SetSprite("avatar_currency_" + good.costType);
		}
		if (s_buyButton != null) {
			if (alreadyBought()) {
				s_buyButton.color = c_alreadyBought;
				buyable = false;
			} else if (!affordable()) {
				s_buyButton.color = c_unaffordable;
				buyable = false;
			} else {
				s_buyButton.color = c_buyable;
				buyable = true;
			}
		}
	}
	
	void OnDown() {
		Invoke("OnHold", .5f);
	}
	
	void OnRelease() {
		if (IsInvoking("OnHold")) {
			CancelInvoke("OnHold");
		}
	}
	
	void setTooltipText() {
		tooltipText = "^CFF914Dff" + good.name + "\n";
		tooltipText += "^CE0E0E0ff" + good.description + "\n";
	}
	
	void OnHold() {
		if (good != null) {
			setTooltipText();
			Tooltip.instance.setText(tooltipText);
			Tooltip.instance.showTooltip(transform.position);
		}
	}
	
	void OnBuy() {
		Player player = GameSaveController.instance.getPlayer();
		if (buyable) {
			if (good.costType == "gold") {
				player.gold -= good.costAmount;
				GameSaveController.instance.getStats().countingStat("Gold spent", good.costAmount);
			} else if (good.costType == "crystal") {
				player.crystal -= good.costAmount;
				GameSaveController.instance.getStats().countingStat("Crystals spent", good.costAmount);
			}
			StoreManager.instance.OnItemPurchasedInGame(good);
		} else {
			if (!affordable()) {
				Notification.activate("You cannot afford to buy this store good!", null);
			} else {
				Notification.activate("You already possess this store good!", null);
			}
		}
	}
	
	bool affordable() {
		Player player = GameSaveController.instance.getPlayer();
		if (good.costType == "gold") {
			if (good.costAmount > player.gold) return false;
		} else if (good.costType == "crystal") {
			if (good.costAmount > player.crystal) return false;
		}
		return true;
	}
	
	bool alreadyBought() {
		Player player = GameSaveController.instance.getPlayer();
		switch (good.category) {
		case "item":
			return false;
		case "spell":
			return player.spellList.Any(s => s.idName == good.realName);
		case "combo":
			return player.comboSpells.Any(s => s.idName == good.realName);
		case "power":
			return player.powers.Any(s => s.idName == good.realName);
		}
		return false;
	}
}
