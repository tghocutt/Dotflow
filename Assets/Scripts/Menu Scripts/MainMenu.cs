using UnityEngine;
using System.Collections;

namespace Dotflow
{
	public class MainMenu : MonoBehaviour {

		public AudioManager audioManager;
		public GUIManager guiManager;
		public GameObject root;

		public UIButton resumeButton;
		public UIButton settingsButton;
		public UIButton creditsButton;
		public UIButton quitButton;

		public void ResumeGame(GameObject go)
		{
			guiManager.guiActive = false;
			audioManager.menuFX[1].Play();
			root.SetActive (false);
			Time.timeScale = 1f;
		}
		
		public void OpenSettings(GameObject go)
		{
			audioManager.ButtonClick ();
			root.SetActive (false);
			guiManager.settingsMenuRoot.SetActive (true);
			guiManager.settingsMenuRoot.transform.parent.gameObject.GetComponent<SettingsMenu> ().GetFromPlayerPrefs ();
		}
		
		public void OpenCredits(GameObject go)
		{
			audioManager.ButtonClick ();
			root.SetActive (false);
			guiManager.creditsMenuRoot.SetActive (true);
		}

		public void ExitGame(GameObject go)
		{
			PlayerPrefs.Save ();

			audioManager.ButtonClick ();
			Application.Quit ();
		}

		private void Start()
		{
			//Main Menu Listeners
			UIEventListener.Get (resumeButton.gameObject).onClick += ResumeGame;
			UIEventListener.Get (settingsButton.gameObject).onClick += OpenSettings;
			UIEventListener.Get (creditsButton.gameObject).onClick += OpenCredits;
			UIEventListener.Get (quitButton.gameObject).onClick += ExitGame;
		}
	}
}