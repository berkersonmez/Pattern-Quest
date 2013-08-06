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
	public int currentMana=20;
	public int spellPower;
	public int level=1;
	public int spellPerTurn = 1;
	public bool isPlayer=false;
	public string spriteName;
	public string type;
	public List<string> spellNamesList = new List<string>();
	public List<Spell> spellList = new List<Spell>();
	Queue<ActiveSpell> nextTurnsActiveSpells = new Queue<ActiveSpell>();
	
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
	
	public virtual void decreaseHp(int amount) {
		currentHp -= amount;
		if(currentHp < 0)
			currentHp = 0;
		CombatTextController.instance.deployText(amount, (int)CombatTextController.Placement.CREATURE);
	}
	
	public void increaseMana(int amount) {
		currentMana += amount;
		if(currentMana > mana)
			currentMana = mana;
	}
	
	public void decreaseMana(int amount) {
		currentMana -= amount;
	}
	
	public void restoreHealthMana() {
		currentHp = hp;
		currentMana = mana;
	}
	
	// Activate spells one by one to put a small delay.
	// Return true if all spells in that turn finishes.
	public bool activateActiveSpell(ref Queue<ActiveSpell> activeSpells) {
		if (activeSpells.Count == 0) {
			activeSpells = new Queue<ActiveSpell>(nextTurnsActiveSpells);
			nextTurnsActiveSpells.Clear();
			return true;
		} else {
			ActiveSpell activeSpell = activeSpells.Dequeue();
			bool result = activeSpell.effect(this);
			if(result == true)
				nextTurnsActiveSpells.Enqueue(activeSpell);
			return false;
		}
	}
	
	public void play(Battle battle, ref Creature caster, ref Creature target){
		Spell spell = this.spellList[0];
		DungeonController.instance.battle.castSpell(spell);
		Debug.Log("oy oy oy");
	}
	
	public Creature (){
		
	}
	
	public Creature (string type, int level){
		//GET CREATURES FROM AN EXTERNAL FILE
	}
}


