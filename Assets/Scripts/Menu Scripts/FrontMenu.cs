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
	public GameObject background;
	public GameObject[] loadingScreens = new GameObject[0];

	//quits out of the running game
	public void ExitGame(GameObject go)
	{
		PlayerPrefs.DeleteKey ("ZenModeOn"); /* since this is more for internal use, it doesn't need to be saved to disk */
		PlayerPrefs.Save (); /* saves all options before the application quits  */

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
		background.SetActive (false);
		optionsMenuRoot.SetActive (true);
		optionsMenuRoot.transform.parent.gameObject.GetComponent<FrontSettingsMenu>().GetFromPlayerPrefs ();
	}

	private void PlayGameSurvival(GameObject go)
	{
		frontMenuRoot.SetActive (false);
		background.SetActive (false);
		int randy = Mathf.RoundToInt (Random.Range (0, loadingScreens.Length));
		loadingScreens [randy].SetActive (true);
		PlayerPrefs.SetInt ("ZenModeOn",0);
		Application.LoadLevel (1);
	}

	private void PlayGameZen(GameObject go)
	{
		frontMenuRoot.SetActive (false);
		int randy = Mathf.RoundToInt (Random.Range (0, loadingScreens.Length));
		loadingScreens [randy].SetActive (true);
		PlayerPrefs.SetInt ("ZenModeOn",1);
		Application.LoadLevel (1);
	}
	
	private void Start()
	{
		//Main Menu Listeners
		UIEventListener.Get (playSurvivalButton.gameObject).onClick += PlayGameSurvival;
		UIEventListener.Get (playZenButton.gameObject).onClick += PlayGameZen;
		UIEventListener.Get (creditsButton.gameObject).onClick += OpenCredits;
		UIEventListener.Get (settingsButton.gameObject).onClick += OpenSettings;
		UIEventListener.Get (quitButton.gameObject).onClick += ExitGame;
	}
}