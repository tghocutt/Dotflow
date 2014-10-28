using UnityEngine;
using System.Collections;

namespace Dotflow
{

	public class MainMenuScript : DotflowMenu {

		public DotflowElement[] mainMenuElements = new DotflowElement[0];
		public bool childrenMoving = false;

		//implemented from the base class
		public override void Open(DotflowElement[] elements)
		{
			base.Open (elements);
		}

		//implemented from the base class
		public override void Close(DotflowElement[] elements)
		{
			base.Close (elements);
		}
		

		//restarts a new game with new dots.
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

		//opens up the settings menu
		private void Settings(GameObject go)
		{
			if (!DotflowUIManager.isMenuMoving) {
				Close (mainMenuElements);
				DotflowUIManager.settingsMenu.Open (DotflowUIManager.settingsMenu.settingsMenuElements);
			}
		}

		//opens up the credits menu
		private void Credits(GameObject go)
		{
			if (!DotflowUIManager.isMenuMoving) 
			{
				Close (mainMenuElements);
				DotflowUIManager.creditsMenu.Open(DotflowUIManager.creditsMenu.creditsMenuElements);
			}
		}

		//exits out of the application
		private void Quit(GameObject go)
		{
			if (!DotflowUIManager.isMenuMoving) 
			{
				Application.Quit ();
			}
		}

		//does nothing currently
		//TODO what do we want this to do?
		private void Menu(GameObject go)
		{
			if (!DotflowUIManager.isMenuMoving)
			{

			}
		}

		//resumes the current game
		private void Play(GameObject go)
		{
			if (!DotflowUIManager.isMenuMoving) 
			{
				DotflowUIManager.isMenuActive = false;
				Close (mainMenuElements);
				DotflowUIManager.HUD.Open (DotflowUIManager.HUD.hudElements);
			}
		}

		//keeps track of all of its child elements to determine
		//if they are moving.
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


		//adds listeners to the appropriate dotflow elements
		private void Start () {
			UIEventListener.Get (mainMenuElements[0].gameObject).onClick += Back;
			UIEventListener.Get (mainMenuElements[1].gameObject).onClick += Settings;
			UIEventListener.Get (mainMenuElements[2].gameObject).onClick += Credits;
			UIEventListener.Get (mainMenuElements[3].gameObject).onClick += Quit;
			//UIEventListener.Get (mainMenuElements[4].gameObject).onClick += Menu;
			UIEventListener.Get (mainMenuElements[5].gameObject).onClick += Play;
		}
	}
}