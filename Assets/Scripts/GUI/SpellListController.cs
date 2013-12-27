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

	void Start () {
		instance = this;
		spellListScrollArea = transform.Find("SpellsList").gameObject;
		spellList = spellListScrollArea.transform.Find("Content").gameObject;
		comboListScrollArea = transform.Find("CombosList").gameObject;
		comboList = comboListScrollArea.transform.Find("Content").gameObject;

		refreshSpellList();
		refreshComboList();
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
		tk2dUIScrollableArea sa = spellListScrollArea.GetComponent<tk2dUIScrollableArea>();
		sa.ContentLength = sa.MeasureContentLength();
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
			holder.setSpell(spell);
			i++;
		}
		tk2dUIScrollableArea sa = comboListScrollArea.GetComponent<tk2dUIScrollableArea>();
		sa.ContentLength = sa.MeasureContentLength();
	}

	void Update() {
		// Disable colliders for spells in list that are hidden under combo list.
		foreach(Transform child in spellList.transform) {
			SpellHolder holder = child.GetComponent<SpellHolder>();
			if (child.transform.localPosition.y + spellList.transform.localPosition.y < -7f)
				holder.avatar.collider.enabled = false;
			else
				holder.avatar.collider.enabled = true;
		}
	}

}
