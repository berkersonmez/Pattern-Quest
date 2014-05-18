using UnityEngine;
using System.Collections;

[System.Serializable]
public class SoulRipper : Spell {
	
	public SoulRipper(){
		name = "Soul Ripper";
		idName = "SoulRipper";
		percent = 10;
		mana = 3;
		type = "fire";
		level = 1;
		shape.Add(9);
		shape.Add(2);
		shape.Add(2);
		shape.Add(9);
		shape.Add(9);
		spriteName = "avatar_spell_9";
	}
	
	public override bool cast(Battle battle, Creature caster, Creature target, int critIncrease){
		string combatTextExtra = "";
		if(caster.currentMana - mana < 0)
			return false;
		Spell temp = new Spell();
		temp = this.copy();
		temp.damage = (int)((float)target.Hp * this.percent / 100.0);
		caster.react(temp,"self",ref combatTextExtra);
		bool result = target.react(temp,"enemy",ref combatTextExtra);
		if(result)
			return true;
		
		int currentDamage = temp.damage;
		if(currentDamage < 0)
			currentDamage = 0;
		caster.decreaseMana(temp.mana);
		
		target.decreaseHp(caster, currentDamage);
		// Combat text
		int placement = target.isPlayer ? (int)CombatTextController.Placement.PLAYER : (int)CombatTextController.Placement.CREATURE;
		CombatTextController.instance.deployText(name, currentDamage.ToString() + combatTextExtra, placement, Color.red);
		return true;
	}
	
	public override void setTooltipText() {
		// Tooltip text for spell.
		// Coloring: ^CRRGGBBAA*text*
		Creature caster = owner;
		int currentDamage = damage + caster.SpellPower;
		
		tooltipText = "^C7ED8E6ff" + name + "\n";
		tooltipText += "^CffffffffType: " + type + "\n";
		if (mana != 0) tooltipText += "Mana Cost: " + mana + "\n\n";
		tooltipText += "Inflicts " + currentDamage + " damage to enemy.\n";
	}
	
}
