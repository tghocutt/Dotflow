using UnityEngine;
using System.Collections;

namespace Dotflow
{

	public class MainMenuScript : DotflowMenu {

		public DotflowElement[] mainMenuElements = new DotflowElement[0];

		public override void Open(DotflowElement[] elements)
		{
			base.Open (elements);
		}


		public override void Close(DotflowElement[] elements)
		{
			base.Close (elements);
		}
		


		private void Back(GameObject go)
		{


		}
		
		private void Settings(GameObject go)
		{
			
			
		}
		private void Credits(GameObject go)
		{
			
			
		}
		private void Quit(GameObject go)
		{
			
			
		}
		private void Menu(GameObject go)
		{
			
			
		}
		private void Play(GameObject go)
		{
			
			
		}



		private void Start () {
	

			UIEventListener.Get (mainMenuElements[0].gameObject).onClick += Back;
			UIEventListener.Get (mainMenuElements[1].gameObject).onClick += Settings;
			UIEventListener.Get (mainMenuElements[2].gameObject).onClick += Credits;
			UIEventListener.Get (mainMenuElements[3].gameObject).onClick += Quit;
			UIEventListener.Get (mainMenuElements[4].gameObject).onClick += Menu;
			UIEventListener.Get (mainMenuElements[5].gameObject).onClick += Play;




		}
	}
}