using UnityEngine;
using System.Collections;
using hmm.Soomla;
using Soomla;

public class StoreManager : MonoBehaviour {
	public static StoreManager instance;
	public Store StoreAssets {get; set;}

	void Awake() {
		instance = this;
	}
	
	void Start () {
		StoreAssets = new Store();
		StoreController.Initialize(StoreAssets);
	}
	
	public void openStore() {
		StorefrontController.OpenStore();
	}
	
}
