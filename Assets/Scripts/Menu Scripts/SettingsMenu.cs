using UnityEngine;
using System.Collections;


namespace Dotflow
{
	public class SettingsMenu : MonoBehaviour {

		public GUIManager guiManager;

		public GameObject root;

		public UIButton backButton;

		public UISlider volumeSlider;



		public void GoBack(GameObject go)
		{
			root.SetActive (false);
			guiManager.mainMenuRoot.SetActive (true);
		}


		private void FixedUpdate()
		{
			AudioListener.volume = 1.0f - volumeSlider.value;
		}


		private void Start()
		{
			UIEventListener.Get (backButton.gameObject).onClick += GoBack;
		}
	}
}
