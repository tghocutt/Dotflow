using UnityEngine;
using System.Collections;

namespace Dotflow
{
	public class Lives : MonoBehaviour {

		public GameObject[] lives;

		public int maxLives;
		public int currentLives;

		public void SetLifeTotal(int total)
		{
			if (total >= 0 && total <= 5) 
			{
				for (int i = 0; i < 5; i++) 
				{
					lives [i].SetActive ((i + 1) <= total);
				}

				currentLives = total;
			}
		}
	}
}