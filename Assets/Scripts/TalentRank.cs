using UnityEngine;
using System.Collections;

[System.Serializable]
public class TalentRank {

	public string description;

	public string[] addSpell; // Spell names to add
	public string[] addCombo;
	public string[] addPower;
	public string[] changeSpell; // Ex: "Poison:dot=4,turn=5"
	public string[] changeCombo;
	public string[] changePower;

	public void activate() {
		Player player = GameSaveController.instance.getPlayer();
		foreach (string addName in addSpell) {
			player.spellList.Add(Globals.instance.getSpell(addName, player));
		}
		foreach (string addName in addCombo) {
			player.comboSpells.Add(Globals.instance.getSpell(addName, player));
		}
		foreach (string addName in addPower) {
			player.powers.Add(Globals.instance.getPower(addName, player));
		}
		foreach (string changeString in changeSpell) {
			applyChangeSpell(changeString, "Spell");
		}
		foreach (string changeString in changeCombo) {
			applyChangeSpell(changeString, "Combo");
		}
		foreach (string changeString in changePower) {
			applyChangeSpell(changeString, "Power");
		}
	}

	public void deactivate() {
		Player player = GameSaveController.instance.getPlayer();
		foreach (string addName in addSpell) {
			player.unlearnSpell(addName, "Spell");
		}
		foreach (string addName in addCombo) {
			player.unlearnSpell(addName, "Combo");
		}
		foreach (string addName in addPower) {
			player.unlearnSpell(addName, "Power");
		}
		foreach (string changeString in changeSpell) {
			applyUndoSpell(changeString, "Spell");
		}
		foreach (string changeString in changeCombo) {
			applyUndoSpell(changeString, "Combo");
		}
		foreach (string changeString in changePower) {
			applyUndoSpell(changeString, "Power");
		}
	}

	public void applyChangeSpell(string changeString, string type) {
		Player player = GameSaveController.instance.getPlayer();
		string[] values = changeString.Split(':');
		string spellName = values[0];
		Spell spell = player.getSpell(spellName, type);
		if (spell != null) {
			//spell.change(values[1]);
		}
	}

	public void applyUndoSpell(string changeString, string type) {
		Player player = GameSaveController.instance.getPlayer();
		string[] values = changeString.Split(':');
		string spellName = values[0];
		Spell spell = player.getSpell(spellName, type);
		if (spell != null) {
			//spell.unchange(values[1]);
		}
	}
}
