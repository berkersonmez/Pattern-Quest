using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Player : Creature {

	public int xp;
	public int crystal;
	public float xpBonus;
	public float goldBonus;
	public int tp;
	public Item redStone;
	public Item blueStone;
	public Item greenStone;
	public List<Item> inventory = new List<Item>();
	public List<KeyValuePair<int, int>> talents = new List<KeyValuePair<int, int>>();

	public Dictionary<int,Dictionary<string,int>> questSlayCounter = new Dictionary<int, Dictionary<string,int>>();
	public List<int> completedQuests = new List<int>();
	public List<int> answeredQuestions = new List<int>();

	public Player(){
		isPlayer = true;
		spellPerTurn = 2;
		spriteName = "avatar_player_2";
	}
	
	public override void decreaseHp(Creature caster, int amount) {
		currentHp -= amount;
		if(currentHp < 0)
			currentHp = 0;
	}

	public override void increaseMana(int amount) {
		currentMana += amount;
		if(currentMana > Mana)
			currentMana = Mana;
	}

	public Item wearItem(Item item) {
		Item swapped = null;
		if (item.type == "blue") {
			swapped = this.blueStone;
			this.blueStone = item;
		} else if (item.type == "green") {
			swapped = this.greenStone;
			this.greenStone = item;
		} else if (item.type == "red") {
			swapped = this.redStone;
			this.redStone = item;
		}
		this.inventory.Remove(item);
		if (swapped != null && swapped.type != null) {
			this.inventory.Add(swapped);
			this.unwearItem(swapped);
		}

		spellPower += item.damage;
		hp += item.hp;
		mana += item.mana;
		armor += item.armor;
		manaRegen += item.manaRegen;
		hpRegen += item.hpRegen;
		return swapped;
	}
	
	// Defines consuming behaviors
	public void consumeItem(Item item) {
		if (item.type != "consumable") return;
		if (item.effect.Contains("talent_reset")) {
			TownController.instance.resetTalents();
		}
		this.inventory.Remove(item);
	}

	public void slainCreature(string creatureName) {
		// for quests (may count kills in this for stats)
		foreach(KeyValuePair<int, Dictionary<string, int>> pair in questSlayCounter) {
			if (pair.Value.ContainsKey(creatureName)) {
				pair.Value[creatureName]++;
			}
		}
	}

	public void unwearItem(Item item) {
		spellPower -= item.damage;
		hp -= item.hp;
		mana -= item.mana;
		armor -= item.armor;
		manaRegen -= item.manaRegen;
		hpRegen -= item.hpRegen;
	}

	public int getTalentRank(int talentID) {
		foreach (KeyValuePair<int, int> pair in talents) {
			if (pair.Key == talentID) {
				return pair.Value;
			}
		}
		return 0;
	}

	public void setTalentRank(int talentID, int rank) {
		int foundIndex = -1;
		for (int i = 0; i < talents.Count; i++) {
			if (talents[i].Key == talentID) {
				foundIndex = i;
			}
		}
		if (foundIndex != -1) {
			talents[foundIndex] = new KeyValuePair<int, int>(talentID, rank);
		} else {
			talents.Add(new KeyValuePair<int, int>(talentID, rank));
		}
	}

	public Spell getSpell(string name, string type) {
		if (type == "Spell") {
			foreach (Spell spell in spellList) {
				if (spell.name == name) return spell;
			}
		} else if (type == "Combo") {
			foreach (Spell spell in comboSpells) {
				if (spell.name == name) return spell;
			}
		} else if (type == "Power") {
			foreach (Spell spell in powers) {
				Debug.Log(spell.name);
				Debug.Log(name);
				if (spell.name == name) return spell;
			}
		}
		return null;
	}

	public void unlearnSpell(string name, string type) {
		if (type == "Spell") {
			for (int i = 0; i < spellList.Count; i++) {
				if (spellList[i].name == name) spellList.RemoveAt(i);
			}
		} else if (type == "Combo") {
			for (int i = 0; i < comboSpells.Count; i++) {
				if (comboSpells[i].name == name) comboSpells.RemoveAt(i);
			}
		} else if (type == "Power") {
			for (int i = 0; i < powers.Count; i++) {
				if (powers[i].name == name) powers.RemoveAt(i);
			}
		}
	}

	public bool isCompletedQuest(int questID) {
		return (completedQuests.IndexOf(questID) != -1);
	}
}
