using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PatternController : MonoBehaviour {
	
	public static PatternController instance;
	
	public GameObject buttonPrefab;
	public PatternButton[,] buttons = new PatternButton[4,4];
	public List<int[]> pattern = new List<int[]>();
	
	void Start() {
		instance = this;
		createButtons();
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
		if (buttons[x,y].highlighted) {
			return;
		}
		// Next pattern node should be adjacent
		if (pattern.Count != 0) {
			int[] lastElement = pattern[pattern.Count - 1];
			if (Mathf.Abs(lastElement[0] - x) <= 1 && Mathf.Abs(lastElement[1] - y) <= 1) {
				buttons[x,y].highlight();
				int[] newNode = {x, y};
				pattern.Add(newNode);
			}
		} else {
			buttons[x,y].highlight();
			int[] newNode = {x, y};
			pattern.Add(newNode);
		}
	}
	
	public void finishPattern() {
		Debug.Log("====PATTERN====");
		foreach(int[] node in pattern) {
			buttons[node[0], node[1]].unhighlight();
			Debug.Log("(x:" + node[0] + "y:" + node[1] + ")");
		}
		Debug.Log("===============");
		pattern.Clear();
	}
	
}
