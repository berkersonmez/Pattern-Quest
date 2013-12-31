using UnityEngine;
using System.Collections;

public class BasicAttack : Spell {
	
	public BasicAttack(){
		name = "BasicAttack";
		damage = 3;
		mana = 0;
		type = "fire";
		level = 1;
		shape.Add(9);
		shape.Add(9);
	}
	
	public override bool cast(Battle battle, Creature caster, Creature target){
		string combatTextExtra = "";
		if(caster.currentMana - mana < 0)
			return false;
		Spell temp = new Spell();
		temp = this.copy();
		temp.damage += caster.spellPower/2 - target.armor;
		caster.react(temp,"self",ref combatTextExtra);
		bool result = target.react(temp,"enemy",ref combatTextExtra);
		if(result)
			return true;
		
		int currentDamage = temp.damage;
		if(currentDamage < 0)
			currentDamage = 0;
		caster.decreaseMana(temp.mana);
		
		currentDamage = applyCritical(caster, currentDamage);
		
		target.decreaseHp(caster, currentDamage);
		// Combat text
		int placement = target.isPlayer ? (int)CombatTextController.Placement.PLAYER : (int)CombatTextController.Placement.CREATURE;
		CombatTextController.instance.deployText(name, currentDamage.ToString() + combatTextExtra, placement, Color.red);
		return true;
	}
	
}
