using UnityEngine;
using System.Collections;

public class Poison : Spell {
	
public Poison(){
		name = "Poison";
		damage = 0;
		damageOverTime = 3;
		turn = 3;
		mana = 4;
		type = "nature";
		level = 1;
		isOverTime = true;
		shape.Add(8);
		shape.Add(6);
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
	
public override bool cast(Battle battle, ref Creature caster, ref Creature target){
	if(caster.currentMana - mana < 0)
		return false;
	caster.decreaseMana(mana);
	battle.addActiveSpell(this, target);
	return true;
}
	
}
