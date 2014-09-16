using UnityEngine;
using System.Collections;

namespace Dotflow{
	public class LineBoxCollider2D : MonoBehaviour {

		public BoxCollider2D boxCollider;

		void OnTriggerEnter2D(Collider2D other){
			Dot tempDot = other.gameObject.GetComponent<Dot> ();
//			Debug.Log ("Line collided with: " + tempDot.color + "  " + tempDot.GetInstanceID());
			tempDot.dotManager.CollisionWithLine (tempDot);
		}

		// Use this for initialization
		void Start () {
			boxCollider = this.gameObject.GetComponent<BoxCollider2D> ();
			//transform.localScale *= 10;
			transform.localScale = new Vector3(15,15,15);
		}
		
		// Update is called once per frame
		void Update () {
		
		}
	}
}