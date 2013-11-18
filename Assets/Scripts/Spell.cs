using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Spell {
	public enum EffectType {PROJECTILE = 0, ENEMY_INSTANT, SELF_INSTANT};
	
	public int damage=0;
	public int damageOverTime=0;
	public int mana;
	public string type;
	public int heal=0;
	public int healOverTime=0;
	public int level=1;
	public int maxLevel;
	public string name;
	public int turn=0;
	public bool isOverTime=false;
	public List<int> shape = new List<int>();
	public int effectType;
	
	public Spell(){
		
	}
	
	public Spell(int damage){
		this.damage = damage;
		this.mana = 0;
		//this.mana = (int)Mathf.Sqrt(damage);
		//Debug.Log("mana: " + this.mana);
		this.name = "Basic Attack";
		this.shape.Add(9);
		this.shape.Add(9);
	}
	
	public virtual bool cast(Battle battle, ref Creature caster, ref Creature target){
		if(caster.currentMana - mana < 0)
			return false;
		caster.decreaseMana(mana);
		target.decreaseHp(damage, name);
		return true;		
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
		creature.decreaseHp(damageOverTime, name);
		creature.increaseHp(healOverTime);
	}
}
