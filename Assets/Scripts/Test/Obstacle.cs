using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Dotflow
{
	public class Obstacle : MonoBehaviour 
	{
		public DotManager dotManager;
		public Dot dot;
		//public string colorTag;
		public GameObject slotPrefab;
		public GameObject gemPrefab;

		public Sprite filledSprite;

		public Transform[] slotPositions = new Transform[0];

		public Color[] possibleSlotColors = new Color[0];
		public Color[] actualSlotColors = new Color[4];

		public string[] possibleColorTags = new string[0];
		public string[] actualColorTags = new string[4];

		public bool[] filled = new bool[4];

		public SpriteRenderer[] slotSprites = new SpriteRenderer[4];


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


		public void FillDots(List<Dot> dots)
		{
			for(int i = 0; i < dots.Count; i++)
			{
				for(int j = 0; j < actualColorTags.Length; j++)
				{
					//if(dots[i] != null)Debug.Log(dots[i].tag)
					if(dots[i] != null && dots[i].tag == actualColorTags[j] && !filled[j])
					{
						Debug.Log (dots[i].tag + " : " + actualColorTags[j]);
						slotSprites[j].sprite = filledSprite;
						slotSprites[j].color = actualSlotColors[j];
						slotSprites[j].transform.localScale = new Vector3(0.5f,0.5f,0.5f);

						dots.RemoveAt(i);
						filled[j] = true;
						Debug.Log ("filling dot");
					}
				}
			}

			KillObstacle ();
		}


		private void KillObstacle()
		{
			bool test = true;

			foreach (bool b in filled) 
			{
				if(b == false) test = false;
			}

			Debug.Log (test);
			if (test)
			{
				GameObject go = Instantiate (gemPrefab) as GameObject;
				go.transform.localPosition = transform.position;
				Dot dot = go.GetComponent<Dot>();

				dot.dotManager = dotManager;

				for(int i = 0; i < dotManager.obstacleManager.obstacles.Count; i++)
				{
					if(dotManager.obstacleManager.obstacles[i].GetInstanceID() == this.GetInstanceID())
					{
						dotManager.obstacleManager.obstacles.RemoveAt(i);
					}
				}

				Destroy (this.gameObject);
			}
		}


		private bool Contains(int[] array, int num)
		{
			bool b = false;
			foreach(int i in array)
			{
				if (i == num) b = true;
				break;
			}

			return b;
		}


		private void CraftObstacle()
		{
			int i = 0;
			foreach(Transform transform in slotPositions)
			{
				int randy = Mathf.FloorToInt(Random.Range(0, dotManager.amountOfDotColors));
				GameObject go = Instantiate (slotPrefab) as GameObject;

				go.transform.parent = slotPositions[i].transform;
				go.transform.localPosition = new Vector3(0,0,0);
				go.transform.localScale = new Vector3(0.2f,0.2f,0.2f);

				SpriteRenderer sprite = go.GetComponent<SpriteRenderer>();

				sprite.color = possibleSlotColors[randy];
				slotSprites[i] = sprite;

				actualSlotColors[i] = possibleSlotColors[randy];
				actualColorTags[i] = possibleColorTags[randy];


				filled[i] = false;

				i++;
			}
		}


		private void Start()
		{
			for(int i = 0; i < filled.Length; i++)
			{
				filled[i] = false;
			}

			dot.dotManager = dotManager;
			possibleSlotColors = dotManager.arrayOfDotColors;

			CraftObstacle ();
		}
	}
}
