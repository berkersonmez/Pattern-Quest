using UnityEngine;
using System.Collections;

public class GUIController : MonoBehaviour {
	
	public static GUIController instance;
	
	bool mouseRaycast = false;
	
	void Start() {
		instance = this;
		mouseRaycast = false;
	}
	
	void Update() {
		if (Input.GetButtonDown("Fire1")) {
			mouseRaycast = true;
			// Hide tooltips on click.
			Tooltip.instance.hideTooltip();
		} else if (Input.GetButtonUp("Fire1")) {
			mouseRaycast = false;
			PatternController.instance.finishPattern();
		}
		
		if (mouseRaycast) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 100)) {
				PatternButton hitButton = hit.collider.GetComponent<PatternButton>();
				if (hitButton != null) {
					hitButton.onHover();
				}
			}
		}
	}
}
