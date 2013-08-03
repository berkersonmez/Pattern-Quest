using UnityEngine;
using System.Collections;

public class CombatTextController : MonoBehaviour {
	
	public static CombatTextController instance;
	
	public GameObject combatTextPrefab;

	// Use this for initialization
	void Start () {
		instance = this;
	}
	
	public void deployText(int damage) {
		GameObject combatText = Instantiate(combatTextPrefab) as GameObject;
		combatText.transform.parent = GameObject.Find("AnchorUC").transform;
		combatText.transform.localPosition = new Vector3(5.5f, -8f, -1f);
		combatText.GetComponent<CombatText>().setText("-" + damage);
	}
}
