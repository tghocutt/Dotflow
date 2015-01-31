using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Dotflow
{
	public class HUDMenuScript : DotflowMenu {


		public DotflowElement[] hudElements = new DotflowElement[0];
		public bool childrenMoving = false;
		public UILabel gemLabel;
		public DotManager dotManager;
		public AudioManager audioManager;



		//public UILabel gemTotal;

		public override void Open(DotflowElement[] elements)
		{
			audioManager.menuFX [0].Play ();
			base.Open (elements);
			gemLabel.text = PlayerPrefs.GetInt("gemTotal").ToString();
			dotManager.isGameInProgress = true;
		}


		public override void Close(DotflowElement[] elements)
		{
			audioManager.menuFX [0].Play ();
			base.Close (elements);
		}


		private void Menu(GameObject go)
		{
			audioManager.menuFX [0].Play ();
			if (!DotflowUIManager.isMenuMoving) {
				DotflowUIManager.isMenuActive = true;
				Close (hudElements);
				DotflowUIManager.mainMenu.Open (DotflowUIManager.mainMenu.mainMenuElements);
			}
		}


		private void Update()
		{
			bool moving = false;
			foreach(DotflowElement e in hudElements)
			{
				if(e.amIMoving) moving = true;
				break;
			}
			childrenMoving = moving;
		}

		private void Start () 
		{
			UIEventListener.Get (hudElements[0].gameObject).onClick += Menu;
		}
	}
}
