using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpellReflect : Power {
	
	public SpellReflect(){
		name = "SpellReflect";
		effectOn = "enemy";
		totalCoolDown = 5;
		mana = 0;
		type = "fire";
		active = true;
		level = 1;
	}
	
	public override bool react(Spell castedSpell, string effectOn, ref string combatTextExtra){
		if(effectOn != this.effectOn)
			return false;
		if(castedSpell.isOneTick == false){
			this.active = false;
			castedSpell.cast(DungeonController.instance.battle, castedSpell.owner, castedSpell.owner);
			//TODO: PRINT "REFLECTED" ON TARGET
			return true;
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