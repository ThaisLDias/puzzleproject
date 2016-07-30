using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MusicStop : MonoBehaviour {


	private Sprite on;
	private Sprite off;

	void Start(){
	
		on = Resources.Load<Sprite>("Sprites/on");
		off = Resources.Load<Sprite>("Sprites/off");
	}

	public void StopMusic(){
		Music.ToggleMusic ();
		
	}

	public void OnMouseDown()
	{
		if (Music.soundOn) {
			GameObject.FindGameObjectWithTag("soundImage").GetComponent<Image>().overrideSprite = on;
		}
		else {
			GameObject.FindGameObjectWithTag("soundImage").GetComponent<Image>().overrideSprite = off;
		}
	} 
}
