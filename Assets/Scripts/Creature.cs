using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Creature
{
	
	public string name;
	public int damage=4;
	public int hp=25;
	public int currentHp=25;
	public int mana=20;
	public int manaRegen=2;
	public int manaRegenPercentBetweenBattles=0;
	public int hpRegen = 0;
	public int currentMana=20;
	public int spellPower=0;
	public int armor = 0;
	public int level=1;
	public int spellPerTurn = 1;
	public int criticalStrikeChance = 30;
	public int critDamageIncrease = 1; // Shows how many nodes will be "crit point". Each increases damage 10%
	public int critDamageBonus = 0;
	public int brain=0;		//Test variable
	public bool isPlayer=false;
	public string spriteName;
	public string type;
	public int gold;
	public List<Item> droppedItems = new List<Item>();
	public List<string> spellNamesList = new List<string>();
	public List<Spell> spellList = new List<Spell>();
	public List<Spell> eatenSpells = new List<Spell>();
	public List<int> ai_spell_points = new List<int>();
	public List<Spell> comboSpells = new List<Spell>();
	public List<Power> powers = new List<Power>();
	Queue<ActiveSpell> nextTurnsActiveSpells = new Queue<ActiveSpell>();

	// Percentage value increases (0.1 = 10% increase)
	public float damagePercent = 0;
	public float hpPercent = 0;
	public float manaPercent = 0;
	public float manaRegenPercent = 0;
	public int hpRegenPercent = 0;
	public float spellPowerPercent = 0;
	public float armorPercent = 0;

	// Getters apply percentages to find real values
	public int Damage {get {return (int)(damage + damage * damagePercent);}}
	public int Hp {get {return (int)(hp + hp * hpPercent);}}
	public int Mana {get {return (int)(mana + mana * manaPercent);}}
	public int ManaRegen {get {return (int)(manaRegen + manaRegen * manaRegenPercent);}}
	public int HpRegen {get {return (int)(hpRegen + hpRegen * hpRegenPercent);}}
	public int SpellPower {get {return (int)(spellPower + spellPower * spellPowerPercent);}}
	public int Armor {get {return (int)(armor + armor * armorPercent);}}

	public void setValues(){
		this.currentHp = this.Hp;
		this.currentMana = this.Mana;
		//Spell spell = new Spell(damage);
		//Debug.Log(spell.damage);
		//this.spellList.Add(spell);
	}
	
	public void increaseHp(int amount) {
		currentHp += amount;
		if(currentHp > Hp)
			currentHp = Hp;
	}

	public void changeHpPercent(int percent){
		currentHp += (int) ((float)percent/100 * hp);
		if(currentHp < 0)
			currentHp = 0;
		if(currentHp > Hp)
			currentHp = Hp;
	}

	public void changeManaPercent(int percent){
		currentMana += (int) ((float)percent/100 * mana);
		if(currentMana < 0)
			currentMana = 0;
		if(currentMana > mana)
			currentMana = mana;
	}
	
	public virtual void decreaseHp(Creature caster, int amount) {
		currentHp -= amount;
		if(currentHp < 0)
			currentHp = 0;
	}

	public virtual void increaseMana(int amount){
		currentMana += amount;
		if(currentMana > Mana)
			currentMana = Mana;
	}
	
	public void decreaseMana(int amount) {
		currentMana -= amount;
	}
	
	public void restoreHealthMana() {
		currentHp = Hp;
		currentMana = Mana;
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

	public void checkPowers(){
		Power toDelete = new Power();
		foreach(Power power in powers){
			if(power.justForThisBattle == true && power.active == false){
				toDelete = power;
				break;
			}
		}
		powers.Remove(toDelete);
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
		eatenSpells.Clear();
		for(int i=0; i<ai_spell_points.Count ; i++)
			ai_spell_points[i]=0;
	}

	public void finishBattle(){
		foreach(Power power in powers){
			if(power.justForThisBattle == true){
				powers.Remove(power);
				return;
			}
		}
	}

	public Spell getSpell(string name, int level){
		foreach(Spell spell in spellList){
			if(spell.idName == name && spell.level == level)
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

	public void calculatePointsOfSpells(){
		for(int i=0; i<ai_spell_points.Count ; i++)
			ai_spell_points[i]=0;
		//Calculate Absorb
		bool isAbsorbActive = false;
		Spell lastEatenSpell = this.eatenSpells[eatenSpells.Count - 1];
		Spell secondEatenSpell = this.eatenSpells[eatenSpells.Count - 2];
		if(lastEatenSpell.type == secondEatenSpell.type){
			if(lastEatenSpell.damage + lastEatenSpell.damageOverTime*lastEatenSpell.turn +
			   secondEatenSpell.damage + secondEatenSpell.damageOverTime*secondEatenSpell.turn > hp*10.0/100){
				for(int i=0; i<spellList.Count ; i++){
					if(spellList[i].name == "Absorb Damage" && spellList[i].type == lastEatenSpell.type){
						for(int j=0; j<this.powers.Count ; j++){
							if(powers[j].name == "Absorb Damage"){
								Debug.Log("HALA ABSORB VAR");
								this.ai_spell_points[i] = 0;
								isAbsorbActive = true;
							}	
						}
						if(isAbsorbActive == false){
							this.ai_spell_points[i] += this.spellPower*100;
							Debug.Log("ABSORB GELIYOR");
							//Debug.Log( spellList[i].name + " " + ai_spell_points[i] + " puan aldi");
							//Debug.Log(spellList[i].name + spellList[i].type + spellList[i].damage);
						}
					}
				}
			}
		}
		//Calculate Damage Spells
		for(int i=0; i<spellList.Count ; i++){
			if(spellList[i].currentCoolDown > 0){
				Debug.Log(spellList[i].name + " su anda cooldown'da");
				this.ai_spell_points[i] = 0;
				continue;
			}
			int totalDamage = spellList[i].damage + spellList[i].damageOverTime*spellList[i].turn;
			int totalHeal = spellList[i].heal + spellList[i].healOverTime*spellList[i].turn;
			if(totalDamage > 0){
				this.ai_spell_points[i] += totalDamage + this.spellPower;
			}
			if(totalHeal > 0){
				this.ai_spell_points[i] += (int) ((float)hp/currentHp * (totalHeal + this.spellPower) / 1.5);
			}
			Debug.Log( spellList[i].name + " " + ai_spell_points[i] + " puan aldi");
		}
	}

	public void play(Battle battle, ref Creature caster, ref Creature target){
		Debug.Log("================CREATURE PLAYING===================");
		calculatePointsOfSpells();

		int bestSpell = 0;
		for(int i=0; i<ai_spell_points.Count; i++){
			if(ai_spell_points[bestSpell] < ai_spell_points[i])
				bestSpell = i;
		}
		battle.castSpell(spellList[bestSpell]);

	}



	public Creature (){
		
	}
	
	public Creature (string type, int level){
		//GET CREATURES FROM AN EXTERNAL FILE
	}

	public bool learnSpell(Spell spell){
		spell.owner = this;
		this.spellList.Add(spell);
		this.ai_spell_points.Add(0);  //simply to increase size of the ai spells by 1 too
		return true;
	}
}


