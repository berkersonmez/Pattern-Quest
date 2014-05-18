using UnityEngine;
using System.Collections;

// Item slots in loot window and inventory.
public class ItemHolder : MonoBehaviour {

	public Item item;
	public tk2dSprite avatar; // Assign using inspector
	public tk2dTextMesh text; // Assign using inspector
	public tk2dTextMesh t_wearButton; // Assign using inspector
	private tk2dUIItem itemUI;

	void Start () {
		itemUI = GetComponent<tk2dUIItem>();
		itemUI.OnDown += OnDown;
		itemUI.OnRelease += OnRelease;
	}

	// Sets item of the item slot.
	public void setItem(Item item) {
		this.item = item;
		if (avatar != null) {
			avatar.SetSprite(item.spriteName);
		}
		if (text != null) {
			text.text = item.name;
			text.Commit();
		}
		if (t_wearButton != null) {
			if (item.type == "consumable") {
				t_wearButton.text = "Use";
				t_wearButton.Commit();
			} else {
				t_wearButton.text = "Wear";
				t_wearButton.Commit();
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

	void OnHold() {
		if (item != null && item.type != "") {
			// Set and show tooltip
			item.setTooltipText();
			Tooltip.instance.setText(item.tooltipText);
			Tooltip.instance.showTooltip(transform.position);
		}
	}
}
