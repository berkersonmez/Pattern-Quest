using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Soomla
{
	public class StorefrontController
	{
#if UNITY_IOS && !UNITY_EDITOR
		[DllImport ("__Internal")]
		private static extern int storefrontController_Initialize();
		[DllImport ("__Internal")]
		private static extern int storefrontController_InitializeWithVersion(int sfVersion);
		[DllImport ("__Internal")]
		private static extern int storefrontController_OpenStore();
		[DllImport ("__Internal")]
		private static extern int storefrontController_OpenStoreToItemId(string itemId);
#endif

		public static void Initialize() {
			StoreController.SetupSoomSec();
			
#if UNITY_ANDROID && !UNITY_EDITOR
			AndroidJNI.PushLocalFrame(100);
			using(AndroidJavaClass jniStoreFront = new AndroidJavaClass("com.soomla.unity.Storefront")) {
				jniStoreFront.CallStatic("initialize");
			}
			AndroidJNI.PopLocalFrame(IntPtr.Zero);
#elif UNITY_IOS && !UNITY_EDITOR
			storefrontController_Initialize();
#endif
		}
		
		public static void Initialize(int sfVersion) {
			StoreController.SetupSoomSec();
			
#if UNITY_ANDROID && !UNITY_EDITOR
			AndroidJNI.PushLocalFrame(100);
			using(AndroidJavaClass jniStoreFront = new AndroidJavaClass("com.soomla.unity.Storefront")) {
				jniStoreFront.CallStatic("initialize", sfVersion);
			}
			AndroidJNI.PopLocalFrame(IntPtr.Zero);
#elif UNITY_IOS && !UNITY_EDITOR
			storefrontController_InitializeWithVersion(sfVersion);
#endif
		}
		
		public static void OpenStore() {
#if UNITY_ANDROID && !UNITY_EDITOR
			AndroidJNI.PushLocalFrame(100);
			using(AndroidJavaClass jniStoreFront = new AndroidJavaClass("com.soomla.unity.Storefront")) {
				jniStoreFront.CallStatic("openStore", Application.unityVersion);
			}
			AndroidJNI.PopLocalFrame(IntPtr.Zero);
#elif UNITY_IOS && !UNITY_EDITOR
			storefrontController_OpenStore();
#endif
		}


		public static void OpenStoreToItemId(string itemId) {
#if UNITY_ANDROID && !UNITY_EDITOR
			AndroidJNI.PushLocalFrame(100);
			using(AndroidJavaClass jniStoreFront = new AndroidJavaClass("com.soomla.unity.Storefront")) {
				jniStoreFront.CallStatic("openStore", itemId, Application.unityVersion);
			}
			AndroidJNI.PopLocalFrame(IntPtr.Zero);
#elif UNITY_IOS && !UNITY_EDITOR
			storefrontController_OpenStoreToItemId(itemId);
#endif
		}

	}
}
