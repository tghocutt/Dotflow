using UnityEngine;
using System.Collections;

namespace Dotflow
{
	public class Lives : MonoBehaviour {

		public GameObject[] lives;

		public int maxLives;
		public int currentLives;

		/*public void SetLifeTotal(int total)
		{
			if (total >= 0 && total <= 5) 
			{
				for (int i = 0; i < 5; i++) 
				{
					lives [i].SetActive ((i + 1) <= total);
				}

				currentLives = total;
			}
		}*/


		public void AddLife()
		{
			if(currentLives < 5)
			{

				StartCoroutine(GrowLife(lives[currentLives]));
				currentLives++;
			}
		}


		public void RemoveLife()
		{
			if(currentLives > 0)
			{
				currentLives--;
				StartCoroutine(ShrinkLife(lives[currentLives]));

			}
		}


		public IEnumerator ShrinkLife(GameObject go)
		{
			go.transform.localScale = new Vector3 (1, 1f, 1);

			Vector3 desiredScale = new Vector3 (0.001f, 0.001f, 0.001f);
			
			while(desiredScale.x < go.transform.localScale.x)
			{
				go.transform.localScale =new Vector3(go.transform.localScale.x - Time.deltaTime,
				                                     go.transform.localScale.y - Time.deltaTime,
				                                     go.transform.localScale.z - Time.deltaTime); 

				yield return new WaitForEndOfFrame();
			}
			
			go.transform.localScale = desiredScale;
			go.SetActive (false);
		}


		public IEnumerator GrowLife(GameObject go)
		{
			go.transform.localScale = new Vector3 (0.001f, 0.001f, 0.001f);
			go.SetActive (true);
			Vector3 desiredScale = new Vector3 (1, 1, 1);
			
			while(desiredScale.x > go.transform.localScale.x)
			{
				go.transform.localScale =new Vector3(go.transform.localScale.x + Time.deltaTime,
				                                     go.transform.localScale.y + Time.deltaTime,
				                                     go.transform.localScale.z + Time.deltaTime); 

				yield return new WaitForEndOfFrame();
			}
			
			go.transform.localScale = desiredScale;
		}
	}
}