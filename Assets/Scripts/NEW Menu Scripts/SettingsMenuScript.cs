using UnityEngine;
using System.Collections;

namespace Dotflow
{
	public class SettingsMenuScript : DotflowMenu {

		public DotflowElement[] settingsMenuElements = new DotflowElement[0];
		public bool childrenMoving = false;
		public AudioManager audioManager;

		public UISlider masterSlider;

		public override void Open(DotflowElement[] elements)
		{
			base.Open (elements);
		}


		public override void Close(DotflowElement[] elements)
		{
			base.Close (elements);

		}

		private void MasterMute(GameObject go)
		{

		}

		private void MusicMute(GameObject go)
		{
			
		}

		private void SFXMute(GameObject go)
		{
			
		}

		private void GoBack(GameObject go)
		{
			audioManager.menuFX [0].Play ();
			if (!DotflowUIManager.isMenuMoving) {
				Close (settingsMenuElements);
				DotflowUIManager.mainMenu.Open (DotflowUIManager.mainMenu.mainMenuElements);
			}
		}


		private void Update()
		{
			AudioListener.volume = masterSlider.value;

			bool moving = false;
			foreach(DotflowElement e in settingsMenuElements)
			{
				if(e.amIMoving) moving = true;
				break;
			}
			childrenMoving = moving;
		}


		private void Start () 
		{
			UIEventListener.Get (settingsMenuElements[0].gameObject).onClick += MasterMute;
			UIEventListener.Get (settingsMenuElements[5].gameObject).onClick += MusicMute;
			UIEventListener.Get (settingsMenuElements[6].gameObject).onClick += SFXMute;
			UIEventListener.Get (settingsMenuElements[4].gameObject).onClick += GoBack;
		}
	}
}