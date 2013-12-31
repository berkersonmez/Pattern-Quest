using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Spell {
	public enum EffectType {PROJECTILE = 0, ENEMY_INSTANT, SELF_INSTANT};
	
	public int damage=0;
	public int damageOverTime=0;
	public int mana=0;
	public string type;
	public int heal=0;
	public int healOverTime=0;
	public int currentCooldDown=0;
	public int totalCoolDown=0;
	public int level=1;
	public int maxLevel;
	public bool isOneTick=false;	//This is an special variable to make 1 tick of any dot understandable
	public string name;
	public string spriteName = "avatar_spell_0";
	public int turn=0;
	public bool isOverTime=false;
	public List<int> shape = new List<int>();
	public int effectType;

	public Creature owner;

	public string tooltipText;
	
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

	public Spell(string name, int damage){
		this.name = name;
		this.damage = damage;
		this.mana = 0;
	}

	public Spell(string name, int damage, bool isOneTick){
		this.name = name;
		this.damage = damage;
		this.isOneTick = isOneTick;
		this.mana = 0;
	}

	public virtual bool cast(Battle battle, Creature caster, Creature target){
		string combatTextExtra = "";
		if(caster.currentMana - mana < 0)
			return false;
		Spell temp = new Spell();
		temp = this.copy();
		//This means if the spell is not a psudo spell
		if(temp.isOneTick == false){
			if(temp.damage > 0)
				temp.damage += caster.spellPower - target.armor;
			caster.react(temp,"self",ref combatTextExtra);
		}
		caster.decreaseMana(temp.mana);
		bool result = target.react(temp,"enemy",ref combatTextExtra);
		if(result)
			return true;

		int currentDamage = temp.damage;
		if(currentDamage < 0)
			currentDamage = 0;

		currentDamage = applyCritical(caster, currentDamage);

		target.decreaseHp(caster, currentDamage);
		// Combat text
		int placement = target.isPlayer ? (int)CombatTextController.Placement.PLAYER : (int)CombatTextController.Placement.CREATURE;
		CombatTextController.instance.deployText(name, currentDamage.ToString() + combatTextExtra, placement, Color.red);
		this.currentCooldDown = this.totalCoolDown;
		return true;		
	}

	public virtual void update(){
		if(this.currentCooldDown > 0)
			this.currentCooldDown--;
	}

	public virtual void resetCooldown(){
		this.currentCooldDown = 0;
	}

	public virtual int applyCritical(Creature caster, int currentDamage) {
		int randomValue = Random.Range(1,100);
		if(randomValue <= caster.criticalStrikeChance){
			currentDamage = currentDamage * 2;
		}
		return currentDamage;
	}
	
	public virtual bool increaseLevel(){
		if(level == maxLevel)
			return false;
		else{
			level++;
			return true;
		}
	}
	
	public virtual void turnEffect(Battle battle, ref Creature caster, ref Creature target){
		if(this.damageOverTime > 0){
			int currentDamage = damageOverTime;
			Spell temp = new Spell(this.name,currentDamage,true);
			temp.cast(battle, caster, target);
			return;
		}
		if(healOverTime > 0) {
			string combatTextExtra = "";
			target.increaseHp(healOverTime);
			// Combat text
			int placement = target.isPlayer ? (int)CombatTextController.Placement.PLAYER : (int)CombatTextController.Placement.CREATURE;
			CombatTextController.instance.deployText(name, healOverTime.ToString() + combatTextExtra, placement, Color.green);
		}
	}

	public virtual void setTooltipText() {
		// Tooltip text for spell.
		// Coloring: ^CRRGGBBAA*text*
		tooltipText = "^C7ED8E6ff" + name + "\n";
		tooltipText += "^CffffffffType: " + type + "\n";
		if (mana != 0) tooltipText += "Mana Cost: " + mana + "\n";
		if (damage != 0) tooltipText += "Damage: " + damage + "\n";
		if (heal != 0) tooltipText += "Heal: " + heal + "\n";
	}

	public Spell copy(){
		return (Spell)this.MemberwiseClone();
	}
}
