using UnityEngine;
using System.Collections;

public class Item {
	
	public string name;
	public int damage;
	public int spellPower;
	public string rarity;
	public string type;
	public int armor;
	public int hp;
	public int mana;
	public int manaRegen;
	public int level;
	public float dropChance;
	public string spriteName;

	public string tooltipText;
	
	public Item(){
		damage = 0;
		spellPower = 0;
		rarity = "common";
		armor = 0;
		hp = 0;
		mana = 0;
		level = 1;		
	}
	
	//Fixes empty and wrong values
	public void setValues(){
		if(this.name.Contains("common"))
			this.name = "Level " + this.level + " " + this.type;
		setTooltipText();
	}

	public void setTooltipText() {
		tooltipText = "^C00FF3Cff" + name + "\n";
		tooltipText += "^CffffffffType: " + type + "\n";
		if (damage != 0) tooltipText += "Damage: " + damage + "\n";
		if (armor != 0) tooltipText += "Armor: " + armor + "\n";
		if (hp != 0) tooltipText += "HP: " + hp + "\n";
		if (mana != 0) tooltipText += "Mana: " + mana + "\n";
		if (manaRegen != 0) tooltipText += "Mana Regen: " + mana + "\n";
		if (level != 0) tooltipText += "Level: " + level + "\n";
	}
}
