using UnityEngine;
using System.Collections;

public class SpawnComplement : MonoBehaviour {

	public void ComplementPlayer(GameObject prefab)
	{
		GameObject go = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
		go.transform.parent = gameObject.transform;
		go.transform.localScale = Vector3.one;
		go.transform.position = Vector3.zero;

		transform.parent = null;
		StartCoroutine (Despawn());
	}

	private IEnumerator Despawn()
	{
		yield return new WaitForSeconds (10f);
		Destroy (gameObject);
	}
}
