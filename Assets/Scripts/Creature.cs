using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Creature
{
	
	public string name;
	public int damage=4;
	public int hp=25;
	public int currentHp=25;
	public int mana=20;
	public int manaRegen=2;
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
		//Spell spell = new Spell(damage);
		//Debug.Log(spell.damage);
		//this.spellList.Add(spell);
	}
	
	public void increaseHp(int amount) {
		currentHp += amount;
		if(currentHp > hp)
			currentHp = hp;
	}
	
	public void decreaseHp(int amount) {
		currentHp -= amount;
		CombatTextController.instance.deployText(amount);
	}
	
	public void increaseMana(int amount) {
		currentMana += amount;
		if(currentMana > mana)
			currentMana = mana;
	}
	
	public void decreaseMana(int amount) {
		currentMana -= amount;
	}
	
	public void activateActiveSpells(ref List<ActiveSpell> activeSpells){
		List<ActiveSpell> nextTurnsActiveSpells = new List<ActiveSpell>();
		foreach (ActiveSpell activeSpell in activeSpells){
			//.effect() return false if the remaining turn of the active spell is 0, otherwise true
			bool result = activeSpell.effect(this);
			if(result == true)
				nextTurnsActiveSpells.Add(activeSpell);
		}
		activeSpells.Clear();
		activeSpells = nextTurnsActiveSpells;
	}
	
	public void play(Battle battle, ref Creature caster, ref Creature target){
		Spell spell = this.spellList[0];
		DungeonController.instance.battle.castSpell(spell);
	}
	
	public Creature (){
		
	}
	
	public Creature (string type, int level){
		//GET CREATURES FROM AN EXTERNAL FILE
	}
}


