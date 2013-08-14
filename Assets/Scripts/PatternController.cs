using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PatternController : MonoBehaviour {
	
	public static PatternController instance;
	
	public GameObject buttonPrefab;
	public GameObject bridgePrefab;
	public PatternButton[,] buttons = new PatternButton[4,4];
	public List<int[]> pattern = new List<int[]>();
	public List<int> directions = new List<int>();
	public List<GameObject> bridges = new List<GameObject>();
	Creature creature = new Creature();
		
	
	void Start() {
		instance = this;
		createButtons();
		creature = XmlParse.instance.getCreature("ufak");
	}
	
	private void createButtons() {
		tk2dCameraAnchor anchor = GameObject.Find("AnchorLL").GetComponent<tk2dCameraAnchor>();
		for (int i = 0 ; i < 4 ; i++) {
			for (int j = 0 ; j < 4 ; j++) {
				GameObject obj = Instantiate(buttonPrefab) as GameObject;
				obj.transform.parent = anchor.transform;
				obj.transform.localPosition = new Vector3(j * 3.5f - 5.25f, 2.5f + i * 3.5f, 0);
				buttons[i,j] = obj.GetComponent<PatternButton>();
				buttons[i,j].x = i;
				buttons[i,j].y = j;
			}
		}
	}
	
	public void addToPattern(int x, int y) {
		int[] newNode = {x, y};
		// Next pattern node should be adjacent
		if (pattern.Count != 0) {
			int[] lastElement = pattern[pattern.Count - 1];
			int bridgeCount = 0;
			if (buttons[x,y].highlighted) {
				if (areNodesEqual(newNode, lastElement)) {
					return;
				}
				bridgeCount = countBridges(newNode, lastElement);
			}
			if (Mathf.Abs(lastElement[0] - x) <= 1 && Mathf.Abs(lastElement[1] - y) <= 1) {
				buttons[x,y].highlight();
				pattern.Add(newNode);
				drawBridgeBetween(lastElement, newNode, bridgeCount);
			}
		} else {
			buttons[x,y].highlight();
			pattern.Add(newNode);
		}
	}
	
	public List<int> convertToDirectional() {
		List<int> dirRep = new List<int>();
		for (int i = 1 ; i < pattern.Count ; i++) {
			int[] dirDiff = new int[2];
			dirDiff[0] = pattern[i][0] - pattern[i-1][0];
			dirDiff[1] = pattern[i][1] - pattern[i-1][1];
			dirRep.Add(getDirectionInt(dirDiff));
		}
		return dirRep;
	}
	
	// Coordinate diff. representation to int representation
	public int getDirectionInt(int[] dirDiff) {
		if (dirDiff[0] == 0) {
			switch (dirDiff[1]) {
			case 1:
				return 6;
			case -1:
				return 4;
			}
		}
		if (dirDiff[0] == 1) {
			switch (dirDiff[1]) {
			case 1:
				return 9;
			case 0:
				return 8;
			case -1:
				return 7;
			}
		}
		if (dirDiff[0] == -1) {
			switch (dirDiff[1]) {
			case 1:
				return 3;
			case 0:
				return 2;
			case -1:
				return 1;
			}
		}
		return -1;
	}
	
	public bool areNodesEqual(int[] n1, int[] n2) {
		if (n1[0] == n2[0] && n1[1] == n2[1]) {
			return true;
		}
		return false;
	}
	
	// Not used
	public bool isBridgePresent(int[] n1, int[] n2) {
		for(int i = 0 ; i < pattern.Count ; i++) {
			if (areNodesEqual(pattern[i], n1)) {
				if (i-1 >= 0 && areNodesEqual(pattern[i-1], n2)) {
					return true;
				}
				if (i+1 < pattern.Count && areNodesEqual(pattern[i+1], n2)) {
					return true;
				}
			}
		}
		return false;
	}
	
	// Count present bridges for coloring.
	public int countBridges(int[] n1, int[] n2) {
		int n = 0;
		for(int i = 0 ; i < pattern.Count ; i++) {
			// If n == 2 no need to count more.
			if (n == 2) return n;
			if (areNodesEqual(pattern[i], n1)) {
				if (i-1 >= 0 && areNodesEqual(pattern[i-1], n2)) {
					n++;
				} 
				if (i+1 < pattern.Count && areNodesEqual(pattern[i+1], n2)) {
					n++;
				}
			}
		}
		return Mathf.Clamp(n, 0, 2);
	}
	
	public void drawBridgeBetween(int[] n1, int[] n2, int bridgeCount) {
		tk2dCameraAnchor anchor = GameObject.Find("AnchorLL").GetComponent<tk2dCameraAnchor>();
		GameObject obj = Instantiate(bridgePrefab) as GameObject;
		obj.transform.parent = anchor.transform;
		Vector3 n1Pos = buttons[n1[0], n1[1]].transform.localPosition;
		Vector3 n2Pos = buttons[n2[0], n2[1]].transform.localPosition;
		obj.transform.localPosition = (n1Pos + n2Pos) / 2;
		obj.transform.localPosition += new Vector3(0, 0, 1f - bridgeCount * .1f);
		string bridgeColorStr = (bridgeCount + 1) + "_";
		obj.GetComponent<tk2dSprite>().SetSprite("pattern_bridge_" + bridgeColorStr + bridgeType(n1, n2));
		bridges.Add(obj);
	}
	
	public string bridgeType(int[] n1, int[] n2) {
		int xd = n1[0] - n2[0];
		int yd = n1[1] - n2[1];
		if (xd == 0) {
			return "v";
		} else if (yd == 0) {
			return "h";
		} else if ((xd < 0 && yd < 0) || (xd > 0 && yd > 0)) {
			return "d1";
		} else {
			return "d2";
		}
	}
	
	public void finishPattern() {
		Debug.Log("====PATTERN====");
		foreach(int[] node in pattern) {
			buttons[node[0], node[1]].unhighlight();
			Debug.Log("(x:" + node[0] + "y:" + node[1] + ")");
		}
		Debug.Log("===============");
		
		// Debug this to see directional rep.
		directions = this.convertToDirectional();
		
		pattern.Clear();
		foreach(GameObject bridge in bridges) {
			Destroy(bridge);
		}
		bridges.Clear();
		//Example values to test checkspell method
		Spell spell = getCastedSpell(DungeonController.instance.player);
		if(spell != null)
			DungeonController.instance.battle.castSpell(spell);
	}
	
	public Spell getCastedSpell(Player player) {
		foreach(Spell spell in player.spellList){
			if(isEqual(directions , spell.shape)){
				Debug.Log(spell.name);
				return spell;
			}
		}
		return null;
	}
	
	//I added this method because couldn't find a proper way to check equality of two lists
	public bool isEqual(List<int> list1, List<int> list2){
		if(list1.Count == list2.Count){
			for(int i=0; i<list1.Count; i++)
				if(list1[i] != list2[i])
					return false;
			return true;
		}else
			return false;
		}
}
