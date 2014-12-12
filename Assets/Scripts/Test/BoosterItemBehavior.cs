using UnityEngine;
using System.Collections;

namespace Dotflow
{
	public class BoosterItemBehavior : MonoBehaviour 
	{
		public UIGrid grid;
		public GameObject upperButtonPrefab;
		public UIDragDropContainer dragDropContainer;

		public int cost;
		public bool selected;

		private void Update()
		{
			grid = gameObject.GetComponentInParent<UIGrid> ();
			if (grid)
			{
				//dragDropContainer.reparentTarget = grid.transform;
			}
		}
	}
}
