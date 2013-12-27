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
	public int spellPower;
	public int armor = 0;
	public int level=1;
	public int spellPerTurn = 1;
	public int criticalStrikeChance = 5;
	public bool isPlayer=false;
	public string spriteName;
	public string type;
	public int gold;
	public List<Item> droppedItems = new List<Item>();
	public List<string> spellNamesList = new List<string>();
	public List<Spell> spellList = new List<Spell>();
	public List<Spell> comboSpells = new List<Spell>();
	Queue<ActiveSpell> nextTurnsActiveSpells = new Queue<ActiveSpell>();
	
	public void setValues(){
		this.currentHp = this.hp;
		this.currentMana = this.mana;
		//Spell spell = new Spell(damage);
		//Debug.Log(spell.damage);
		//this.spellList.Add(spell);
	}
	
	public void increaseHp(int amount, string effectName) {
		currentHp += amount;
		if(currentHp > hp)
			currentHp = hp;
		CombatTextController.instance.deployText(effectName, amount, (int)CombatTextController.Placement.PLAYER);
	}
	
	public virtual void decreaseHp(Creature caster, int amount, string effectName) {
		int randomValue = Random.Range(1,100);
		if(randomValue <= caster.criticalStrikeChance){
			amount = amount * 2;
			effectName.ToUpper();
		}
		currentHp -= amount;
		if(currentHp < 0)
			currentHp = 0;
		CombatTextController.instance.deployText(effectName, amount, (int)CombatTextController.Placement.CREATURE);
	}

	public void increaseMana(int amount){
		currentMana += amount;
		if(currentMana > mana)
			currentMana = mana;
	}

	public void increaseMana(int amount, string effectName) {
		currentMana += amount;
		if(currentMana > mana)
			currentMana = mana;
		CombatTextController.instance.deployText(effectName, amount, (int)CombatTextController.Placement.CREATURE);
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

	public Spell getSpell(string name, int level){
		foreach(Spell spell in spellList){
			if(spell.name == name && spell.level == level)
				return spell;
		}
		return null;
	}
	
	// Activate spells one by one to put a small delay.
	// Return true if all spells in that turn finishes.
	public bool activateActiveSpell(Creature caster, ref Queue<ActiveSpell> activeSpells) {
		if (activeSpells.Count == 0) {
			activeSpells = new Queue<ActiveSpell>(nextTurnsActiveSpells);
			nextTurnsActiveSpells.Clear();
			return true;
		} else {
			ActiveSpell activeSpell = activeSpells.Dequeue();
			bool result = activeSpell.effect(caster, this);
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


