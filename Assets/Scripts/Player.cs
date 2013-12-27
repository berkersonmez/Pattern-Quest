using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : Creature {

	public int xp;
	public Item redStone;
	public Item blueStone;
	public Item greenStone;
	public List<Item> inventory = new List<Item>();
	
	public Player(){
		isPlayer = true;
		spellPerTurn = 2;
		spriteName = "avatar_player_1";
	}
	
	public override void decreaseHp(Creature caster, int amount, string effectName) {
		int randomValue = Random.Range(1,100);
		if(randomValue <= caster.criticalStrikeChance){
			amount = amount * 2;
			effectName.ToUpper();
		}
		currentHp -= amount;
		if(currentHp < 0)
			currentHp = 0;
		CombatTextController.instance.deployText(effectName, amount, (int)CombatTextController.Placement.PLAYER);
	}

	public void increaseMana(int amount, string effectName) {
		currentMana += amount;
		if(currentMana > mana)
			currentMana = mana;
		CombatTextController.instance.deployText(effectName, amount, (int)CombatTextController.Placement.PLAYER);
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

	public void unwearItem(Item item) {
		spellPower -= item.damage;
		hp -= item.hp;
		mana -= item.mana;
		armor -= item.armor;
		manaRegen -= item.manaRegen;
		hpRegen -= item.hpRegen;
	}
}
