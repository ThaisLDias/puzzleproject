using UnityEngine;
using System.Collections;

public class Snap : MonoBehaviour {

	private string cuteChildGameObjectName;

	void Awake () {
		cuteChildGameObjectName = "";
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (cuteChildGameObjectName == "" || cuteChildGameObjectName == col.gameObject.name) {
			cuteChildGameObjectName = col.gameObject.name;
			col.gameObject.transform.position = new Vector2 (this.transform.position.x, this.transform.position.y + 0.5f);
		}
	}	

	void OnTriggerStay2D (Collider2D col) {
		if (cuteChildGameObjectName == "" || cuteChildGameObjectName == col.gameObject.name) {
			cuteChildGameObjectName = col.gameObject.name;
			col.gameObject.transform.position = new Vector2 (this.transform.position.x, this.transform.position.y + 0.5f);
		}
	}
	
	void OnTriggerExit2D (Collider2D col) {
		cuteChildGameObjectName = "";
	}
}