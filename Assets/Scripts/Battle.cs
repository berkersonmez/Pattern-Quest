using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Battle {
	enum State {CAST_PHASE = 0, SPELL_ANIM, SPELL_EFFECT, ACTIVE_SPELL_EFFECT, END};
	enum Turn {PLAYER = 0, CREATURE};
	
	public Creature player;
	public Creature creature;
	public Creature dead=null;
	public int turn=0;
	public int state=0;
	public int whoseTurn=0;
	public float delayUpdateUntil = 0f;
	public Queue<Spell> castedSpells = new Queue<Spell>();
	public List<ComboSpell> comboSpells = new List<ComboSpell>();
	public Queue <ActiveSpell> activeSpellsOnPlayer = new Queue<ActiveSpell>();
	public Queue <ActiveSpell> activeSpellsOnCreature = new Queue<ActiveSpell>();
	public Spell castingSpell;

	
	public Battle(Player player, Creature creature){
		this.player = player;
		this.creature = creature;
		turn = 0;
		whoseTurn = (int)Turn.PLAYER;
		DungeonController.instance.switchTurn(true);
	}
	
	public bool isAnyoneDead(){
		if(player.currentHp <= 0){
			Debug.Log("Player is dead");
			dead = player;
			DungeonController.instance.finishBattle(false);
			return true;
		}
		if(creature.currentHp <= 0){
			Debug.Log(creature.name + " is dead");
			dead = creature;
			DungeonController.instance.finishBattle(true);
			return true;
		}
		return false;
	}
	
	public void castSpell(Spell spell) {
		if (state != (int)State.CAST_PHASE) return;
		
		castingSpell = spell;

		state = (int)State.SPELL_ANIM;
	}

	public void castComboSpells(){
		if(whoseTurn == (int)Turn.PLAYER) {
			this.comboSpells = player.getComboSpells(castedSpells);
			if(comboSpells == null)
				return;
			foreach(ComboSpell combo in comboSpells){
				combo.cast(this,player,creature);
			}
			this.comboSpells.Clear();
		} else {
			this.comboSpells = creature.getComboSpells(castedSpells);
			if(comboSpells == null)
				return;
			foreach(ComboSpell combo in comboSpells){
				combo.cast(this,creature,player);
			}
			this.comboSpells.Clear();
		}
	}

	public void addActiveSpell(Spell spell, Creature target){
		Queue <ActiveSpell> activeSpells;
		if(target.isPlayer)
			activeSpells = activeSpellsOnPlayer;
		else
			activeSpells = activeSpellsOnCreature;
		foreach(ActiveSpell activeSpell in activeSpells){
			if(activeSpell.spell.isOverTime)
				if(activeSpell.spell.type == spell.type){
				activeSpell.remainingTurn = spell.turn;
				return;
			}
		}
		ActiveSpell newActiveSpell = new ActiveSpell(spell);
		activeSpells.Enqueue(newActiveSpell);
	}
	
	public void delayUpdate(float seconds) {
		delayUpdateUntil = Time.time + seconds;
	}
	
	public void passNextTurn(){
		this.turn++;	
		player.increaseMana(player.manaRegen);
		creature.increaseMana(creature.manaRegen);
	}
	
	public void spellAnimComplete() {
		bool result;
		if (whoseTurn == (int)Turn.PLAYER) {
			result = castingSpell.cast(this, player, creature);
		} else {
			result = castingSpell.cast(this, creature, player);
		}
		state = (int)State.CAST_PHASE;
		
		Debug.Log("sonuc: " + result);
		if(result == true)
			castedSpells.Enqueue(castingSpell);
		if (isAnyoneDead()) {
			state = (int)State.END;
			return;
		}
		if(this.castedSpells.Count == 2){
			state = (int)State.SPELL_EFFECT;

		}
		delayUpdate(1.5f);
	}
	
	public void update() {
		if (Time.time < delayUpdateUntil) return;
		
		switch (state) {
		case (int)State.ACTIVE_SPELL_EFFECT:
			if (whoseTurn == (int)Turn.PLAYER) {
				if (player.activateActiveSpell(creature, ref activeSpellsOnPlayer)) {
					state = (int)State.CAST_PHASE;
					delayUpdate(0.5f);
				} else {
					delayUpdate(.7f);
				}
			} else {
				if (creature.activateActiveSpell(player, ref activeSpellsOnCreature)) {
					state = (int)State.CAST_PHASE;
					delayUpdate(0.5f);
				} else {
					delayUpdate(.7f);
				}
			}
			break;
		case (int)State.SPELL_ANIM:
				spellAnimComplete();
			break;
		case (int)State.CAST_PHASE:
			if (whoseTurn == (int)Turn.CREATURE) {
				creature.play(this,ref creature,ref player);
				
			}
			break;
		case (int)State.SPELL_EFFECT:
			// TODO: Combo logic here?
			castComboSpells();
			castedSpells.Clear();

			state = (int)State.ACTIVE_SPELL_EFFECT;
			delayUpdate(0.4f);
			if(whoseTurn == (int)Turn.PLAYER) {
				DungeonController.instance.switchTurn(false);
				passNextTurn();
			} else {
				DungeonController.instance.switchTurn(true);
			}
			whoseTurn = (whoseTurn + 1) % 2;
			break;
		case (int)State.END:
			// TODO: Implement next battle logic here
			break;
		}
	}
	
}