using UnityEngine;

using System.Collections;
using System.Collections.Generic;

namespace Dotflow {
	[RequireComponent (typeof(LineRenderer))]

	public class LineManager : MonoBehaviour {

		private LineRenderer lineRenderer;
		private List<LineBoxCollider2D> listOfColliders;
		public LineBoxCollider2D lineBoxColliderPrefab;
		public GameObject mouseFollower;

		// Use this for initialization
		void Start () {
			Debug.Log (new Vector3(3,4,0) - new Vector3(2,2,0));

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

		public void updateColliders (List<Transform>vertexList, float lineWidth, Color lineColor)
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
			copyVertexList.Add (mouseFollower.transform);

			for (int i = 0; i < listOfColliders.Count; i++) {
				if (i < copyVertexList.Count-1) {
					listOfColliders[i].boxCollider.enabled = true;

					Vector3 direction = (copyVertexList[i+1].position - copyVertexList[i].position); //calculates direction based on this vertex position's and the next
					direction.z = 0; //this prevents 3D rotating, aka Z axis rotation

					//Debug.Log(direction.magnitude.ToString());
					copyVertexList[i].right = direction.normalized; //points the vertix at that direction

					listOfColliders[i].transform.position = copyVertexList[i].position + (direction / 2);			//copyVertexList[i].transform.position + (new Vector3(1,1,0) * direction.magnitude / 2);
					listOfColliders[i].transform.right = direction.normalized;

					//Debug.Log(listOfColliders[i].transform.localScale.ToString() + " --- " + listOfColliders[i].transform.lossyScale.ToString());

					//listOfColliders[i].boxCollider.center = Vector3.right * direction.magnitude / 2;
					listOfColliders[i].transform.localScale = new Vector3(direction.magnitude, lineWidth, 1);
					listOfColliders[i].GetComponent<SpriteRenderer>().color = lineColor;
					listOfColliders[i].spriteRenderer.enabled = true;

				} else {
					listOfColliders[i].boxCollider.enabled = false;
					listOfColliders[i].spriteRenderer.enabled = false;
				}

				copyVertexList.Remove (mouseFollower.transform);
			}
		}
		
		// Update is called once per frame
		void Update () {
			
		}
	}
}