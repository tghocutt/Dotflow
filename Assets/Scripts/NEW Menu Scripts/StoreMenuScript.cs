using UnityEngine;
using System.Collections;

namespace Dotflow
{
	public class StoreMenuScript : DotflowMenu {

		public DotflowElement[] storeMenuElements = new DotflowElement[0];
		public bool childrenMoving = false;
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
				Close (storeMenuElements);
				DotflowUIManager.mainMenu.Open (DotflowUIManager.mainMenu.mainMenuElements);
			}
		}


		private void Start () 
		{
			UIEventListener.Get (storeMenuElements[0].gameObject).onClick += Back;
		}
	}
}
