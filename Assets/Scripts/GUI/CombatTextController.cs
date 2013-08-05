using UnityEngine;
using System.Collections;

public class CombatTextController : MonoBehaviour {
	public enum Placement {PLAYER = 0, CREATURE};
	
	public static CombatTextController instance;
	
	public GameObject combatTextPrefab;

	// Use this for initialization
	void Start () {
		instance = this;
	}
	
	public void deployText(int damage, int placement) {
		GameObject combatText = Instantiate(combatTextPrefab) as GameObject;
		combatText.transform.parent = GameObject.Find("AnchorUC").transform;
		float pX;
		if (placement == (int)Placement.PLAYER) {
			pX = -5.5f;
		} else {
			pX = 5.5f;
		}
		combatText.transform.localPosition = new Vector3(pX, -8f, -1f);
		combatText.GetComponent<CombatText>().setText("-" + damage);
	}
}
