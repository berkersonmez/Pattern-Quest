using UnityEngine;
using System.Collections;

public class Healthbar : MonoBehaviour {
	public Creature owner;
	private tk2dUIProgressBar progressBar;
	private tk2dTextMesh text;
	
	void Start() {
		progressBar = GetComponent<tk2dUIProgressBar>();
		text = transform.Find("Background").Find("ProgressBarHighlight").Find("Text").GetComponent<tk2dTextMesh>();
	}
	
	void Update () {
		if (owner != null) {
			progressBar.Value = (float)owner.currentHp / owner.hp;
			text.text = owner.currentHp + "/" + owner.hp;
			text.Commit();
		}
	}
}
