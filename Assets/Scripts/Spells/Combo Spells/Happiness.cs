using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Happiness : ComboSpell {
	
	public Happiness(){
		name = "Happiness";
		damage = 0;
		healOverTime = 3;
		turn = 5;
		mana = 0;
		type = "nature";
		level = 1;
		isOverTime = true;
		spriteName = "avatar_spell_12";
	}
	
	public override bool cast(Battle battle, Creature caster, Creature target, int critIncrease){
		battle.addActiveSpell(this, caster);
		return true;
	}

	public override bool requires(Queue<Spell> castedSpells){
		int casted_heal_count = 0;
		foreach(Spell spell in castedSpells){
			if(spell.name == "Heal")
				casted_heal_count++;
		}
		if(casted_heal_count >= 2)
			return true;
		return false;
	}

	public override void setTooltipText() {
		// Tooltip text for spell.
		// Coloring: ^CRRGGBBAA*text*
		
		tooltipText = "^C7ED8E6ff" + name + "\n";
		tooltipText += "^CffffffffType: " + type + "\n";
		tooltipText += "Combo: Heal + Heal\n\n";
		tooltipText += "Heals the caster for " + healOverTime + " every turn for " + turn + " turns.\n";
	}
}
