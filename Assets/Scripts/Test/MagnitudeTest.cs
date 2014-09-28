using UnityEngine;
using System.Collections;

public class MagnitudeTest : MonoBehaviour {

	public GameObject object1;
	public GameObject object2;


	private void Start()
	{
		Debug.Log ("vector3.distance value : " + Vector3.Distance (object1.transform.position, object2.transform.position));

		Vector3 test = object2.transform.position - object1.transform.position;
		Debug.Log ("magnitude value : " + Vector3.Magnitude (test));
	}
}
