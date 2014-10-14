using UnityEngine;
using System.Collections;

namespace Dotflow
{
	public class CreditsMenu : MonoBehaviour {

		public AudioManager audioManager;
		public GUIManager guiManager;

		public GameObject root;
		public UIButton backButton;

		public void GoBack(GameObject go)
		{
			PlayerPrefs.SetInt ("creditsWatched", 1);
			PlayerPrefs.Save ();

			audioManager.ButtonClick ();
			root.SetActive (false);
			guiManager.mainMenuRoot.SetActive (true);
		}

		private void Start()
		{
			UIEventListener.Get (backButton.gameObject).onClick += GoBack;
		}
	}
}