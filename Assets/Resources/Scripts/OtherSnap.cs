using UnityEngine;
using System.Collections;

public class OtherSnap : MonoBehaviour {
		
		private string cuteChildGameObjectName;

		private GameObject worm;
		private GameObject fish;
		private GameObject bird;
		public int hasHappened;
		
		void Awake () {
			cuteChildGameObjectName = "";
			fish = GameObject.Find("Fish");
			worm = GameObject.Find("Worm");
			bird = GameObject.Find("Bird");
		}
		
		void OnTriggerEnter2D (Collider2D col) {
			if (cuteChildGameObjectName == "" || cuteChildGameObjectName == col.gameObject.name) {
				cuteChildGameObjectName = col.gameObject.name;
				col.gameObject.transform.position = new Vector2 (this.transform.position.x, this.transform.position.y);
			}
		}	

		void OnTriggerStay2D (Collider2D col) {
			if (cuteChildGameObjectName == "" || cuteChildGameObjectName == col.gameObject.name) {
				cuteChildGameObjectName = col.gameObject.name;
				col.gameObject.transform.position = new Vector2 (this.transform.position.x, this.transform.position.y);
			}
		}
		
		void OnTriggerExit2D (Collider2D col) {
			cuteChildGameObjectName = "";
		}

		void Update(){
		hasHappened = SceneManager.pg_count;
		if (hasHappened < 800) {
			fish.SetActive (false);
			worm.SetActive (false);
			bird.SetActive (true);
		}

		if (hasHappened > 800 && hasHappened < 1600) {
			bird.SetActive(false);
			worm.SetActive(true);
		}

		if (hasHappened > 1600) {
			worm.SetActive(false);
			fish.SetActive(true);
		}
	}
}
























