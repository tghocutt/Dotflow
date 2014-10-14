using UnityEngine;
using System.Collections;

public class DeathMenu : MonoBehaviour {

	public AudioManager audioManager;

	public GameObject root;

	public UIButton restartButton;
	public UIButton quitButton;


	public void RestartGame(GameObject go)
	{
		audioManager.ButtonClick ();
		root.SetActive(false);
		Application.LoadLevel(Application.loadedLevel);
	}


	public void ExitGame(GameObject go)
	{
		audioManager.ButtonClick ();
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
