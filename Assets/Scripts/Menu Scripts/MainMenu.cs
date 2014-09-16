﻿using UnityEngine;
using System.Collections;

namespace Dotflow
{
	public class MainMenu : MonoBehaviour {
		
		public GUIManager guiManager;

		public GameObject root;

		public UIButton resumeButton;
		public UIButton settingsButton;
		public UIButton creditsButton;
		public UIButton quitButton;


		//resumes the game
		public void ResumeGame(GameObject go)
		{
			root.SetActive (false);
			Time.timeScale = 1f;
		}
		
		//opens settings menu
		public void OpenSettings(GameObject go)
		{
			root.SetActive (false);
			guiManager.settingsMenuRoot.SetActive (true);
		}
		
		
		public void OpenCredits(GameObject go)
		{
			root.SetActive (false);
			guiManager.creditsMenuRoot.SetActive (true);
		}


		//quits out of the running game
		public void ExitGame(GameObject go)
		{
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
