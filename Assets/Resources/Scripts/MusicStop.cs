using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MusicStop : MonoBehaviour {

	public bool isPlaying;
	private Sprite on;
	private Sprite off;

	void Start() {
		isPlaying = false;
		ToggleMusic ();

		on = Resources.Load<Sprite>("Sprites/on");
		off = Resources.Load<Sprite>("Sprites/off");
	}

	public void ToggleMusic() {
		if (isPlaying) {
			Music.StopMusic ();
			isPlaying = false;

			GameObject.FindGameObjectWithTag("soundImage").GetComponent<Image>().overrideSprite = off;
		} else {
			Music.PlayMusic ();
			isPlaying = true;

			GameObject.FindGameObjectWithTag("soundImage").GetComponent<Image>().overrideSprite = on;
		}
	} 
}
