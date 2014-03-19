using UnityEngine;
using System.Collections;
using System.Reflection;

[System.Serializable]
public class TalentRank {

	public string description;

	public string[] changeVariableNames; // Ex: damage, spellPower, armor
	public int[] changeVariableValues;

	public string[] changeVariableNamesFloat; // Ex damagePercent, spellPowerPercent
	public float[] changeVariableValuesFloat;

	public string[] addSpell; // Spell names to add
	public string[] addCombo;
	public string[] addPower;
	public string[] changeSpell; // Ex: "Poison:dot=4,turn=5"
	public string[] changeCombo;
	public string[] changePower;

	public void activate() {
		Player player = GameSaveController.instance.getPlayer();
		for (int i = 0; i < changeVariableNames.Length; i++) {
			string varName = changeVariableNames[i];
			int varValue = changeVariableValues[i];
			FieldInfo info = player.GetType().GetField(varName);
			info.SetValue(player, (int)info.GetValue(player) + varValue);
		}
		for (int i = 0; i < changeVariableNamesFloat.Length; i++) {
			string varName = changeVariableNamesFloat[i];
			float varValue = changeVariableValuesFloat[i];
			FieldInfo info = player.GetType().GetField(varName);
			info.SetValue(player, (float)info.GetValue(player) + varValue);
		}
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
		for (int i = 0; i < changeVariableNames.Length; i++) {
			string varName = changeVariableNames[i];
			int varValue = changeVariableValues[i];
			FieldInfo info = player.GetType().GetField(varName);
			info.SetValue(player, (int)info.GetValue(player) - varValue);
		}
		for (int i = 0; i < changeVariableNamesFloat.Length; i++) {
			string varName = changeVariableNamesFloat[i];
			float varValue = changeVariableValuesFloat[i];
			FieldInfo info = player.GetType().GetField(varName);
			info.SetValue(player, (float)info.GetValue(player) - varValue);
		}
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
		Debug.Log(changeString);
		string[] values = changeString.Split(':');
		string spellName = values[0];
		Spell spell = player.getSpell(spellName, type);
		Debug.Log(spell.name);
		if (spell != null) {
			spell.change(values[1]);
		}
	}

	public void applyUndoSpell(string changeString, string type) {
		Player player = GameSaveController.instance.getPlayer();
		string[] values = changeString.Split(':');
		string spellName = values[0];
		Spell spell = player.getSpell(spellName, type);
		if (spell != null) {
			spell.unchange(values[1]);
		}
	}
}
