using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class PhpConnect : MonoBehaviour {
	private string insertUrl = "http://pupper.esy.es/insertScore.php";
	private string updateUrl = "http://pupper.esy.es/updateScore.php"; 
	public string nome;
	public int acertos;
	public int erros;
	public int tentativas;

	public void UpdateHighScore(string nome, int acertos, int erros)
	{
		StartCoroutine (_UpdateHighScore (nome, acertos, erros));
	}

	public IEnumerator _UpdateHighScore(string _player, int _acertos, int _erros)
	{
		WWWForm form = new WWWForm();
		if (_player == "" || _player == null) {
			_player = "Criança";
		}
		form.AddField("nome", _player);
		form.AddField("acertos", _acertos); 
		form.AddField("erros", _erros);
		form.AddField("resolvidos", 4);
		
		
		WWW www = new WWW(updateUrl, form);
		yield return www;
		
	}
}
