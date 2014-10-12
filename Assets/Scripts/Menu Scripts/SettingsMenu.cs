using UnityEngine;
using System.Collections;

namespace Dotflow
{
	public class SettingsMenu : MonoBehaviour {

		public AudioManager audioManager;
		public GUIManager guiManager;
		public GameObject root;

		public UIButton backButton;
		public UISlider volumeSlider;
		public SettingsKnobs settingsKnobs;

		public void GoBack(GameObject go)
		{
			settingsKnobs.SetValues ();
			PlayerPrefs.SetFloat ("volume", volumeSlider.value);

			audioManager.ButtonClick ();
			root.SetActive (false);
			guiManager.mainMenuRoot.SetActive (true);
		}

		private void FixedUpdate()
		{
			AudioListener.volume = 1.0f - volumeSlider.value;
		}

		private void Start()
		{
			GetFromPlayerPrefs ();
			UIEventListener.Get (backButton.gameObject).onClick += GoBack;
		}

		public void GetFromPlayerPrefs() {
			if (PlayerPrefs.HasKey ("volume"))
				volumeSlider.value = PlayerPrefs.GetFloat ("volume");
		}
	}
}