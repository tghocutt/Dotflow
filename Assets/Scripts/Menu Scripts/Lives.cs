using UnityEngine;
using System.Collections;

public class Lives : MonoBehaviour {

	public GameObject[] lives;

	public void startLives (int numberOfLives){
		lives = new GameObject[numberOfLives];

		//TODO: programatically add the sprites
	}
}