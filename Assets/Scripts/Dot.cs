using UnityEngine;
using System.Collections;

namespace Dotflow
{
	public class Dot : MonoBehaviour {

		public Rigidbody2D rigidBody;
		public bool clickedOrDetected;
		public DotManager dotManager;
		public int id;

		[HideInInspector]
		public Color color;



		public void Draw(GameObject go)
		{
			dotManager.listOfLineVertices.Add(this.gameObject.transform);
		}


		private void Start () {

			color = GetComponent<SpriteRenderer>().color;

			gameObject.transform.rotation = new Quaternion(0f, 0f, Random.Range(0.0f, 360.0f), 0f);

			dotManager.allDots.Add(this);

			id = this.GetInstanceID ();
		}

//		void Update () {
//			Debug.Log (rigidbody2D.velocity.ToString());
//		}
	}
}
