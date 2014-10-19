using UnityEngine;
using System.Collections;

namespace Dotflow
{
	public class DeathMenuScript : DotflowMenu {

		public DotflowElement[] DeathMenuElements = new DotflowElement[0];

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
			Application.Quit ();
		}


		private void Restart(GameObject go)
		{

		}


		private void Start () {
			UIEventListener.Get (DeathMenuElements[0].gameObject).onClick += Quit;
			UIEventListener.Get (DeathMenuElements[1].gameObject).onClick += Restart;
		}
	}
}
