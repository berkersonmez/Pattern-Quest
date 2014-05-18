using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ReducedMana : Power {
	
	public ReducedMana(){
		name = "Reduced Mana";
		idName = "ReducedMana";
		percent = 50;
		effectOn = "self";
		totalCoolDown = -1;
		mana = 0;
		type = "all";
		active = true;
		level = 1;
		spriteName = "avatar_spell_8";
	}
	
	public override bool react(Spell castedSpell, string effectOn, ref string combatTextExtra){
		if(effectOn != this.effectOn)
			return false;
		if(DungeonController.instance.battle.turn > 1){
			this.active = false;
			return false;
		}
		castedSpell.mana -= (int) (castedSpell.mana * ((float)percent/100));
		return false;
	}

	public override void setTooltipText() {
		// Tooltip text for spell.
		// Coloring: ^CRRGGBBAA*text*
		
		tooltipText = "^C7ED8E6ff" + name + "\n";
		tooltipText += "^CffffffffType: " + type + "\n\n";
		
		tooltipText += "Reduce mana cost of the spells casted on first turn by " + percent + " percent.\n";
	}
	
}