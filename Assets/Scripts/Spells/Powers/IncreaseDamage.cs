using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IncreaseDamage : Power {
	
	public IncreaseDamage(){
		name = "IncreaseDamage";
		totalAmount = 2;
		currentAmount = 2;
		percent = 0;
		effectOn = "self";
		totalCoolDown = -1;
		mana = 0;
		type = "all";
		active = true;
		level = 1;
	}
	
	public override bool react(Spell castedSpell, string effectOn, ref string combatTextExtra){
		if(effectOn != this.effectOn)
			return false;
		if(castedSpell.damage == 0)
			return false;
		if((castedSpell.type == this.type) || (this.type == "all"))
			castedSpell.damage += this.currentAmount;
		this.currentCooldDown = totalCoolDown;
		this.active = false;
		return false;
	}

	public override void setTooltipText() {
		// Tooltip text for spell.
		// Coloring: ^CRRGGBBAA*text*
		
		tooltipText = "^C7ED8E6ff" + name + "\n";
		tooltipText += "^CffffffffType: " + type + "\n\n";
		
		tooltipText += "Increases damage by " + totalAmount + " on attack.\n";
	}
	
}