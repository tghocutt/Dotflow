using UnityEngine;
using System.Collections;

namespace Dotflow
{
	public class Obstacle : MonoBehaviour {

		public DotManager dotManager;
		public Dot dot;
		public string colorTag;

		public IEnumerator Grow()
		{
			transform.localScale = new Vector3 (0.01f, 0.01f, 0.01f);

			while(transform.localScale.x < 1f)
			{
				transform.localScale = new Vector3(transform.localScale.x + (Time.deltaTime * 2),
				                                   transform.localScale.y + (Time.deltaTime * 2),
				                                   transform.localScale.z + (Time.deltaTime * 2));

				yield return new WaitForEndOfFrame();
			}

			transform.localScale = new Vector3 (1, 1, 1);
		}


		private void Start()
		{
			dot.dotManager = dotManager;
		}
	}
}
