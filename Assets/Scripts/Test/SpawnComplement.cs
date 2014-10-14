using UnityEngine;
using System.Collections;

public class SpawnComplement : MonoBehaviour {

	public void ComplementPlayer(GameObject prefab)//, Vector3 position)
	{
		//Vector3 newpos = Camera.main.WorldToScreenPoint (position);

		//GameObject go = Instantiate(prefab, newpos, Quaternion.identity) as GameObject;
		//go.transform.parent = gameObject.transform;

		//gameObject.transform.position = newpos;

		StartCoroutine (Despawn());
	}

	private IEnumerator Despawn()
	{
		yield return new WaitForSeconds (10f);
		Destroy (gameObject);
	}
}
