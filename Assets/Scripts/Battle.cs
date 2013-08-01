using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Battle : MonoBehaviour {
	
	public static Battle instance;
	public Creature player;
	public Creature creature;
	public Creature dead=null;
	public int turn=0;
	public int whoseTurn=0;
	public List<Spell> castedSpells = new List<Spell>();
	public List <ActiveSpell> activeSpellsOnPlayer = new List<ActiveSpell>();
	public List <ActiveSpell> toDeleteSpellsOnPlayer = new List<ActiveSpell>();
	public List <ActiveSpell> activeSpellsOnCreature = new List<ActiveSpell>();
	public List <ActiveSpell> toDeleteSpellsOnCreature = new List<ActiveSpell>();
	
	
	
	public bool isAnyoneDead(){
		
		if(player.currentHp <= 0){
			Debug.Log("Player is dead");
			dead = player;
			return true;
		}
		if(creature.currentHp <= 0){
			Debug.Log(creature.name + " is dead");
			dead = creature;
			return true;
		}
		return false;
	}
	
	public void passNextTurn(){
		if(whoseTurn == 0){
			foreach (ActiveSpell activeSpell in activeSpellsOnPlayer){
				if(activeSpell.effect(ref player) == false)
					this.toDeleteSpellsOnPlayer.Add(activeSpell);
			}
			if(isAnyoneDead())
				return;
			foreach (Spell spell in castedSpells){
				Debug.Log("player buyu atiy");
				spell.cast(this, ref player, ref creature);
			}
			if(isAnyoneDead())
				return;
		}
		else{
			foreach (ActiveSpell activeSpell in activeSpellsOnCreature){
				if(activeSpell.effect(ref creature) == false)
					this.toDeleteSpellsOnCreature.Add(activeSpell);
			}
			if(isAnyoneDead())
				return;
			foreach (Spell spell in castedSpells){
				spell.cast(this, ref creature, ref player);
			}
			if(isAnyoneDead())
				return;
			turn++;
		}
		clearFinishedSpells();
	}
	
	public void clearFinishedSpells(){
		foreach (ActiveSpell activeSpell in toDeleteSpellsOnPlayer){
				activeSpellsOnPlayer.Remove(activeSpell);
		}
		foreach (ActiveSpell activeSpell in toDeleteSpellsOnCreature){
				activeSpellsOnCreature.Remove(activeSpell);
		}
		toDeleteSpellsOnPlayer.Clear();
		toDeleteSpellsOnCreature.Clear();
	}
	
	// Use this for initialization
	void Start () {
		instance = this;
	}
	
	void Update () {
		if(this.castedSpells.Count == 2){
			passNextTurn();
			Debug.Log ("2 buyu atildi, ecnebinin cani: " + creature.currentHp);
			castedSpells.Clear();
		}
	}
	
}