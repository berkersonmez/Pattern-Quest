using UnityEngine;
using System.Collections;

public class SaveSlotsController : MonoBehaviour {
	public static SaveSlotsController instance;

	void Awake () {
		instance = this;
	}
	
	public void loadSaveSlots() {
		int i = 1;
		foreach (Transform slot in transform) {
			SaveSlot saveSlot = slot.GetComponent<SaveSlot>();
			saveSlot.init(i);
			saveSlot.load();
			i++;
		}
	}
}
