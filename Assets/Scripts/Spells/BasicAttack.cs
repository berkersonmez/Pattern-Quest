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
		int currentDamage = 3 + caster.spellPower - target.armor;
		if(currentDamage < 0)
			currentDamage = 0;
		target.decreaseHp(caster, currentDamage, name);
		return true;
		//Fireball görsel efekti yapan fonksiyon eklenecek
	}
	
}
