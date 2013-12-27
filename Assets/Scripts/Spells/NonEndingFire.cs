using UnityEngine;
using System.Collections;

public class NonEndingFire : Spell {
	
	public NonEndingFire(){
		name = "NonEndingFire";
		damage = 4;
		damageOverTime = 2;
		turn = 3;
		mana = 5;
		type = "fire";
		level = 1;
		isOverTime = true;
		shape.Add(6);
		shape.Add(2);
		shape.Add(4);
		shape.Add(8);
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
	
	public override bool cast(Battle battle, Creature caster, Creature target){
		if(caster.currentMana - mana < 0)
			return false;
		int currentDamage = damage + caster.spellPower - target.armor;
		if(currentDamage < 0)
			currentDamage = 0;
		caster.decreaseMana(mana);
		target.decreaseHp(caster, damage, name);
		Debug.Log(this.name + "'i patlattim *" + this.damage + "*");
		battle.addActiveSpell(this, target);
		return true;
	}
	
}
