using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : Creature {

	public int xp;
	public Item redStone;
	public Item blueStone;
	public Item greenStone;
	public List<Item> inventory = new List<Item>();

	public Dictionary<int,Dictionary<string,int>> questSlayCounter = new Dictionary<int, Dictionary<string,int>>();
	
	public Player(){
		isPlayer = true;
		spellPerTurn = 2;
		spriteName = "avatar_player_1";
	}
	
	public override void decreaseHp(Creature caster, int amount) {
		currentHp -= amount;
		if(currentHp < 0)
			currentHp = 0;
	}

	public override void increaseMana(int amount) {
		currentMana += amount;
		if(currentMana > mana)
			currentMana = mana;
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
}
