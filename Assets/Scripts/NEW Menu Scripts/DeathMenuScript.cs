using UnityEngine;
using System.Collections;

namespace Dotflow
{
	public class DeathMenuScript : DotflowMenu {

		public DotflowElement[] DeathMenuElements = new DotflowElement[0];
		public bool childrenMoving = false;
		public DotManager dotManager;
		public UILabel score;
		public UILabel gemLabel;
		public AudioManager audioManager;

		public override void Open(DotflowElement[] elements)
		{
			score.text = DotflowUIManager._dotManager.score.ToString ();
			base.Open (elements);
			dotManager.isGameInProgress = false;
			gemLabel.text = PlayerPrefs.GetInt ("gemTotal").ToString ();
		}


		public override void Close(DotflowElement[] elements)
		{
			base.Close (elements);
		}


		private void Quit(GameObject go)
		{
			audioManager.menuFX [0].Play ();
			if (!DotflowUIManager.isMenuMoving) 
			{
				DotflowUIManager._dotManager.RestartGame();
				Close (DeathMenuElements);
				DotflowUIManager.mainMenu.Open (DotflowUIManager.mainMenu.mainMenuElements);
			}
		}


		private void Restart(GameObject go)
		{
			audioManager.menuFX [0].Play ();
			if (!DotflowUIManager.isMenuMoving) 
			{
				Close (DeathMenuElements);
				DotflowUIManager._dotManager.RestartGame();
				DotflowUIManager.boosterMenu.Open (DotflowUIManager.boosterMenu.boosterMenuElements);
			}
		}

		private void UseLifeGem(GameObject go) {
			if (!DotflowUIManager.isMenuMoving) 
			{
				audioManager.menuFX [0].Play ();
				if (PlayerPrefs.GetInt("gemTotal") > 0) {
					DotflowUIManager._dotManager.LifeGemUsed();
					Close (DeathMenuElements);
					DotflowUIManager.HUD.Open (DotflowUIManager.HUD.hudElements);
					DotflowUIManager.isMenuActive = false;
				}else {
					//TODO:Tell the player he has no more life gems?
				}
			}
		}

		private void Update()
		{
			bool moving = false;
			foreach(DotflowElement e in DeathMenuElements)
			{
				if(e.amIMoving) moving = true;
				break;
			}
			childrenMoving = moving;
		}


		private void Start () {
			UIEventListener.Get (DeathMenuElements[0].gameObject).onClick += Restart;
			UIEventListener.Get (DeathMenuElements[1].gameObject).onClick += Quit;
			UIEventListener.Get (DeathMenuElements[3].gameObject).onClick += UseLifeGem;
		}
	}
}