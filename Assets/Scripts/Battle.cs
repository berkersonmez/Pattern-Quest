using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Battle {
	enum State {CAST_PHASE = 0, SPELL_EFFECT, ACTIVE_SPELL_EFFECT, END};
	enum Turn {PLAYER = 0, CREATURE};
	
	public Creature player;
	public Creature creature;
	public Creature dead=null;
	public int turn=0;
	public int state=0;
	public int whoseTurn=0;
	public float delayUpdateUntil = 0f;
	public Queue<Spell> castedSpells = new Queue<Spell>();
	public List <ActiveSpell> activeSpellsOnPlayer = new List<ActiveSpell>();
	public List <ActiveSpell> toDeleteSpellsOnPlayer = new List<ActiveSpell>();
	public List <ActiveSpell> activeSpellsOnCreature = new List<ActiveSpell>();
	public List <ActiveSpell> toDeleteSpellsOnCreature = new List<ActiveSpell>();
	
	public Battle(ref Player player, ref Creature creature){
		this.player = player;
		this.creature = creature;
		turn = 0;
		whoseTurn = (int)Turn.PLAYER;
	}
	
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
	
	// To be deleted
	public void passNextTurn(){
		if(whoseTurn == (int)Turn.PLAYER){
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
	
	// To be deleted
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
	
	public void castSpell(Spell spell) {
		if (state != (int)State.CAST_PHASE) return;
		
		if (whoseTurn == (int)Turn.PLAYER) {
			spell.cast(this, ref player, ref creature);
		} else {
			spell.cast(this, ref creature, ref player);
		}
		castedSpells.Enqueue(spell);
		if (isAnyoneDead()) {
			state = (int)State.END;
			return;
		}
		if(this.castedSpells.Count == 2){
			state = (int)State.SPELL_EFFECT;
		}
	}
	
	public void delayUpdate(float seconds) {
		delayUpdateUntil = Time.time + seconds;
	}
	
	public void update() {
		if (Time.time < delayUpdateUntil) return;
		
		switch (state) {
		case (int)State.CAST_PHASE:
			break;
		case (int)State.SPELL_EFFECT:
			// TODO: Combo logic here?
			state = (int)State.ACTIVE_SPELL_EFFECT;
			delayUpdate(1f);
			break;
		case (int)State.ACTIVE_SPELL_EFFECT:
			// TODO: Implement active spell thing here
			turn++;
			state = (int)State.CAST_PHASE;
			break;
		case (int)State.END:
			// TODO: Implement next battle logic here
			break;
		}
	}
	
}