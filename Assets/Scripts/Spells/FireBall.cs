using UnityEngine;
using System.Collections;

public class FireBall : Spell {
	
public FireBall(){
		name = "Fire Ball";
		damage = 5;
		mana = 3;
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
	
public override void cast(Battle battle, Creature caster, Creature target){
	target.hp = target.hp - this.damage;
	caster.mana = caster.mana - this.mana;
	//Fireball görsel efekti yapan fonksiyon eklenecek
}
	
}
