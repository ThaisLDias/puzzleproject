using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class PhpConnect : MonoBehaviour {
	public string myUrl = "http://pupper.esy.es/insertScore.php"; 
	public string nome;
	public int acertos;
	public int erros;
	public int tentativas;

	void Start()
	{
//		nome = GetComponen
	}
	public void ButtonClick()
	{
		StartCoroutine (SendHighScore (nome, acertos, erros, tentativas));
	}
	
	IEnumerator SendHighScore(string _player, int _acertos,int _erros,int _tentativas)
	{
		WWWForm form = new WWWForm();
		if (_player == "" || _player == null) {
			_player = "Autista";
		}
		form.AddField("nome", _player);
		form.AddField("acertos", _acertos); 
		form.AddField("erros", _erros);
		form.AddField("resolvidos", _tentativas);

		
		WWW www = new WWW(myUrl,form);
		yield return www;
		
		//Application.LoadLevel (Application.loadedLevel);
		
	}
}
