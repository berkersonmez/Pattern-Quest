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
		if(this.name == null)
			this.name = "level " + this.level + " " + this.type;
		if(this.type != "weapon")
			this.damage = 0;
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
