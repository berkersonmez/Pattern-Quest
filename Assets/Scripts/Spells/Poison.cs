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
	
public override void cast(Battle battle, ref Creature caster, ref Creature target){
	caster.currentMana = caster.currentMana - this.mana;
	ActiveSpell activeSpell = new ActiveSpell(this);
	if(target.isPlayer)
		battle.activeSpellsOnPlayer.Add(activeSpell);
	else
		battle.activeSpellsOnCreature.Add(activeSpell);
	//GÖRSEL EFEKT
}
	
}
