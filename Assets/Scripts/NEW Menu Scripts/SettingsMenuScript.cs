using UnityEngine;
using System.Collections;

namespace Dotflow
{
	public class SettingsMenuScript : DotflowMenu {

		public DotflowElement[] creditsMenuElements = new DotflowElement[0];

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


		private void Start () 
		{
			UIEventListener.Get (creditsMenuElements[0].gameObject).onClick += MasterMute;
			UIEventListener.Get (creditsMenuElements[5].gameObject).onClick += MusicMute;
			UIEventListener.Get (creditsMenuElements[6].gameObject).onClick += SFXMute;
		}
	}
}