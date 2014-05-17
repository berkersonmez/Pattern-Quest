using UnityEngine;
using System.Collections;
using Soomla;

// Item slots in loot window and inventory.
public class StoreItemHolder : MonoBehaviour {
	
	public StoreGood good;
	public tk2dSprite avatar; // Assign using inspector
	public tk2dTextMesh text; // Assign using inspector
	public tk2dTextMesh t_buyButton; // Assign using inspector
	public tk2dSprite s_buyButton; // Assign using inspector
	public tk2dUIItem b_buyButton; // Assign using inspector
	private tk2dUIItem itemUI;
	
	void Start () {
		itemUI = GetComponent<tk2dUIItem>();
		itemUI.OnDown += OnDown;
		itemUI.OnRelease += OnRelease;
		b_buyButton.OnClick += OnBuy;
	}
	
	// Sets good of the good slot.
	public void setGood(StoreGood good) {
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
		if (s_buyButton != null) {
			s_buyButton.SetSprite("avatar_currency_" + good.costType);
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
	
	void OnHold() {
		if (good != null) {
			Tooltip.instance.setText(good.description);
			Tooltip.instance.showTooltip(transform.position);
		}
	}
	
	void OnBuy() {
		
	}
}
