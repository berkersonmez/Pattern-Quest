using UnityEngine;
using System.Collections;

[System.Serializable]
public class NonEndingFire : Spell {
	
	public NonEndingFire(){
		name = "Non-Ending Fire";
		idName = "NonEndingFire";
		damage = 4;
		damageOverTime = 2;
		turn = 3;
		mana = 5;
		type = "fire";
		totalCoolDown = 3;
		level = 1;
		isOverTime = true;
		shape.Add(6);
		shape.Add(2);
		shape.Add(4);
		shape.Add(8);
		spriteName = "avatar_spell_10";
	}
	
	public override bool increaseLevel(){
		if(level == maxLevel)
			return false;
		else{
			level++;
			damageOverTime = Mathf.CeilToInt(damageOverTime*3/2);
			mana = Mathf.CeilToInt(mana*5/3);
			return true;
		}
	}
	
	public override bool cast(Battle battle, Creature caster, Creature target, int critIncrease){
		string combatTextExtra = "";
		if(this.currentCooldDown > 0)
			return false;
		if(caster.currentMana - mana < 0)
			return false;
		Spell temp = new Spell();
		temp = this.copy();
		temp.damage += (caster.SpellPower - target.Armor)/2;
		temp.damageOverTime += (int)((float)(caster.SpellPower - target.Armor) / turn);
		caster.react(temp,"self",ref combatTextExtra);
		bool result = target.react(temp,"enemy",ref combatTextExtra);
		if(result)
			return true;

		int currentDamage = temp.damage;
		if(currentDamage < 0)
			currentDamage = 0;
		caster.decreaseMana(temp.mana);

		currentDamage = applyCritical(caster, currentDamage, critIncrease);

		target.decreaseHp(caster, currentDamage);
		// Combat text
		int placement = target.isPlayer ? (int)CombatTextController.Placement.PLAYER : (int)CombatTextController.Placement.CREATURE;
		CombatTextController.instance.deployText(name, currentDamage.ToString() + combatTextExtra, placement, Color.red);

		battle.addActiveSpell(temp, target);
		if(this.totalCoolDown > 0)
			this.currentCooldDown = this.totalCoolDown;
		return true;
	}

	public override void setTooltipText() {
		// Tooltip text for spell.
		// Coloring: ^CRRGGBBAA*text*
		Creature caster = owner;
		int currentDamage = damage + caster.SpellPower;
		float currentDamageOverTime = (float)(damageOverTime*turn + caster.SpellPower) / turn;


		tooltipText = "^C7ED8E6ff" + name + "\n";
		tooltipText += "^CffffffffType: " + type + "\n";
		if (mana != 0) tooltipText += "Mana Cost: " + mana + "\n\n";
		tooltipText += "Deals " + currentDamage + " damage instantly and " + (int)currentDamageOverTime +
			" every turn for " + turn + " turns.\n";
	}
	
}
