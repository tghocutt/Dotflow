using UnityEngine;
using System.Collections;

namespace Dotflow
{
	public class CreditsMenuScript : DotflowMenu {

		public DotflowElement[] creditsMenuElements = new DotflowElement[0];
		public bool childrenMoving = false;
		public AudioManager audioManager;

		public override void Open(DotflowElement[] elements)
		{
			base.Open (elements);
		}


		public override void Close(DotflowElement[] elements)
		{
			base.Close (elements);
		}


		private void GoBack(GameObject go)
		{
			audioManager.menuFX [0].Play ();
			if (!DotflowUIManager.isMenuMoving) {
				Close (creditsMenuElements);
				DotflowUIManager.mainMenu.Open (DotflowUIManager.mainMenu.mainMenuElements);
			}
		}


		private void Update()
		{
			bool moving = false;
			foreach(DotflowElement e in creditsMenuElements)
			{
				if(e.amIMoving) moving = true;
				break;
			}
			childrenMoving = moving;
		}


		private void Start () 
		{
			UIEventListener.Get (creditsMenuElements[0].gameObject).onClick += GoBack;
		}
	}
}
