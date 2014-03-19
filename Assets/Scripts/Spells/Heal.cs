using UnityEngine;
using System.Collections;

[System.Serializable]
public class Heal : Spell {
	
	public Heal(){
		name = "Heal";
		damage=0;
		damageOverTime=0;
		heal = 8;
		healOverTime=0;
		mana = 5;
		type = "nature";
		level = 1;
		spriteName = "avatar_spell_4";
		shape.Add(6);
		shape.Add(6);
		shape.Add(7);
		shape.Add(2);
		shape.Add(2);
	}
	
	public override bool increaseLevel(){
		if(level + 1 < maxLevel){
			level = level+1;
			heal = heal*2;
			mana = Mathf.CeilToInt(mana*5/3);
			return true;
		}
		else
			return false;
		
	}
	
	public override bool cast(Battle battle, Creature caster, Creature target, int critIncrease){
		string combatTextExtra = "";
		if(caster.currentMana - mana < 0)
			return false;
		Spell temp = new Spell();
		temp = this.copy();
		temp.heal = temp.heal + caster.SpellPower;
		caster.react(temp,"self",ref combatTextExtra);
		caster.decreaseMana(temp.mana);
		int currentHeal = temp.heal;
		if(currentHeal < 0)
			currentHeal = 0;
		currentHeal = applyCritical(caster, currentHeal, critIncrease);

		caster.increaseHp(currentHeal);
		// Combat text
		int placement = caster.isPlayer ? (int)CombatTextController.Placement.PLAYER : (int)CombatTextController.Placement.CREATURE;
		CombatTextController.instance.deployText(name, currentHeal.ToString() + combatTextExtra, placement, Color.green);
		return true;
	}

	public override void setTooltipText() {
		// Tooltip text for spell.
		// Coloring: ^CRRGGBBAA*text*
		Creature caster = owner;
		int currentHeal = heal + caster.SpellPower;

		tooltipText = "^C7ED8E6ff" + name + "\n";
		tooltipText += "^CffffffffType: " + type + "\n";
		if (mana != 0) tooltipText += "Mana Cost: " + mana + "\n\n";
		tooltipText += "Heals the caster for " + currentHeal + " hp.\n";
	}
	
}
