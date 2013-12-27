using UnityEngine;
using System.Collections;

// Spell item slot.
public class SpellHolder : MonoBehaviour {
	
	public Spell spell;
	public tk2dSprite avatar; // Assign using inspector
	public tk2dTextMesh text; // Assign using inspector
	private tk2dUIItem itemUI;
	
	void Start () {
		itemUI = GetComponent<tk2dUIItem>();
		itemUI.OnDown += OnDown;
		itemUI.OnRelease += OnRelease;
	}
	
	// Sets spell of the item slot.
	public void setSpell(Spell spell) {
		this.spell = spell;
		if (avatar != null) {
			avatar.SetSprite(spell.spriteName);
		}
		if (text != null) {
			text.text = spell.name;
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
		if (spell != null) {
			// Set and show tooltip
			spell.setTooltipText();
			Tooltip.instance.setText(spell.tooltipText);
			Tooltip.instance.showTooltip(transform.position);
		}
	}
}
