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
		}

		// Use this for initialization
		void Start () {
			boxCollider = GetComponent<BoxCollider2D> ();
			spriteRenderer = GetComponent<SpriteRenderer> ();
			//transform.localScale = new Vector3(1,1,1);
		}
		
		// Update is called once per frame
		void Update () {
		
		}
	}
}