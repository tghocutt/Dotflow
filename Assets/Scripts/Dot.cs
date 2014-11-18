using UnityEngine;
using System.Collections;

namespace Dotflow
{
	public class Dot : MonoBehaviour {

		public Rigidbody2D rigidBody;
		public bool clickedOrDetected;
		public DotManager dotManager;
		public int id;
		public string tag;
		public bool isPowerup;
		public UIAnchor anchor;
		public SpriteRenderer background;

		//[HideInInspector]
		public Color color;

		public void Draw(GameObject go)
		{
			dotManager.listOfLineVertices.Add(this.gameObject.transform);
		}

		public void SetColor(Color color){
			this.color = color;
			GetComponent<SpriteRenderer>().color = color;
		}

		private void Start () {

			color = GetComponent<SpriteRenderer>().color;

			//dotManager.allDots.Add(this);

			id = this.GetInstanceID ();

		}

//		void Update () {
//			Debug.Log (rigidbody2D.velocity.ToString());
//		}
	}
}
