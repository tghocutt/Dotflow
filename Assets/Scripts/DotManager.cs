using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Dotflow
{
	public class DotManager : MonoBehaviour {

		public GUIManager guiManager;
		public ExplosionRenderer explosionRenderer;
		public Main main; /* instance of the Main script (you can find it on the scripts folder), this is being used to figure out the screen size for the spawning */
		public AudioManager audioManager; /* the object that holds all the audio and plays it */
		public LineManager lineManager; /* lineManager is the game object that holds the line renderer, and has the script that keeps the collision boxes up to date */
		public GUIText debugText;
		public PowerupManager powerupManager;

		public int score;
		public UILabel scoreLabel;

		public int startingLives = 3;
		public int maxLives = 5;

		public Lives livesClass;

		/* difficulty ramp variables */
		public int numberOfLevels = 5; /* how many shrinkings the game does before it stops doing so */
		public float speedDifficultyThreshold = 105f; /* speed that the dots must achieve in order for a shrinking to occur */
		public float dotShrinkAmount = 0.2f; /* how much the dots shrink when the great shrinking happens*/

		public int maxAmountDots = 25; /* the absolute maximum amout of dots allowed on the screen, works as a cap for difficulty, to prevent overwhelming */
		public int currentMaxDots; /* maximum amount of dots allowed on screen right now, works as a difficulty adjust */
		public int dotCount; /* current ammount of dots on screen */
		public int startingAmountDots = 4; /* how many dots should start on the screen */

		public float chanceOfPowerupSpawn = 0.2f; /* percentual chance of a powerup spawning instead of a normal dot, from 0 to 1 */

		public float dotCurrentSpeed = 100f; /* current speed of the dots, increases during the game, up until the threshold above */
		public float dotSlowestSpeed = 100f; /* base top speed that all dots start on, aka the slowest speed */
		public float dotSpeedBoostAmount = 1f; /* how much speed each boost gives to the dots */

		//public LineRenderer lineRenderer; /* the Renderer component for the lines */
		public float lineWidth = 4; /* the thickness, or width, for the line */

		public GameObject dotContainer; /* the parent for all the dots, a simple container for them */
		public GameObject[] dotPrefabs; /* array of all dot prefabs */
		public PowerupController[] dotPowerupPrefabs;

		public List<Dot> allDots = new List<Dot>(); /* list containing all dot objects that exist */
		public List<Dot> dotsInLine = new List<Dot> (); /* list containing all dot objects currently forming the line, in order of connection */
		public List<Transform> listOfLineVertices = new List<Transform>(); /* list that holds all the vertices's positions */

		[HideInInspector]
		public bool lineBeingDrawn = false; /* boolean value that tells if the line is being currently drawn or not */

		private float spawnSize = 1.0f; /* starting size for the dots, in terms of unity scale */
		public Color lineColor = Color.white; /* the current color of the line, Color.white meaning no line/no color */

		private PowerupController pc;

		public GameObject getRandomPowerup() {
			float r = Random.Range (0f, 1f);
			float csum = 0;
			foreach (PowerupController p in dotPowerupPrefabs) {
				csum += p.powerupChanceWeight;
				if (r <= csum)
					return p.gameObject;
			}
			Debug.Log ("Something wrong with the power ups, spawning the first power up instead");
			return dotPowerupPrefabs [0].gameObject; //this should never happen
		}

		//spawn dot instantiates a new random dot from prefab array (manual assignment)
		public IEnumerator SpawnDot()
		{
			//picks random location
			Vector2 spawnPos = new Vector2(Random.Range(-main.screenSize.x, main.screenSize.x), Random.Range(-main.screenSize.y, main.screenSize.y));

			GameObject dotObject;
			if (currentMaxDots > 4 && Random.Range(0f, 1f) <= chanceOfPowerupSpawn) { /* rolls the chance of a power up spawn */
				dotObject = Instantiate (getRandomPowerup()) as GameObject;
				dotObject.transform.rotation = Quaternion.identity;
				dotObject.GetComponent<PowerupController>().powerupManager = powerupManager;
			} else {
				int randy = Mathf.RoundToInt (Random.Range (0, dotPrefabs.Length));
				dotObject = Instantiate (dotPrefabs [randy]) as GameObject;
				dotObject.transform.rotation = Quaternion.identity;
			}

			//sets parent and scale
			dotObject.transform.parent = dotContainer.transform;
			dotObject.transform.localScale = new Vector3 (spawnSize, spawnSize, spawnSize);

			//sets random speed vector based on max speed
			float randyX = Random.Range(0, dotCurrentSpeed);
			float randyY = dotCurrentSpeed - randyX;

			//applies all those parameters to the new dot
			Vector2 randomVector = new Vector2(randyX, randyY);
			dotObject.transform.localPosition = spawnPos;
			dotObject.rigidbody2D.AddForce(randomVector);
			dotObject.GetComponent<Dot>().dotManager = this;

			dotCount += 1;
			dotObject.transform.rotation = Quaternion.identity;
			yield return new WaitForSeconds (2f); /* 2f is a constant 2 second delay minimum between dot spawns */
		}


		//increases the difficulty of the game
		public void IncreaseDifficulty()
		{
			if (dotCurrentSpeed >= speedDifficultyThreshold) { /* if speed threshold is reached, shrinking happens here */
				StartCoroutine(startShrinking()); /* the intention is to have this shrink the dots to make it harder, but turn down the other difficulty knobs */
			}

			if (currentMaxDots < maxAmountDots) /* if the maximum dot cap hasn't been reached */
				currentMaxDots++; /* increase the max amount of dots by 1 */

			dotCurrentSpeed = dotCurrentSpeed + dotSpeedBoostAmount; /* increase the speed of all dots */

		}
	
		/* coroutine that shrinks all dots, and takes care of the logic behind the shrinking */
		IEnumerator startShrinking() {
			Vector3 goalScale = allDots [0].transform.localScale * (1-dotShrinkAmount); //this line could be a problem if the shrinking happens at the same time as ALL dots on screen are destroyed, low probability but still could happen

			dotCurrentSpeed = dotSlowestSpeed; /* slows all dots that will spawn after the shrinking, doenst affect the dots that exist currently */

			while (allDots[0].transform.localScale.x > goalScale.x) {
				spawnSize -= (1-dotShrinkAmount) * Time.deltaTime;

				foreach (Dot dot in allDots) {
					dot.transform.localScale -= ((1-dotShrinkAmount) * new Vector3(1,1,0)) * Time.deltaTime;
				}

				yield return new WaitForEndOfFrame();
			}
			Debug.Log("Scale: " + spawnSize.ToString() + " Variable for Speed: " + dotCurrentSpeed + " Dot Speed: " + allDots[0].rigidbody2D.velocity.magnitude);
		}

		//takes a list of dots, destroys and removes them from the dotlist, 
		private void DestroyDots(List<Dot> ds)
		{
			if (ds.Count >= 2) {

				score += 10 * ds.Count * ds.Count;
				scoreLabel.text = score.ToString();

				explosionRenderer.DrawExplosions (ds, lineColor);
				//loops through dots in line, destroys them, removes them from dotlist
				foreach (Dot d in ds) {
					for (int i = 0; i < allDots.Count; i++) {
						if (allDots [i] == d) {
							Destroy (allDots [i].gameObject);
							allDots.RemoveAt (i);
							dotCount -= 1;
							audioManager.Pop ();
						}
					}
				}
				IncreaseDifficulty ();
			}
			ClearLine ();
		}


		void ClearLine(){
			listOfLineVertices.Clear();
			dotsInLine.Clear();
			lineColor = Color.white;
		}

		//draw line draws a line between all of the dots in the line, using the dot locations as vertices
		private IEnumerator DrawLine()
		{
			//gets mouse position to draw the raycast
			Vector3 raycastMousePos = Input.mousePosition;
			raycastMousePos.x = Mathf.Clamp01(raycastMousePos.x / Screen.width);
			raycastMousePos.y = Mathf.Clamp01(raycastMousePos.y / Screen.height);
			raycastMousePos.z = 0f;
			
			//gets mouse position to draw the line
			Vector3 linedrawMousePos = new Vector3();
			linedrawMousePos.x = raycastMousePos.x;
			linedrawMousePos.y = raycastMousePos.y;
			linedrawMousePos.z = 1.0f;

			//sets line vertex locations
			for (int i = 0; i < listOfLineVertices.Count; i++) {
				listOfLineVertices [i].position = new Vector3 (listOfLineVertices [i].position.x, listOfLineVertices [i].position.y, 0.0f);
			}

			//adds new dot to the end of the line
			if (lineBeingDrawn)
			{
				RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ViewportToWorldPoint(raycastMousePos), Vector3.forward, 300f);

				foreach(RaycastHit2D h in hits)
				{
					if(h.collider.gameObject.layer == 9)
					{
						if(!dotsInLine.Contains(h.collider.GetComponentInParent<Dot>()))
						{
							Dot dot = h.collider.GetComponentInParent<Dot>();
							if (lineColor == Color.white || lineColor == dot.color || dot.isPowerup)
							{
								if (!dot.isPowerup && lineColor == Color.white)
									lineColor = dot.color;

								dotsInLine.Add(dot);
								listOfLineVertices.Add(dot.transform);
								if(dotsInLine.Count < audioManager.dotsConnecting.Length)
								{
									audioManager.dotsConnecting[dotsInLine.Count - 1].Play();
								} else {
									audioManager.dotsConnecting[audioManager.dotsConnecting.Length - 1].Play();
								}
							}
						}
					}
					
				}
			}
			lineManager.updateColliders (listOfLineVertices, lineWidth, lineColor);

			yield return null;
		}


		//detects input, and tracks dot count, and starts coroutine
		private void Update()
		{
			if (!guiManager.guiActive) { /* stops in-game user input when the UI is active */

				if (Input.GetMouseButtonDown (0)) {
						lineBeingDrawn = true;
						audioManager.soundFX [1].Play ();
				}

				if (Input.GetMouseButtonUp (0)) {
						DestroyDots (dotsInLine);
						lineBeingDrawn = false;
						audioManager.soundFX [1].Stop ();
				}

				if (dotCount < currentMaxDots) {
						StartCoroutine("SpawnDot");
				}

				StartCoroutine (DrawLine ());
				lineManager.updateColliders (listOfLineVertices, lineWidth, lineColor);

				//debugText.text = allDots[0].rigidbody2D.velocity.ToString();
			}
		}

		public void CollisionWithLine (Dot collidedDot)
		{

			if (dotsInLine.Count > 0 && !collidedDot.isPowerup && collidedDot.color != lineColor && lineColor != Color.white){

				if(livesClass.currentLives == 0)
				{
					livesClass.SetLifeTotal(0);
					audioManager.soundFX[2].Play();
					//Time.timeScale = 0.1f;
					guiManager.deathMenuRoot.SetActive(true);
					ClearLine();
					guiManager.guiActive = true;
				} else {
					livesClass.SetLifeTotal(livesClass.currentLives - 1);
					audioManager.soundFX[2].Play();
					ClearLine();
					lineBeingDrawn = false;
				}
			}
		}

		//adds listener to UI elements
		private void Start()
		{
			livesClass.SetLifeTotal (startingLives);
			currentMaxDots = startingAmountDots;
		}
	}
}
