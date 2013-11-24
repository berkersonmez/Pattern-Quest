using UnityEngine;
using System.Collections;

public class Tooltip : MonoBehaviour {
	public static Tooltip instance;

	public int mode = 0; // 0: menu, 1: dungeon

	private tk2dTextMesh meshText;
	private tk2dSlicedSprite spriteBG;

	void Start () {
		instance = this;
		meshText = transform.Find("Text").GetComponent<tk2dTextMesh>();
		spriteBG = transform.Find("BG").GetComponent<tk2dSlicedSprite>();
		hideTooltip();
	}

	public void setText(string text) {
		meshText.text = text;
		meshText.Commit();
		float bgY = (meshText.GetEstimatedMeshBoundsForString(meshText.text).size.y / 1.46f) * 26.2f + 21.8f;
		spriteBG.dimensions = new Vector2(349,
		                                  bgY);
	}

	public void showTooltip(Vector3 position) {
		Vector3 globalPos = position;
		if (mode == 0) {
			position = globalPos - (Camera.main.transform.position - new Vector3(12f, 20f, 0f));
		} else if (mode == 1) {
			position = globalPos - Camera.main.transform.position;
		}
		meshText.gameObject.SetActive(true);
		spriteBG.gameObject.SetActive(true);
		float pixelX = position.x * 20;
		float pixelY = position.y * 20;
		if (pixelX + spriteBG.dimensions.x > 480) {
			pixelX = 480 - spriteBG.dimensions.x;
		}
		if (pixelY - spriteBG.dimensions.y < 0) {
			pixelY = spriteBG.dimensions.y;
		}
		if (mode == 0) {
			this.transform.localPosition = new Vector3(pixelX / 20 - 12f, pixelY / 20 - 20f, transform.localPosition.z);
		} else if (mode == 1) {
			this.transform.localPosition = new Vector3(pixelX / 20, pixelY / 20, transform.localPosition.z);
		}

	}

	public void hideTooltip() {
		meshText.gameObject.SetActive(false);
		spriteBG.gameObject.SetActive(false);
	}

}
