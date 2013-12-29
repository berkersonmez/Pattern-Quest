using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpellListController : MonoBehaviour {
	public static SpellListController instance;

	public GameObject prefabSpell;
	public GameObject spellListScrollArea;
	public GameObject spellList;
	public GameObject comboListScrollArea;
	public GameObject comboList;
	public GameObject powerListScrollArea;
	public GameObject powerList;

	void Start () {
		instance = this;
		spellListScrollArea = transform.Find("SpellsList").gameObject;
		spellList = spellListScrollArea.transform.Find("Content").gameObject;
		comboListScrollArea = transform.Find("CombosList").gameObject;
		comboList = comboListScrollArea.transform.Find("Content").gameObject;
		powerListScrollArea = GameObject.Find("PowersList");
		powerList = powerListScrollArea.transform.Find("Content").gameObject;

		refreshSpellList();
		refreshComboList();
		refreshPowerList();
	}

	public void refreshSpellList() {
		foreach(Transform child in spellList.transform) {
			Destroy(child.gameObject);
		}
		List<Spell> inv = GameSaveController.instance.player.spellList;
		int i = 0;
		foreach (Spell spell in inv) {
			GameObject spellEntry = Instantiate(prefabSpell) as GameObject;
			spellEntry.transform.parent = spellList.transform;
			spellEntry.transform.localPosition = new Vector3(-8.7f, 8.3f - (4.3f * i), 0f);
			SpellHolder holder = spellEntry.GetComponent<SpellHolder>();
			holder.setSpell(spell);
			i++;
		}
		if (i != 0) {
			tk2dUIScrollableArea sa = spellListScrollArea.GetComponent<tk2dUIScrollableArea>();
			sa.ContentLength = sa.MeasureContentLength();
		}
	}

	public void refreshComboList() {
		foreach(Transform child in comboList.transform) {
			Destroy(child.gameObject);
		}
		List<Spell> inv = GameSaveController.instance.player.comboSpells;
		int i = 0;
		foreach (Spell spell in inv) {
			GameObject spellEntry = Instantiate(prefabSpell) as GameObject;
			spellEntry.transform.parent = comboList.transform;
			spellEntry.transform.localPosition = new Vector3(-8.7f, 8.3f - (4.3f * i), 0f);
			SpellHolder holder = spellEntry.GetComponent<SpellHolder>();
			spellEntry.transform.Find("ShapeButton").gameObject.SetActive(false);
			holder.setSpell(spell);
			i++;
		}
		if (i != 0) {
			tk2dUIScrollableArea sa = comboListScrollArea.GetComponent<tk2dUIScrollableArea>();
			sa.ContentLength = sa.MeasureContentLength();
		}
	}

	public void refreshPowerList() {
		foreach(Transform child in powerList.transform) {
			Destroy(child.gameObject);
		}
		List<Power> inv = GameSaveController.instance.player.powers;
		int i = 0;
		foreach (Spell spell in inv) {
			GameObject spellEntry = Instantiate(prefabSpell) as GameObject;
			spellEntry.transform.parent = powerList.transform;
			spellEntry.transform.localPosition = new Vector3(-8.7f, 8.3f - (4.3f * i), 0f);
			SpellHolder holder = spellEntry.GetComponent<SpellHolder>();
			spellEntry.transform.Find("ShapeButton").gameObject.SetActive(false);
			holder.setSpell(spell);
			i++;
		}
		if (i != 0) {
			tk2dUIScrollableArea sa = powerListScrollArea.GetComponent<tk2dUIScrollableArea>();
			sa.ContentLength = sa.MeasureContentLength();
		}
	}

	void Update() {
		// Disable colliders for spells in list that are hidden under combo list.
		foreach(Transform child in spellList.transform) {
			SpellHolder holder = child.GetComponent<SpellHolder>();
			if (child.transform.localPosition.y + spellList.transform.localPosition.y < -7f) {
				holder.avatar.collider.enabled = false;
			} else {
				holder.avatar.collider.enabled = true;
			}

		}
	}

}
