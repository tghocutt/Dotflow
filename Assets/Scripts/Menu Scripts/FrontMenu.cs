using UnityEngine;
using System.Collections;

public class FrontMenu: MonoBehaviour {

	public UIButton playSurvivalButton;
	public UIButton playZenButton;
	public UIButton creditsButton;
	public UIButton quitButton;
	public UIButton settingsButton;


	public GameObject frontMenuRoot;
	public GameObject creditsMenuRoot;
	public GameObject optionsMenuRoot;

	//quits out of the running game
	public void ExitGame(GameObject go)
	{
		Application.Quit ();
	}


	private void OpenCredits(GameObject go)
	{
		frontMenuRoot.SetActive (false);
		creditsMenuRoot.SetActive (true);
	}


	private void OpenSettings(GameObject go)
	{
		frontMenuRoot.SetActive (false);
		optionsMenuRoot.SetActive (true);
	}


	private void PlayGame(GameObject go)
	{
		Application.LoadLevel (1);
	}
	
	
	private void Start()
	{
		//Main Menu Listeners
		UIEventListener.Get (playSurvivalButton.gameObject).onClick += PlayGame;
		UIEventListener.Get (playZenButton.gameObject).onClick += PlayGame;
		UIEventListener.Get (creditsButton.gameObject).onClick += OpenCredits;
		UIEventListener.Get (settingsButton.gameObject).onClick += OpenSettings;
		UIEventListener.Get (quitButton.gameObject).onClick += ExitGame;
	}
}
