using UnityEngine;

using System.Collections;
using System.Collections.Generic;

namespace Dotflow {

	/* reminder: this version of LineManager doesn't use the LineRenderer component anymore; remove/avoid any use of it */

	public class LineManager : MonoBehaviour {

		private List<LineBoxCollider2D> listOfColliders;
		public LineBoxCollider2D lineBoxColliderPrefab;
		public GameObject mouseFollower;

		void Start () {
			listOfColliders = new List<LineBoxCollider2D>();
		
			for(int i = 0; i < 15; i++) { //placeholder code to start out with 15 colliders, should make it dynamic someday
				LineBoxCollider2D newBox = Instantiate(lineBoxColliderPrefab) as LineBoxCollider2D; //creates a new collider, disables it
				newBox.transform.parent = this.transform; //sets the parent as the line manager
				listOfColliders.Add(newBox); //and stores it in the list
			}
		}

		public void updateColliders (List<Transform>vertexList, float lineWidth, Color lineColor)
		{
			List<Transform> copyVertexList = new List<Transform> ();
			copyVertexList.AddRange (vertexList); /* makes a copy of the received list; remeber that C# passes parameters by reference, not copy */

			copyVertexList.Add(mouseFollower.transform); /* adds the current position of the mouseFollower to the end of the list */

			/* TODO: here would be a code to dynamically add colliders as needed */

			for (int i = 0; i < listOfColliders.Count; i++) {
				if (i < copyVertexList.Count-1) {
					Vector3 direction = (copyVertexList[i+1].position - copyVertexList[i].position); //calculates direction based on this vertex position's and the next
					direction.z = 0; //this prevents 3D rotating, aka Z axis rotation

					//copyVertexList[i].right = direction.normalized; //points the vertex at that direction

					listOfColliders[i].transform.position = copyVertexList[i].position + (direction / 2);
					listOfColliders[i].transform.position = new Vector3(listOfColliders[i].transform.position.x, listOfColliders[i].transform.position.y, 1f);

					listOfColliders[i].transform.right = direction.normalized;

					listOfColliders[i].transform.localScale = new Vector3(direction.magnitude, lineWidth, 1);

					listOfColliders[i].GetComponent<SpriteRenderer>().color = lineColor;

					listOfColliders[i].spriteRenderer.enabled = true;
					listOfColliders[i].boxCollider.enabled = true;
				} else {
					listOfColliders[i].spriteRenderer.enabled = false;
					listOfColliders[i].boxCollider.enabled = false;
				}
			}
			copyVertexList.Remove(mouseFollower.transform);
		}
	}
}