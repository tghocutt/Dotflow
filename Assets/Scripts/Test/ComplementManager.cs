using UnityEngine;
using System.Collections;

namespace Dotflow
{
	public class ComplementManager : MonoBehaviour {

		public GameObject[] complements1 = new GameObject[0];
		public GameObject[] complements2 = new GameObject[0];
		public GameObject[] complements3 = new GameObject[0];
		public GameObject[] complements4 = new GameObject[0];

		public GameObject[] specialMessages = new GameObject[0];

		public Camera uiCamera;
		public DotManager dotManager;

		private GameObject currentComplement;


		public GameObject GenerateComplent(int weight)
		{
			if (currentComplement != null) 
			{
				Destroy(currentComplement);
			}

			int randy;
			GameObject go;
			if (weight > 4 || weight < 1) 
			{
				Debug.LogError("Weight must be a number between 1 and 3 (including 1 and 3.) Remember, 1 is the lowest complement while 3 is the highest complement.");
			}

			if (weight == 1) 
			{
				randy = Mathf.RoundToInt (Random.Range (0, complements1.Length));
				go = complements1 [randy] as GameObject;
			} else if (weight == 2) {
				randy = Mathf.RoundToInt (Random.Range (0, complements2.Length));
				go = complements2 [randy];
			} else if (weight == 3) {
				randy = Mathf.RoundToInt (Random.Range (0, complements3.Length));
				go = complements3 [randy];
			} else {
				randy = Mathf.RoundToInt (Random.Range (0, complements4.Length));
				go = complements4 [randy];
			}


			return go;
		}


		public void ComplementPlayer(GameObject prefab, Vector3 position)
		{
			Vector3 newPos = Camera.main.WorldToViewportPoint (position);
			newPos = new Vector3 (newPos.x - 0.5f, newPos.y - 0.5f, newPos.z);
			Vector3 newNewPos = new Vector3 (uiCamera.pixelWidth * newPos.x, uiCamera.pixelHeight * newPos.y, 0);

			GameObject go = NGUITools.AddChild(gameObject, prefab);

			currentComplement = go;

			go.transform.localPosition = newNewPos;
			StartCoroutine (Despawn(go));
		}


		public void SpecialMessage (int score, Vector3 position, int i)
		{
			Vector3 newPos = Camera.main.WorldToViewportPoint (new Vector3(position.x, position.y - 0.75f, position.z));
			newPos = new Vector3 (newPos.x - 0.5f, newPos.y - 0.5f, newPos.z);
			Vector3 newNewPos = new Vector3 (uiCamera.pixelWidth * newPos.x, uiCamera.pixelHeight * newPos.y, 0);
			
			GameObject go = NGUITools.AddChild(gameObject, specialMessages[i]);

			if(i == 0) go.GetComponent<UILabel>().text = score.ToString();

			go.transform.localPosition = newNewPos;
			StartCoroutine (Despawn(go));
		}



		private IEnumerator Despawn(GameObject go)
		{
			yield return new WaitForSeconds (10f);
			Destroy (go);
		}
	}
}
