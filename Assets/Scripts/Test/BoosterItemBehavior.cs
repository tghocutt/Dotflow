using UnityEngine;
using System.Collections;

namespace Dotflow
{
	public class BoosterItemBehavior : MonoBehaviour 
	{
		public UIGrid grid;

		public UIDragDropContainer dragDropContainer;

		private void Update()
		{
			grid = gameObject.GetComponentInParent<UIGrid> ();

			dragDropContainer.reparentTarget = grid.transform;
		}

	}
}
