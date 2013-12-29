using UnityEngine;
using System.Collections;

public class BasicAttack : Spell {
	
	public BasicAttack(){
		name = "BasicAttack";
		damage = 6;
		mana = 0;
		type = "fire";
		level = 1;
		shape.Add(9);
		shape.Add(9);
	}
	
	public override bool cast(Battle battle, Creature caster, Creature target){
		string combatTextExtra = "";
		int currentDamage = 3 + caster.spellPower - target.armor;
		if(currentDamage < 0)
			currentDamage = 0;

		currentDamage = applyCritical(caster, currentDamage);

		target.decreaseHp(caster, currentDamage);
		// Combat text
		int placement = target.isPlayer ? (int)CombatTextController.Placement.PLAYER : (int)CombatTextController.Placement.CREATURE;
		CombatTextController.instance.deployText(name, currentDamage.ToString() + combatTextExtra, placement, Color.red);
		return true;
	}
	
}
