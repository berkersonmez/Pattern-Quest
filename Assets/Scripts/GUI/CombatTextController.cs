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
	
	public void deployText(string name, int damage, int placement) {
		GameObject combatText = Instantiate(combatTextPrefab) as GameObject;
		combatText.transform.parent = GameObject.Find("AnchorUC").transform;
		float pX;
		if (placement == (int)Placement.PLAYER) {
			pX = -5.5f;
		} else {
			pX = 5.5f;
		}
		combatText.transform.localPosition = new Vector3(pX, -8f, -1f);
		tk2dTextMesh effectName = combatText.transform.Find("Name").GetComponent<tk2dTextMesh>();
		tk2dTextMesh efectDamage = combatText.transform.Find("Damage").GetComponent<tk2dTextMesh>();
		effectName.text = name;
		effectName.Commit();
		efectDamage.text = damage.ToString(); // Damage is taken directly. Should be calculated (take spellDamage etc.).
		efectDamage.Commit();
	}
}
