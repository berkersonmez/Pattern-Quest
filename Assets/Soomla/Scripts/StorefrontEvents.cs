using System;
using UnityEngine;

namespace Soomla
{
	public class StorefrontEvents : MonoBehaviour 
	{
        private const string TAG = "SOOMLA StorefrontEvents";
        private static StorefrontEvents instance = null;
        
        void Awake(){
            if(instance == null){     //making sure we only initialize one instance.
                instance = this;
                GameObject.DontDestroyOnLoad(this.gameObject);
            } else {                    //Destroying unused instances.
                GameObject.Destroy(this.gameObject);
            }
        }
        
        public void onClosingStore(string message) {
            StoreUtils.LogDebug(TAG, "SOOMLA/UNITY onClosingStore");

            StorefrontEvents.OnClosingStore();
        }

        public void onOpeningStore(string message) {
            StoreUtils.LogDebug(TAG, "SOOMLA/UNITY onOpeningStore");

            StorefrontEvents.OnOpeningStore();
        }
        
        public delegate void Action();

		public static Action OnClosingStore = delegate {};

		public static Action OnOpeningStore = delegate {};
		

	}
}
