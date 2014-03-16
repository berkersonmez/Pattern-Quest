using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Clarity : ComboSpell {
	
	public Clarity(){
		name = "Clarity";
		damage = 3;
		mana = 0;
		type = "fire";
		level = 1;
		spriteName = "avatar_spell_6";
	}
	
	public override bool cast(Battle battle, Creature caster, Creature target, int critIncrease){
		string combatTextExtra = "";
		caster.increaseMana(damage);
		// Combat text
		int placement = caster.isPlayer ? (int)CombatTextController.Placement.PLAYER : (int)CombatTextController.Placement.CREATURE;
		CombatTextController.instance.deployText(name, damage.ToString() + combatTextExtra, placement, Color.blue);
		return true;
	}
	
	public override bool requires(Queue<Spell> castedSpells){
		int basic_attack_count = 0;
		foreach(Spell spell in castedSpells){
			if(spell.name == "BasicAttack")
				basic_attack_count++;
		}
		if(basic_attack_count >= 2)
			return true;
		return false;
	}

	public override void setTooltipText() {
		// Tooltip text for spell.
		// Coloring: ^CRRGGBBAA*text*
		
		tooltipText = "^C7ED8E6ff" + name + "\n";
		tooltipText += "^CffffffffType: " + type + "\n";
		tooltipText += "Combo: Basic Attack + Basic Attack\n\n";
		tooltipText += "Restores 3 mana.\n";
	}
}
