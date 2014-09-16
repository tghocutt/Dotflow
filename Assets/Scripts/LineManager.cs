using UnityEngine;

using System.Collections;
using System.Collections.Generic;

namespace Dotflow {
	[RequireComponent (typeof(LineRenderer))]
	[RequireComponent (typeof(Collider2D))]

	public class LineManager : MonoBehaviour {

		private LineRenderer lineRenderer;
		private List<LineBoxCollider2D> listOfColliders;
		public LineBoxCollider2D lineBoxColliderPrefab;
		public GameObject mouseFollower;

		// Use this for initialization
		void Start () {
			lineRenderer = this.GetComponent<LineRenderer>();
			listOfColliders = new List<LineBoxCollider2D>();
		
			for(int i = 0; i < 15; i++) { //placeholder code to start out with 4 colliders, should change sometime in the future
				LineBoxCollider2D newBox = Instantiate(lineBoxColliderPrefab) as LineBoxCollider2D; //creates a new collider, disables it
				newBox.transform.parent = this.transform; //sets the parent as the line manager
				listOfColliders.Add(newBox); //and stores it in the list
			}
		}

		public LineRenderer GetLineRenderer(){
			return lineRenderer;
		}

		public void updateColliders (List<Transform>vertexList)
		{
			List<Transform> copyVertexList = new List<Transform> ();
			copyVertexList.AddRange (vertexList);

			/*
			while (listOfColliders.Count < copyVertexList.Count-1) { //while there's less colliders than lines
				LineBoxCollider2D newBox = Instantiate(lineBoxColliderPrefab) as LineBoxCollider2D; //creates a new collider, disables it
				newBox.transform.parent = this.transform; //sets the parent as the line manager
				listOfColliders.Add(newBox); //and stores it in the list
			}
			*/

			copyVertexList.Add(mouseFollower.transform);

			for (int i = 0; i < listOfColliders.Count; i++) {
				if (i < copyVertexList.Count-1) {
					listOfColliders[i].boxCollider.enabled = true;

					Vector3 direction = (copyVertexList[i].position - copyVertexList[i+1].position) * 15; //calculates direction based on this vertex position's and the next
					direction.z = 0;
					//copyVertexList[i].right = direction.normalized; //points the vertix at that direction

					listOfColliders[i].transform.position = copyVertexList[i].position;
					listOfColliders[i].transform.right = direction.normalized;

					//listOfColliders[i].transform.localScale = copyVertexList[i].localScale * 15; //makes the box scale proportional to the dot scale; this times 15 thing is a patch up due to NGUI; its also just a random number that seems to work
					listOfColliders[i].transform.localScale = new Vector3(15,15,15);
					
					listOfColliders[i].boxCollider.center = -Vector3.right * direction.magnitude / 2;
					listOfColliders[i].boxCollider.size = new Vector3(direction.magnitude, 1, 1);

				} else {
					listOfColliders[i].boxCollider.enabled = false;
				}
			}
		}
		
		// Update is called once per frame
		void Update () {
			
		}
	}
}