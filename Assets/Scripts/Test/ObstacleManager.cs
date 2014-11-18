using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Dotflow
{
	public class ObstacleManager : MonoBehaviour {

		public GameObject[] obstaclePrefabs;
		public Main main;
		public DotManager dotManager;
		public List<Obstacle> obstacles = new List<Obstacle>();

		public void SpawnObstacle()
		{
			int randy = Mathf.RoundToInt (Random.Range (0, obstaclePrefabs.Length));

			Vector2 spawnPos = new Vector2(Random.Range(-main.screenSize.x, main.screenSize.x), Random.Range(-main.screenSize.y, main.screenSize.y));
			GameObject go = Instantiate (obstaclePrefabs[randy]) as GameObject;
			Obstacle ob = go.GetComponent<Obstacle> ();
			Dot dot = go.GetComponent<Dot> ();
			ob.dotManager = dotManager;
			ColorPicker (dot, ob.tag);
			obstacles.Add (ob);
			go.transform.parent = transform.parent;
			go.transform.localPosition = new Vector3 (spawnPos.x, spawnPos.y, 0f);
			StartCoroutine(ob.Grow ());
		}


		private void ColorPicker(Dot dot, string tag)
		{
			if(tag == "red")
			{
				dot.color = dotManager.arrayOfDotColors[2];
				dot.tag = tag;
			} else if(tag == "green")
			{
				dot.color = dotManager.arrayOfDotColors[1];
				dot.tag = tag;
			} else if(tag == "blue")
			{
				dot.color = dotManager.arrayOfDotColors[0];
				dot.tag = tag;
			} else if(tag == "yellow")
			{
				dot.color = dotManager.arrayOfDotColors[4];
				dot.tag = tag;
			} else if(tag == "purple"){
				dot.color = dotManager.arrayOfDotColors[3];
				dot.tag = tag;
			}else {
				Debug.LogError(tag + " is an invalid - check the obstacle prefabs for an incorrect tag");
			}
		}
	}
}