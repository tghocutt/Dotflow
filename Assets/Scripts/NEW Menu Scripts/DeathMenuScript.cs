using UnityEngine;
using System.Collections;

namespace Dotflow
{
	public class DeathMenuScript : DotflowMenu {

		public DotflowElement[] DeathMenuElements = new DotflowElement[0];
		public bool childrenMoving = false;

		public override void Open(DotflowElement[] elements)
		{
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
				Application.Quit ();
			}
		}


		private void Restart(GameObject go)
		{
			if (!DotflowUIManager.isMenuMoving) 
			{
				Close (DeathMenuElements);
				DotflowUIManager.HUD.Open (DotflowUIManager.HUD.hudElements);
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
			UIEventListener.Get (DeathMenuElements[0].gameObject).onClick += Quit;
			UIEventListener.Get (DeathMenuElements[1].gameObject).onClick += Restart;
		}
	}
}
