using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Avatar : MonoBehaviour {

	public bool updateEveryFrame = true;

	public Creature owner;
	private tk2dUIProgressBar healthBar;
	private tk2dTextMesh healthText;
	private tk2dUIProgressBar manaBar;
	private tk2dTextMesh manaText;
	private tk2dTextMesh nameText;
	private tk2dSprite avatarSprite;
	private GameObject activeSpellsList;

	public GameObject activeSpellCounterPrefab;
	
	void Start () {
		healthBar = transform.Find("Healthbar").GetComponent<tk2dUIProgressBar>();
		healthText = healthBar.transform.Find("Background").Find("ProgressBarHighlight").Find("Text").GetComponent<tk2dTextMesh>();
		manaBar = transform.Find("Manabar").GetComponent<tk2dUIProgressBar>();
		manaText = manaBar.transform.Find("Background").Find("ProgressBarHighlight").Find("Text").GetComponent<tk2dTextMesh>();
		nameText = transform.Find("Text").GetComponent<tk2dTextMesh>();
		avatarSprite = transform.Find("Avatar").GetComponent<tk2dSprite>();
		activeSpellsList = transform.Find("ActiveSpells").gameObject;
		owner = null;
	}
	
	public void deadAnim() {
		avatarSprite.color = new Color(.2f, .2f, .2f);
	}
	
	public void setOwner(Creature owner) {
		this.owner = owner;
		nameText.text = owner.name;
		nameText.Commit();
		avatarSprite.SetSprite(owner.spriteName);
		avatarSprite.color = new Color(1f, 1f, 1f);
		// Set avatarSprite here.
	}
	
	public void visualizeTurn(bool isOwnersTurn) {
		if (isOwnersTurn) {
			nameText.color = Color.yellow;
			nameText.Commit();
		} else {
			nameText.color = Color.white;
			nameText.Commit();
		}
	}

	public void updateActiveSpellVisuals() {
		// Draw active spells here.
		foreach(Transform child in activeSpellsList.transform) {
			Destroy(child.gameObject);
		}
		Queue<ActiveSpell> activeSpells;
		if (owner.isPlayer) {
			activeSpells =  DungeonController.instance.battle.activeSpellsOnPlayer;
		} else {
			activeSpells =  DungeonController.instance.battle.activeSpellsOnCreature;
		}
		int i = 0;
		foreach (ActiveSpell activeSpell in activeSpells) {
			GameObject asEntry = Instantiate(activeSpellCounterPrefab) as GameObject;
			asEntry.transform.parent = activeSpellsList.transform;
			asEntry.transform.localPosition = new Vector3(0.6f + (i % 3) * 2.6f,-1f + (i / 3) * -3.2f, 0f);
			SpellHolder holder = asEntry.GetComponent<SpellHolder>();
			holder.setSpell(activeSpell.spell);
			tk2dTextMesh countText = asEntry.transform.Find("Text").GetComponent<tk2dTextMesh>();
			countText.text = activeSpell.remainingTurn.ToString();
			countText.Commit();
			i++;
		}
	}

	public void updateHealthAndMana() {
		if (owner != null) {
			manaBar.Value = (float)owner.currentMana / owner.Mana;
			manaText.text = owner.currentMana + "/" + owner.Mana;
			manaText.Commit();
			healthBar.Value = (float)owner.currentHp / owner.Hp;
			healthText.text = owner.currentHp + "/" + owner.Hp;
			healthText.Commit();
		}
	}

	void Update () {
		if (updateEveryFrame) {
			updateHealthAndMana();
		}
	}
}
