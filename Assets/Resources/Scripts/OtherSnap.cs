using UnityEngine;
using System.Collections;

public class OtherSnap : MonoBehaviour {
		
		private string cuteChildGameObjectName;
		public static int count;
		public static bool completed = false;
		private GameObject worm;
		private GameObject fish;
		private GameObject bird;
		
		void Awake () {
			cuteChildGameObjectName = "";
			count = 0;
			completed = false;
			fish = GameObject.Find("Fish");
			worm = GameObject.Find("Worm");
			bird = GameObject.Find("Bird");
			fish.SetActive (false);
			worm.SetActive (false);
			bird.SetActive (true);
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
				count++;
			}
		}
		
		void OnTriggerExit2D (Collider2D col) {
			cuteChildGameObjectName = "";
		}

		void Update(){
		if (count == 2) {
			completed = true;
		}
		if (completed) {
			bird.SetActive(false);
			fish.SetActive(true);
		}
	}
}
























