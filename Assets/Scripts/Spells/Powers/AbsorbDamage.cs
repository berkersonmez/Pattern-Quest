using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class AbsorbDamage : Power {
	
	public AbsorbDamage(){
		name = "Absorb Damage";
		idName = "AbsorbDamage";
		totalAmount = 0;
		currentAmount = 0;
		percent = 0;
		effectOn = "enemy";
		totalCoolDown = 0;
		mana = 0;
		type = "fire";
		active = true;
		level = 1;
		spriteName = "avatar_spell_5";
	}

	public override bool react(Spell castedSpell, string effectOn, ref string combatTextExtra){
		if(effectOn != this.effectOn)
			return false;
		if(castedSpell.type != this.type)
			if(this.type != "all")
				return false;
		if(currentAmount > 0){
			if(currentAmount <= castedSpell.damage){
				castedSpell.damage -= currentAmount;
				combatTextExtra += "(" + currentAmount + ")";
				currentAmount = 0;
				//DungeonController.instance.battle.creature.decreaseHp(DungeonController.instance.battle.player,amount,"absorb");
			}
			else {
				currentAmount -= castedSpell.damage;
				combatTextExtra += "(" + castedSpell.damage + ")";
				castedSpell.damage = 0;
			}
			if(currentAmount == 0){
				this.active = false;
				this.currentCoolDown = this.totalCoolDown;
				this.currentAmount = this.totalAmount;
				if(this.justForThisBattle == true){
					//In this bracket "Absorb Power" power should be removed from the power list of the creature\\
					Debug.Log("Absorb Bitti");
				}
			}
			return false;
		}
		if(percent > 0) {
			int tempDamage = castedSpell.damage;
			castedSpell.damage = (int) ((float)(castedSpell.damage) * (float)percent / 100);
			combatTextExtra += "(" + (tempDamage - castedSpell.damage) + ")";
			this.active = false;
			this.currentCoolDown = this.totalCoolDown;
		}
		return false;
	}

	public override void setTooltipText() {
		// Tooltip text for spell.
		// Coloring: ^CRRGGBBAA*text*
		
		tooltipText = "^C7ED8E6ff" + name + "\n";
		tooltipText += "^CffffffffType: " + type + "\n\n";

		tooltipText += "Absorbs " + totalAmount + " damage.\n";
	}
	
}