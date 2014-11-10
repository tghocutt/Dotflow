using UnityEngine;
using System.Collections;

namespace Dotflow
{
	public class DeathMenuScript : DotflowMenu {

		public DotflowElement[] DeathMenuElements = new DotflowElement[0];
		public bool childrenMoving = false;

		public UILabel score;

		public override void Open(DotflowElement[] elements)
		{
			score.text = DotflowUIManager._dotManager.score.ToString ();
			base.Open (elements);
		}


		public override void Close(DotflowElement[] elements)
		{
			base.Close (elements);
		}


		private void Quit(GameObject go)
		{
			if (!DotflowUIManager.isMenuMoving) 
			{
				DotflowUIManager._dotManager.RestartGame();
				Close (DeathMenuElements);
				DotflowUIManager.mainMenu.Open (DotflowUIManager.mainMenu.mainMenuElements);
			}
		}


		private void Restart(GameObject go)
		{
			if (!DotflowUIManager.isMenuMoving) 
			{
				Close (DeathMenuElements);
				DotflowUIManager._dotManager.RestartGame();
				DotflowUIManager.boosterMenu.Open (DotflowUIManager.boosterMenu.boosterMenuElements);
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
		}
	}
}
