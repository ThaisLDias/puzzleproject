 using UnityEngine;
using System.Collections;

public class Snap : MonoBehaviour {

	private string cuteChildGameObjectName;
	public bool snapped;

	void Awake () {
		cuteChildGameObjectName = "";
		snapped = false;
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (cuteChildGameObjectName == "" || cuteChildGameObjectName == col.gameObject.name) {
			cuteChildGameObjectName = col.gameObject.name;
			col.gameObject.transform.position = new Vector2 (this.transform.position.x, this.transform.position.y + 0.5f);
			col.gameObject.GetComponent<Draggable>().snapped = true;
		}
	}	

	void OnTriggerStay2D (Collider2D col) {
		if (cuteChildGameObjectName == "" || cuteChildGameObjectName == col.gameObject.name) {
			cuteChildGameObjectName = col.gameObject.name;
			col.gameObject.transform.position = new Vector2 (this.transform.position.x, this.transform.position.y + 0.5f);
			col.gameObject.GetComponent<Draggable>().snapped = true;
		}
	}
	
	void OnTriggerExit2D (Collider2D col) {
		cuteChildGameObjectName = "";
		col.gameObject.GetComponent<Draggable>().snapped = false;
	}
}