using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Rampage : Power {
	
	public Rampage(){
		name = "Rampage";
		idName = "Rampage";
		percent = 10;
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
			castedSpell.owner.criticalStrikeChance = 100;
		}
		return false;
	}
	
	public override void setTooltipText() {
		// Tooltip text for spell.
		// Coloring: ^CRRGGBBAA*text*
		
		tooltipText = "^C7ED8E6ff" + name + "\n";
		tooltipText += "^CffffffffType: " + type + "\n\n";
		
		tooltipText += "You always deal critical damage under %" + percent + " health .\n";
	}
	
}