using UnityEngine;
using System.Collections;
using Soomla;
using System.Linq;

public class StoreCurrencyPackHolder : MonoBehaviour {
	
	public StoreCurrencyPack currencyPack;
	public tk2dSprite avatar; // Assign using inspector
	public tk2dTextMesh text; // Assign using inspector
	public tk2dTextMesh t_buyButton; // Assign using inspector
	public tk2dTextMesh t_packSize; // Assign using inspector
	public tk2dSlicedSprite s_buyButton; // Assign using inspector
	public tk2dUIItem b_buyButton; // Assign using inspector
	public Color c_buyable; // Assign using inspector
	
	private tk2dUIItem itemUI;
	private string tooltipText;
	
	void Start () {
		itemUI = GetComponent<tk2dUIItem>();
		itemUI.OnDown += OnDown;
		itemUI.OnRelease += OnRelease;
		b_buyButton.OnClick += OnBuy;
	}
	
	public void setCurrencyPack(StoreCurrencyPack currencyPack) {
		this.currencyPack = currencyPack;
		if (avatar != null) {
			avatar.SetSprite("avatar_currencypack_crystal");
		}
		if (text != null) {
			text.text = currencyPack.name;
			text.Commit();
		}
		if (t_buyButton != null) {
			t_buyButton.text = currencyPack.cost;
			t_buyButton.Commit();
		}
		if (s_buyButton != null) {
			s_buyButton.color = c_buyable;
		}
		if (t_packSize != null) {
			t_packSize.text = currencyPack.size + " Crystals";
			t_packSize.Commit();
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
		tooltipText = "^CFF914Dff" + currencyPack.name + "\n";
		tooltipText += "^CE0E0E0ff" + currencyPack.description + "\n";
	}
	
	void OnHold() {
		if (currencyPack != null) {
			setTooltipText();
			Tooltip.instance.setText(tooltipText);
			Tooltip.instance.showTooltip(transform.position);
		}
	}
	
	void OnBuy() {
		StoreInventory.BuyItem(currencyPack.itemID);
	}
}
