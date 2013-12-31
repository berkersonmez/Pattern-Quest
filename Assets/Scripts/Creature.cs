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
	public int hpRegen = 0;
	public int currentMana=20;
	public int spellPower=0;
	public int armor = 0;
	public int level=1;
	public int spellPerTurn = 1;
	public int criticalStrikeChance = 5;
	public int brain=0;		//Test variable
	public bool isPlayer=false;
	public string spriteName;
	public string type;
	public int gold;
	public List<Item> droppedItems = new List<Item>();
	public List<string> spellNamesList = new List<string>();
	public List<Spell> spellList = new List<Spell>();
	public List<Spell> comboSpells = new List<Spell>();
	public List<Power> powers = new List<Power>();
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
	
	public virtual void decreaseHp(Creature caster, int amount) {
		currentHp -= amount;
		if(currentHp < 0)
			currentHp = 0;
	}

	public virtual void increaseMana(int amount){
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

	public List<ComboSpell> getComboSpells(Queue<Spell> castedSpells){
		List<ComboSpell> currentComboSpells = new List<ComboSpell>();
		foreach(ComboSpell comboSpell in comboSpells){
			if(comboSpell.requires(castedSpells))
				currentComboSpells.Add(comboSpell);
		}
		if(currentComboSpells.Count >0)
			return currentComboSpells;
		else
			return null;
	}

	//False means spell is canceled or destroyed somehow
	public bool react(Spell castedSpell, string effectOn, ref string combatTextExtra){
		foreach(Power power in powers){
			if(power.active){
				bool result = power.react(castedSpell, effectOn, ref combatTextExtra);
				if(result == true){
					return true;
				}
			}
		}
		return false;
	}

	public void updateCooldowns(){
		foreach(Power power in powers){
			power.update();
		}
		foreach(Spell spell in spellList){
			spell.update();
		}
	}

	public void resetCooldowns(){
		foreach(Power power in powers){
			power.resetCooldown();
		}
		foreach(Spell spell in spellList){
			spell.resetCooldown();
		}
	}

	public Spell getSpell(string name, int level){
		foreach(Spell spell in spellList){
			if(spell.name == name && spell.level == level)
				return spell;
		}
		return null;
	}
	
	// Activate spells one by one to put a small delay.
	// Return true if all spells in that turn finishes.
	public bool activateActiveSpell(Battle battle, Creature caster, ref Queue<ActiveSpell> activeSpells) {
		if (activeSpells.Count == 0) {
			activeSpells = new Queue<ActiveSpell>(nextTurnsActiveSpells);
			nextTurnsActiveSpells.Clear();
			return true;
		} else {
			ActiveSpell activeSpell = activeSpells.Dequeue();
			bool result = activeSpell.effect(battle, caster, this);
			if(result == true)
				nextTurnsActiveSpells.Enqueue(activeSpell);
			return false;
		}
	}
	
	public void play(Battle battle, ref Creature caster, ref Creature target){
		Spell spell = this.spellList[0];
		spell.owner = this;
		battle.castSpell(spell);
		/*Spell poi = new Poison();
		poi.owner = this;
		this.spellList.Add(poi);
		if(brain==0){
			DungeonController.instance.battle.castSpell(poi);
			brain = 1;
		}else
			battle.castSpell(spell);*/
		Debug.Log("oy oy oy");
	}
	
	public Creature (){
		
	}
	
	public Creature (string type, int level){
		//GET CREATURES FROM AN EXTERNAL FILE
	}
}


