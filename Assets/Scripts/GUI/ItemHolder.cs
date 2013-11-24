using UnityEngine;
using System.Collections;

public class ItemHolder : MonoBehaviour {

	public Item item;
	public tk2dSprite avatar;
	public tk2dTextMesh text;
	private tk2dUIItem itemUI;

	void Start () {
		itemUI = GetComponent<tk2dUIItem>();
		itemUI.OnDown += OnDown;
		itemUI.OnRelease += OnRelease;
	}

	public void setItem(Item item) {
		this.item = item;
		if (avatar != null) {
			avatar.SetSprite(item.spriteName);
		}
		if (text != null) {
			text.text = item.name;
			text.Commit();
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
		item.setTooltipText();
		Tooltip.instance.setText(item.tooltipText);
		Tooltip.instance.showTooltip(transform.position);
	}
}
