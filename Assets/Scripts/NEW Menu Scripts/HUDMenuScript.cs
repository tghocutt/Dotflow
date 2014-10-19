using UnityEngine;
using System.Collections;

namespace Dotflow
{
	public class HUDMenuScript : DotflowMenu {


		public DotflowElement[] hudElements = new DotflowElement[0];

		public override void Open(DotflowElement[] elements)
		{
			base.Open (elements);
		}


		public override void Close(DotflowElement[] elements)
		{
			base.Close (elements);
		}

		private void Start () 
		{
			
		}
	}
}
