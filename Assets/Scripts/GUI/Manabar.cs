using UnityEngine;
using System.Collections;

public class Manabar : MonoBehaviour {

	public Creature owner;
	private tk2dUIProgressBar progressBar;
	private tk2dTextMesh text;
	
	void Start() {
		progressBar = GetComponent<tk2dUIProgressBar>();
		text = transform.Find("Background").Find("ProgressBarHighlight").Find("Text").GetComponent<tk2dTextMesh>();
	}
	
	void Update () {
		if (owner != null) {
			progressBar.Value = (float)owner.currentMana / owner.mana;
			text.text = owner.currentMana + "/" + owner.mana;
			text.Commit();
		}
	}
}
