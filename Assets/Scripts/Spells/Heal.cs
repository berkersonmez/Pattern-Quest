using UnityEngine;
using System.Collections;

public class Heal : Spell {
	
	public Heal(){
		name = "Heal";
		heal = 8;
		mana = 5;
		type = "nature";
		level = 1;
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
	
	public override bool cast(Battle battle, ref Creature caster, ref Creature target){
		if(caster.currentMana - mana < 0)
			return false;
		int currentHeal = heal + caster.spellPower;
		caster.decreaseMana(mana);
		caster.increaseHp(currentHeal, name);
		Debug.Log(this.name + "'i patlattim *" + this.heal + "*");
		return true;
		//Fireball görsel efekti yapan fonksiyon eklenecek
	}
	
}
