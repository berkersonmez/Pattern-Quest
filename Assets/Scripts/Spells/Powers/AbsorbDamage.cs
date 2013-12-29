using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AbsorbDamage : Power {
	
	public AbsorbDamage(){
		name = "AbsorbDamage";
		amount = 5;
		percent = 0;
		effectOn = "enemy";
		mana = 0;
		type = "fire";
		active = true;
		level = 1;
	}

	public override bool react(Spell castedSpell, string effectOn, ref string combatTextExtra){
		if(effectOn != this.effectOn)
			return false;
		if(amount > 0){
			if(amount <= castedSpell.damage){
				castedSpell.damage -= amount;
				combatTextExtra += "(Absorbed: " + amount + ")";
				amount = 0;
				//DungeonController.instance.battle.creature.decreaseHp(DungeonController.instance.battle.player,amount,"absorb");
			}
			else {
				amount -= castedSpell.damage;
				combatTextExtra += "(Absorbed: " + castedSpell.damage + ")";
				castedSpell.damage = 0;
			}
			if(amount == 0)
				this.active = false;
			return false;
		}
		if(percent > 0) {
			int tempDamage = castedSpell.damage;
			castedSpell.damage = castedSpell.damage * (percent / 100);
			combatTextExtra += "(Absorbed: " + (tempDamage - castedSpell.damage) + ")";
		}
		return false;
	}

	public override void setTooltipText() {
		// Tooltip text for spell.
		// Coloring: ^CRRGGBBAA*text*
		
		tooltipText = "^C7ED8E6ff" + name + "\n";
		tooltipText += "^CffffffffType: " + type + "\n\n";

		tooltipText += "Absorbs " + amount + " damage.\n";
	}
	
}