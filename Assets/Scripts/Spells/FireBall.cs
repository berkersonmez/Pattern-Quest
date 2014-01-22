using UnityEngine;
using System.Collections;

[System.Serializable]
public class FireBall : Spell {
	
	public FireBall(){
		name = "FireBall";
		damage = 6;
		mana = 3;
		type = "fire";
		level = 1;
		shape.Add(9);
		shape.Add(2);
		shape.Add(9);
	}
		
	public override bool increaseLevel(){
		if(level + 1 < maxLevel){
			level = level+1;
			damage = damage*2;
			mana = Mathf.CeilToInt(mana*5/3);
			return true;
		}
		else
			return false;
	
	}
		
	public override bool cast(Battle battle, Creature caster, Creature target, int critDamageIncrease){
		string combatTextExtra = "";
		if(caster.currentMana - mana < 0)
			return false;
		Spell temp = new Spell();
		temp = this.copy();
		temp.damage = temp.damage + caster.SpellPower - target.Armor;
		caster.react(temp,"self",ref combatTextExtra);
		bool result = target.react(temp,"enemy",ref combatTextExtra);
		if(result)
			return true;

		int currentDamage = temp.damage;
		if(currentDamage < 0)
			currentDamage = 0;
		caster.decreaseMana(temp.mana);

		currentDamage = applyCritical(caster, currentDamage, critDamageIncrease);

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
