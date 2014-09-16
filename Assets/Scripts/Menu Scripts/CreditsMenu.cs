using UnityEngine;
using System.Collections;

namespace Dotflow
{
	public class CreditsMenu : MonoBehaviour {
		
		public GUIManager guiManager;

		public GameObject root;

		public UIButton backButton;



		public void GoBack(GameObject go)
		{
			root.SetActive (false);
			guiManager.mainMenuRoot.SetActive (true);
		}


		private void Start()
		{
			UIEventListener.Get (backButton.gameObject).onClick += GoBack;
		}
	}
}

