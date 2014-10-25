﻿using UnityEngine;
using System.Collections;

namespace Dotflow
{

	public class MainMenuScript : DotflowMenu {

		public DotflowElement[] mainMenuElements = new DotflowElement[0];
		public bool childrenMoving = false;

		public override void Open(DotflowElement[] elements)
		{
			base.Open (elements);
		}


		public override void Close(DotflowElement[] elements)
		{
			base.Close (elements);
		}
		


		private void Back(GameObject go)
		{
			if (!DotflowUIManager.isMenuMoving) 
			{
				Close (mainMenuElements);
				DotflowUIManager.isMenuActive = false;
				DotflowUIManager._dotManager.RestartGame();
				DotflowUIManager.HUD.Open (DotflowUIManager.HUD.hudElements);
			}
		}
		
		private void Settings(GameObject go)
		{
			if (!DotflowUIManager.isMenuMoving) {
				Close (mainMenuElements);
				DotflowUIManager.settingsMenu.Open (DotflowUIManager.settingsMenu.settingsMenuElements);
			}
		}

		private void Credits(GameObject go)
		{
			if (!DotflowUIManager.isMenuMoving) 
			{
				Close (mainMenuElements);
				DotflowUIManager.creditsMenu.Open(DotflowUIManager.creditsMenu.creditsMenuElements);
			}
		}

		private void Quit(GameObject go)
		{
			if (!DotflowUIManager.isMenuMoving) 
			{
				Application.Quit ();
			}
		}

		private void Menu(GameObject go)
		{
			if (!DotflowUIManager.isMenuMoving)
			{

			}
		}

		private void Play(GameObject go)
		{
			if (!DotflowUIManager.isMenuMoving) 
			{
				DotflowUIManager.isMenuActive = false;
				Close (mainMenuElements);
				DotflowUIManager.HUD.Open (DotflowUIManager.HUD.hudElements);
			}
		}


		private void Update()
		{
			bool moving = false;
			foreach(DotflowElement e in mainMenuElements)
			{
				if(e.amIMoving) moving = true;
				break;
			}
			childrenMoving = moving;
		}



		private void Start () {
			UIEventListener.Get (mainMenuElements[0].gameObject).onClick += Back;
			UIEventListener.Get (mainMenuElements[1].gameObject).onClick += Settings;
			UIEventListener.Get (mainMenuElements[2].gameObject).onClick += Credits;
			UIEventListener.Get (mainMenuElements[3].gameObject).onClick += Quit;
			UIEventListener.Get (mainMenuElements[4].gameObject).onClick += Menu;
			UIEventListener.Get (mainMenuElements[5].gameObject).onClick += Play;
		}
	}
}