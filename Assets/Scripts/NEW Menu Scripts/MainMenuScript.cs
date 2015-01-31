using UnityEngine;
using System.Collections;

namespace Dotflow
{
	public class MainMenuScript : DotflowMenu {

		public DotflowElement[] mainMenuElements = new DotflowElement[0];
		public bool childrenMoving = false;
		public DotManager dotManager;
		public AudioManager audioManager;

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
			audioManager.menuFX [0].Play ();

			if (!DotflowUIManager.isMenuMoving) 
			{
				Close (mainMenuElements);
				DotflowUIManager.boosterMenu.Open (DotflowUIManager.boosterMenu.boosterMenuElements);
			}
		}

		//opens up the settings menu
		private void Settings(GameObject go)
		{
			audioManager.menuFX [0].Play ();
			if (!DotflowUIManager.isMenuMoving) {
				Close (mainMenuElements);
				DotflowUIManager.settingsMenu.Open (DotflowUIManager.settingsMenu.settingsMenuElements);
			}
		}

		//opens up the credits menu
		private void Credits(GameObject go)
		{
			audioManager.menuFX [0].Play ();
			if (!DotflowUIManager.isMenuMoving) 
			{
				Close (mainMenuElements);
				DotflowUIManager.creditsMenu.Open(DotflowUIManager.creditsMenu.creditsMenuElements);
			}
		}

		//exits out of the application
		private void Quit(GameObject go)
		{
			audioManager.menuFX [0].Play ();
			if (!DotflowUIManager.isMenuMoving) 
			{
				Application.Quit ();
			}
		}

		//does nothing currently
		//TODO what do we want this to do?
		private void Menu(GameObject go)
		{
			audioManager.menuFX [0].Play ();
			if (!DotflowUIManager.isMenuMoving)
			{

			}
		}

		//resumes the current game
		private void Play(GameObject go)
		{
			audioManager.menuFX [0].Play ();
			if (!DotflowUIManager.isMenuMoving) 
			{
				Close (mainMenuElements);
				if(dotManager.isGameInProgress)
				{
					DotflowUIManager.HUD.Open (DotflowUIManager.HUD.hudElements);
					DotflowUIManager.isMenuActive = false;
				} else {
					DotflowUIManager.boosterMenu.Open (DotflowUIManager.boosterMenu.boosterMenuElements);
				}

			}
		}


		//opens the store
		private void Store(GameObject go)
		{
			audioManager.menuFX [0].Play ();
			if (!DotflowUIManager.isMenuMoving) 
			{
				Close (mainMenuElements);
				DotflowUIManager.storeMenu.Open (DotflowUIManager.storeMenu.storeMenuElements);
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
			if (!PlayerPrefs.HasKey("lifeGem"))
				PlayerPrefs.SetInt("lifeGem",5);

			UIEventListener.Get (mainMenuElements[0].gameObject).onClick += Back;
			UIEventListener.Get (mainMenuElements[1].gameObject).onClick += Settings;
			UIEventListener.Get (mainMenuElements[2].gameObject).onClick += Credits;
			UIEventListener.Get (mainMenuElements[3].gameObject).onClick += Quit;
			//UIEventListener.Get (mainMenuElements[4].gameObject).onClick += Menu;
			UIEventListener.Get (mainMenuElements[5].gameObject).onClick += Play;
			UIEventListener.Get (mainMenuElements[6].gameObject).onClick += Store;
		}
	}
}