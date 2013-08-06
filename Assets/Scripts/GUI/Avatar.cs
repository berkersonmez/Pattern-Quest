using UnityEngine;
using System.Collections;

public class Avatar : MonoBehaviour {
	
	public Creature owner;
	private tk2dUIProgressBar healthBar;
	private tk2dTextMesh healthText;
	private tk2dUIProgressBar manaBar;
	private tk2dTextMesh manaText;
	private tk2dTextMesh nameText;
	private tk2dSprite avatarSprite;
	
	void Start () {
		healthBar = transform.Find("Healthbar").GetComponent<tk2dUIProgressBar>();
		healthText = healthBar.transform.Find("Background").Find("ProgressBarHighlight").Find("Text").GetComponent<tk2dTextMesh>();
		manaBar = transform.Find("Manabar").GetComponent<tk2dUIProgressBar>();
		manaText = manaBar.transform.Find("Background").Find("ProgressBarHighlight").Find("Text").GetComponent<tk2dTextMesh>();
		nameText = transform.Find("Text").GetComponent<tk2dTextMesh>();
		avatarSprite = transform.Find("Avatar").GetComponent<tk2dSprite>();
	}
	
	public void deadAnim() {
		
		avatarSprite.color = new Color(.2f, .2f, .2f);
	}
	
	public void setOwner(Creature owner) {
		this.owner = owner;
		nameText.text = owner.name;
		nameText.Commit();
		avatarSprite.SetSprite(owner.spriteName);
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
	
	void Update () {
		if (owner != null) {
			manaBar.Value = (float)owner.currentMana / owner.mana;
			manaText.text = owner.currentMana + "/" + owner.mana;
			manaText.Commit();
			healthBar.Value = (float)owner.currentHp / owner.hp;
			healthText.text = owner.currentHp + "/" + owner.hp;
			healthText.Commit();
		}
	}
}
