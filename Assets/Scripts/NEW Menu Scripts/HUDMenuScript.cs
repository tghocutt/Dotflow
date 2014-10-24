using UnityEngine;
using System.Collections;

namespace Dotflow
{
	public class HUDMenuScript : DotflowMenu {


		public DotflowElement[] hudElements = new DotflowElement[0];
		public bool childrenMoving = false;

		public override void Open(DotflowElement[] elements)
		{
			base.Open (elements);
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
