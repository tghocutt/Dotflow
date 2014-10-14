using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Dotflow {
	public class ExplosionRenderer : MonoBehaviour {

		public GameObject explosionPrefab;

		public void DrawExplosions(List<Dot> transforms, Color color)
		{
			Go (transforms, color);
		}

		private void Go(List<Dot> transforms, Color color)
		{
			foreach(Dot dot in transforms)
			{
				GameObject newExplosion = Instantiate(explosionPrefab, dot.transform.position, Quaternion.identity) as GameObject;
				newExplosion.GetComponent<SpriteRenderer>().color = color;
			}
		}
	}
}