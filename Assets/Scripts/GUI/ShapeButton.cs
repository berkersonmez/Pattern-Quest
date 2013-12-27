using UnityEngine;
using System.Collections;

public class ShapeButton : MonoBehaviour {

	private Spell spell;
	private tk2dUIItem itemUI;

	void Start () {
		SpellHolder holder = transform.parent.GetComponent<SpellHolder>();
		spell = holder.spell;
		itemUI = GetComponent<tk2dUIItem>();
		itemUI.OnClick += OnClick;
	}

	void OnClick() {
		TownMenu.instance.drawPatternWindow();
		PatternControllerDraw.instance.cleanPattern();
		PatternControllerDraw.instance.setPattern(spell.shape);
		PatternControllerDraw.instance.playDraw();
	}
}
