using UnityEngine;
using System.Collections;

namespace Dotflow
{
	public class MenuTest : DotflowMenu {

		public DotflowElement[] testElements;

		public override void Open(DotflowElement[] elements)
		{
			base.Open (elements);
		}


		public override void Close(DotflowElement[] elements)
		{
			base.Close (elements);
		}

		private void Test(GameObject go)
		{
			Close(testElements);
		}

		private void Start()
		{
			UIEventListener.Get (testElements[0].gameObject).onClick += Test;
		}
	}
}
