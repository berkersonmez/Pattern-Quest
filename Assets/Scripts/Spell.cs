using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
	public List<int> shape = new List<int>();
	
	public virtual  void cast(Battle battle, ref Creature caster, ref Creature target){
		caster.decreaseMana(mana);
		target.decreaseHp(damage);
	}
	
	public virtual bool increaseLevel(){
		if(level == maxLevel)
			return false;
		else{
			level++;
			return true;
		}
	}
	
	public virtual void turnEffect(ref Creature creature){
		creature.decreaseHp(damageOverTime);
		creature.increaseHp(healOverTime);
	}
}
