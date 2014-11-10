using UnityEngine;
using System.Collections;

namespace Dotflow
{
	public class BoosterSelectMenuScript : DotflowMenu {

		public DotflowElement[] boosterMenuElements = new DotflowElement[0];
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


		//resumes the current game
		private void Play(GameObject go)
		{
			if (!DotflowUIManager.isMenuMoving) 
			{
				DotflowUIManager.isMenuActive = false;
				Close (boosterMenuElements);
				DotflowUIManager.HUD.Open (DotflowUIManager.HUD.hudElements);
			}
		}

		private void Back(GameObject go)
		{
			if (!DotflowUIManager.isMenuMoving) 
			{
				DotflowUIManager._dotManager.RestartGame();
				Close (boosterMenuElements);
				DotflowUIManager.mainMenu.Open (DotflowUIManager.mainMenu.mainMenuElements);
			}
		}

		
		private void Start () 
		{
			UIEventListener.Get (boosterMenuElements[0].gameObject).onClick += Back;
			UIEventListener.Get (boosterMenuElements[1].gameObject).onClick += Play;
		}
	}
}
