using UnityEngine;
using System.Collections;

public class QuizCategoriesController : MonoBehaviour {
	public static QuizCategoriesController instance;
	
	public GameObject buttonPrefab;
	
	void Awake() {
		instance = this;
	}
	
	void Start() {
		foreach(Transform child in this.transform) {
			Destroy(child.gameObject);
		}
		string[] categories = XmlParse.instance.getCategories();
		int i = 0;
		foreach (string category in categories) {
			GameObject button = Instantiate(buttonPrefab) as GameObject;
			button.transform.parent = this.transform;
			button.transform.localPosition = new Vector3(0f, -3f * i, 0f);
			QuizCategoryButton holder = button.GetComponent<QuizCategoryButton>();
			holder.setCategory(category);
			i++;
		}
	}
}