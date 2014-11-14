using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Dotflow
{
	public class ObstacleManager : MonoBehaviour {

		public GameObject obstaclePrefab;
		public Main main;
		public DotManager dotManager;
		public List<Obstacle> obstacles = new List<Obstacle>();

		public void SpawnObstacle()
		{
			Vector2 spawnPos = new Vector2(Random.Range(-main.screenSize.x, main.screenSize.x), Random.Range(-main.screenSize.y, main.screenSize.y));
			GameObject go = Instantiate (obstaclePrefab) as GameObject;
			Obstacle ob = go.GetComponent<Obstacle> ();
			ob.dotManager = dotManager;
			obstacles.Add (ob);
			go.transform.parent = transform.parent;
			go.transform.localPosition = new Vector3 (spawnPos.x, spawnPos.y, 0f);
		}
	}
}