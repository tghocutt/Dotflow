﻿using UnityEngine;
using System.Collections;

namespace Dotflow
{
	public class GUIManager : MonoBehaviour {

		//HUD elements
		public UIButton menuButton;
		public UIButton pauseButton;
		public UIButton muteButton;

		public UILabel scoreLabel;


		//Main Menu Elements
		public GameObject mainMenuRoot;
		public GameObject creditsMenuRoot;
		public GameObject settingsMenuRoot;
		public GameObject deathMenuRoot;


		//objects to be managed
		public AudioListener audioListener;


		//lives manager(to let the player know how dead they are)
		public Lives lives;

		private bool paused = false;
		private bool muted = false;

		//lowers the time scale for a paused effect
		public void PauseGame(GameObject go)
		{
			if (paused) 
			{
				Time.timeScale = 0f;
				paused = !paused;
			} else {
				Time.timeScale = 1f;
				paused = !paused;
			}
		}


		//quits out of the running game
		public void MuteGame(GameObject go)
		{
			if (muted) 
			{
				audioListener.enabled = muted;
				muted = !muted;
			} else {
				audioListener.enabled = muted;
				muted = !muted;
			}
		}


		//opens the menu
		public void OpenMenu(GameObject go)
		{
			mainMenuRoot.SetActive (true);
			Time.timeScale = 0f;
		}



		private void Start()
		{
			//HID Listeners
			UIEventListener.Get (menuButton.gameObject).onClick += OpenMenu;
			UIEventListener.Get (pauseButton.gameObject).onClick += PauseGame;
			UIEventListener.Get (muteButton.gameObject).onClick += MuteGame;


		}
	}
}
