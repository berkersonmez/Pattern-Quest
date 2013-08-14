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
	
	// Animation variables
	public GameObject effectParticle1Pre;
	public GameObject effectParticle2Pre;
	public GameObject effectParticle3Pre;
	public GameObject effectParticle1;
	public GameObject effectParticle2;
	public GameObject effectParticle3;
	public Vector3 effectSource;
	public Vector3 effectTarget;
	public Vector3 effectDirection;
	public bool animFlag1 = false;
	public bool animFlag2 = false;
	public bool animFlag3 = false;
	public float delayAnimateUntil = 0f;
	
	public Spell(){
		
	}
	
	public Spell(int damage){
		this.damage = damage;
		this.mana = 0;
		//this.mana = (int)Mathf.Sqrt(damage);
		Debug.Log("mana: " + this.mana);
		this.name = "Basic Attack";
		this.shape.Add(9);
		this.shape.Add(9);
		effectParticle1Pre = EffectHolder.instance.normalAttack;
		effectType = (int)EffectType.ENEMY_INSTANT;
	}
	
	public virtual bool cast(Battle battle, ref Creature caster, ref Creature target){
		if(caster.currentMana - mana < 0)
			return false;
		caster.decreaseMana(mana);
		target.decreaseHp(damage);
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
		creature.decreaseHp(damageOverTime);
		creature.increaseHp(healOverTime);
	}
	
	// ANIMATION STUFF BELOW
	
	public void delayAnimate(float seconds) {
		delayAnimateUntil = Time.time + seconds;
	}
	
	public virtual void animateStart(bool isPlayerCasting) {
		if (effectParticle1Pre != null) {
			effectParticle1 = MonoBehaviour.Instantiate(effectParticle1Pre) as GameObject;
			effectParticle1.transform.parent = GameObject.Find("AnchorUC").transform;
		}
		animFlag1 = false;
		animFlag2 = false;
		animFlag3 = false;
		switch (effectType) {
		case (int)EffectType.PROJECTILE:
			if (isPlayerCasting) {
				effectSource = EffectHolder.instance.playerPos;
				effectTarget = EffectHolder.instance.creaturePos;
				effectDirection = new Vector3(1f, 0f, 0f);
			} else {
				effectSource = EffectHolder.instance.creaturePos;
				effectTarget = EffectHolder.instance.playerPos;
				effectDirection = new Vector3(-1f, 0f, 0f);
				effectParticle1.transform.localRotation = Quaternion.Euler(0, 90f, 0);
			}
			effectParticle1.transform.localPosition = effectSource;
			break;
		case (int)EffectType.ENEMY_INSTANT:
			if (isPlayerCasting) {
				effectTarget = EffectHolder.instance.creaturePos;
			} else {
				effectTarget = EffectHolder.instance.playerPos;
			}
			effectParticle1.transform.localPosition = effectTarget;
			break;
		case (int)EffectType.SELF_INSTANT:
			if (isPlayerCasting) {
				
			} else {
				
			}
			break;
		}
	}
	
	public virtual void animate() {
		if (Time.time < delayAnimateUntil) return;
		
		switch (effectType) {
		case (int)EffectType.PROJECTILE:
			if (!animFlag1) {
				if (Vector3.Distance(effectParticle1.transform.localPosition, effectTarget) < .5f) {
					
					GameObject.Destroy(effectParticle1);
					effectParticle2 = MonoBehaviour.Instantiate(effectParticle2Pre) as GameObject;
					effectParticle2.transform.parent = GameObject.Find("AnchorUC").transform;
					effectParticle2.transform.localPosition = effectTarget;
					delayAnimate(.5f);
					animFlag1 = true;
				} else {
					effectParticle1.transform.position += effectDirection * .3f;
				}
			} else {
				GameObject.Destroy(effectParticle2);
				DungeonController.instance.battle.spellAnimComplete();
			}
			break;
		case (int)EffectType.ENEMY_INSTANT:
			if (!animFlag1) {
				delayAnimate(.5f);
				animFlag1 = true;
			} else {
				GameObject.Destroy(effectParticle1);
				DungeonController.instance.battle.spellAnimComplete();
			}
			break;
		case (int)EffectType.SELF_INSTANT:
			break;
		}
	}
}
