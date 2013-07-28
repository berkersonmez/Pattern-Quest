using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Battle : MonoBehaviour {
	
	public Player player;
	public Creature creature;
	public int turn;
	public int whoseTurn;
	public List <ActiveSpell> activeSpellsOnPlayer = new List<ActiveSpell>();
	public List <ActiveSpell> toDeleteSpellsOnPlayer = new List<ActiveSpell>();
	public List <ActiveSpell> activeSpellsOnCreature = new List<ActiveSpell>();
	public List <ActiveSpell> toDeleteSpellsOnCreature = new List<ActiveSpell>();
	
	public Battle(Player player, Creature creature){
		this.player = player;
		this.creature = creature;
		turn = 0;
		//whoseTurn: 0 demek oyuncunun turu, 1 demek npc nin turu demek
		whoseTurn = 0;
	}
	
	public void passNextTurn(List<Spell> castedSpells){
		if(whoseTurn == 0){
			foreach (ActiveSpell activeSpell in activeSpellsOnPlayer){
				if(activeSpell.effect(player) == false)
					this.toDeleteSpellsOnPlayer.Add(activeSpell);
			}
			foreach (Spell spell in castedSpells){
				spell.cast(this, player, creature);
			}
		}
		else{
			foreach (ActiveSpell activeSpell in activeSpellsOnCreature){
				if(activeSpell.effect(creature) == false)
					this.toDeleteSpellsOnCreature.Add(activeSpell);
			}
			foreach (Spell spell in castedSpells){
				spell.cast(this, player, creature);
			}
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
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}