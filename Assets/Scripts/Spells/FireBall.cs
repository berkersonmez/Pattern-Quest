using UnityEngine;
using System.Collections;

public class FireBall : Spell {
	
	public FireBall(){
		name = "FireBall";
		damage = 5;
		mana = 1;
		type = "fire";
		level = 1;
		shape.Add(6);
		shape.Add(2);
		shape.Add(4);
		shape.Add(8);
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
		
	public override bool cast(Battle battle, ref Creature caster, ref Creature target){
		if(caster.currentMana - mana < 0)
			return false;
		caster.decreaseMana(mana);
		target.decreaseHp(damage, name);
		Debug.Log(this.name + "'i patlattim *" + this.damage + "*");
		return true;
		//Fireball görsel efekti yapan fonksiyon eklenecek
	}
	
}
