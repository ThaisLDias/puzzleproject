using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomSquad : MonoBehaviour {

	public List<GameObject> spriteList = new List<GameObject>();
	public List<GameObject> placeList = new List<GameObject>();
	public List<float> position = new List<float>();
	public List<float> position2 = new List<float>();
	public int eg_Count; 
	private int eg_limit;
	private bool call;

	// Use this for initialization
	void Start () {

		eg_Count = 0;
		eg_limit = 15;
		StartRand ();
	}

	void StartRand()
	{
		int j = 0;

		while (position.Count > 0) {
			int rdm = Random.Range(0, position.Count);
			int rdm2 = Random.Range(0, position2.Count);

			Instantiate(spriteList[j],new Vector2(-5.46f, position[rdm]),  Quaternion.identity);
			Instantiate(placeList[j] ,new Vector2(5.53f,  position2[rdm2]),Quaternion.identity);

			position.Remove(position[rdm]);
			position2.Remove(position2[rdm2]);

			j++;
		}
	}

	void Update(){
		if(eg_Count == 3){
		if (PlayerPrefs.GetInt("eg_Count") >= eg_limit) {
			Application.LoadLevel (1);
		} 
		else {
			PlayerPrefs.SetInt("eg_Count",PlayerPrefs.GetInt("eg_Count") + eg_Count);
			Application.LoadLevel(Application.loadedLevel);
			}
		}
	}


}
