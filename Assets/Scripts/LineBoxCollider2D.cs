using UnityEngine;
using System.Collections;

namespace Dotflow{
	public class LineBoxCollider2D : MonoBehaviour {
		[HideInInspector]
		public BoxCollider2D boxCollider;
		public SpriteRenderer spriteRenderer;

		void OnTriggerEnter2D(Collider2D other){

			Dot tempDot = other.gameObject.GetComponent<Dot> ();
			tempDot.dotManager.CollisionWithLine (tempDot);
			//Obstacle obstacle = other.gameObject.GetComponent<Obstacle> ();
			/*if (tempDot != null) 
			{
				tempDot.dotManager.CollisionWithLine (tempDot);
			} else {
				if(obstacle != null) {
					//obstacle.dotManager.CollisionWithObstacle(obstacle);
				}
			}*/

		}

		// Use this for initialization
		void Start () {
			boxCollider = GetComponent<BoxCollider2D> ();
			spriteRenderer = GetComponent<SpriteRenderer> ();
		}
		
		// Update is called once per frame
		void Update () {
		
		}
	}
}