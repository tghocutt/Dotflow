using UnityEngine;
using System.Collections;

public class DeathMenu : MonoBehaviour {

	public GameObject root;

	public UIButton restartButton;
	public UIButton quitButton;


	public void RestartGame(GameObject go)
	{
		root.SetActive(false);
		Application.LoadLevel("Main");
	}


	public void ExitGame(GameObject go)
	{
		root.SetActive(false);
		Application.Quit();
	}


	private void Start()
	{
		//Main Menu Listeners
		UIEventListener.Get (restartButton.gameObject).onClick += RestartGame;
		UIEventListener.Get (quitButton.gameObject).onClick += ExitGame;
	}
}
