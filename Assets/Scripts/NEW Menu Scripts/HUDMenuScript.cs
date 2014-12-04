using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Dotflow
{
	public class HUDMenuScript : DotflowMenu {


		public DotflowElement[] hudElements = new DotflowElement[0];
		public bool childrenMoving = false;
		public UILabel gemLabel;

		private List<GameObject> gos = new List<GameObject> ();

		//public UILabel gemTotal;

		public override void Open(DotflowElement[] elements)
		{
			base.Open (elements);
			gemLabel.text = PlayerPrefs.GetInt("gemTotal").ToString();
		}


		public override void Close(DotflowElement[] elements)
		{
			base.Close (elements);
		}


		private void Menu(GameObject go)
		{
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
