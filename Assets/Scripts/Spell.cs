using UnityEngine;
using System.Collections;

public class Spell {

	public int damage=0;
	public int damageOverTime=0;
	public int mana;
	public string type;
	public int heal=0;
	public int healOverTime=0;
	public int level=1;
	public int maxLevel;
	public string name;
	public int turn;
	public bool isOverTime=false;
	
	public virtual  void cast(Battle battle, Creature caster, Creature target){
		caster.mana = caster.mana - this.mana;
		target.hp = target.hp - this.damage;
	}
	
	public virtual bool increaseLevel(){
		if(level == maxLevel)
			return false;
		else{
			level++;
			return true;
		}
	}
	
	public virtual void turnEffect(Creature creature){
		creature.currentHp = creature.currentHp - damageOverTime;
		creature.currentHp = creature.currentHp + healOverTime;
	}
}
