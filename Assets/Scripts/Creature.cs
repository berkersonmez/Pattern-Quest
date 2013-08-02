using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Creature
{
	
	public string name;
	public int hp=25;
	public int currentHp=25;
	public int mana;
	public int manaRegen;
	public int currentMana;
	public int spellPower;
	public int level=1;
	public int spellPerTurn = 1;
	public bool isPlayer=false;
	public string type;
	public List<string> spellNamesList = new List<string>();
	public List<Spell> spellList = new List<Spell>();
	
	public void setValues(){
		this.currentHp = this.hp;
		this.currentMana = this.mana;
	}
	
	public void increaseHp(int amount) {
		currentHp += amount;
	}
	
	public void decreaseHp(int amount) {
		currentHp -= amount;
		CombatTextController.instance.deployText(amount);
	}
	
	public void increaseMana(int amount) {
		currentMana += amount;
	}
	
	public void decreaseMana(int amount) {
		currentMana -= amount;
	}
	
	public Creature (){
		
	}
	
	public Creature (string type, int level){
		//GET CREATURES FROM AN EXTERNAL FILE
	}
}


