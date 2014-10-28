using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Dotflow
{
	public class DotManager : MonoBehaviour {

		public ComplementManager complementManager;
		//public GUIManager guiManager;
		public ExplosionRenderer explosionRenderer;
		public Main main; /* instance of the Main script (you can find it on the scripts folder), this is being used to figure out the screen size for the spawning */
		public AudioManager audioManager; /* the object that holds all the audio and plays it */
		public LineManager lineManager; /* lineManager is the game object that holds the line renderer, and has the script that keeps the collision boxes up to date */
		public GUIText debugText;
		public PowerupManager powerupManager;

		public int score;
		private float scoreMultiplier = 1; /* for the score powerup */

		public UILabel scoreLabel;

		public int startingLives = 3;
		public int maxLives = 5;

		public Lives livesClass;

		/* difficulty ramp variables */
		public int numberOfLevels = 5; /* how many shrinkings the game does before it stops doing so */
		public int currentLevel = 1; /*  */
		public float speedDifficultyThreshold = 105f; /* speed that the dots must achieve in order for a shrinking to occur */
		public float dotShrinkAmount = 0.2f; /* how much the dots shrink when the great shrinking happens*/

		public int maxAmountDots = 25; /* the absolute maximum amout of dots allowed on the screen, works as a cap for difficulty, also prevent overwhelming the player */
		public int currentMaxDots; /* maximum amount of dots allowed on screen right now, works as a difficulty adjust */
		public int dotCount; /* current ammount of dots on screen */
		public int startingAmountDots = 4; /* how many dots should start on the screen */

		public float chanceOfPowerupSpawn = 0.2f; /* percentual chance of a powerup spawning instead of a normal dot, from 0 to 1 */

		public float dotCurrentSpeed = 100f; /* current speed of the dots, increases during the game, up until the threshold above */
		public float dotSlowestSpeed = 100f; /* base top speed that all dots start on, aka the slowest speed */
		public float dotMaxSpeed = 200f; /* maximum speed the dots can achieve */
		public float dotSpeedBoostAmount = 1f; /* how much speed each boost gives to the dots */

		//public LineRenderer lineRenderer; /* the Renderer component for the lines */
		public float lineWidth = 4; /* the thickness, or width, for the line */

		public GameObject dotContainer; /* the parent for all the dots, a simple container for them */
		public GameObject dotPrefab; /* the only dot prefab */
		public PowerupController[] dotPowerupPrefabs; /* the only power up prefab */

		public Color[] arrayOfDotColors; /* holds all dot colors that should be in the game, the order is important, since not all colors are going to be used from the start */

		public List<Dot> allDots = new List<Dot>(); /* list containing all dot objects that exist */
		public List<Dot> dotsInLine = new List<Dot> (); /* list containing all dot objects currently forming the line, in order of connection */
		public List<Transform> listOfLineVertices = new List<Transform>(); /* list that holds all the vertices's positions */

		[HideInInspector]
		public bool lineBeingDrawn = false; /* boolean value that tells if the line is being currently drawn or not */

		public Color lineColor = Color.white; /* the current color of the line, Color.white meaning no line/no color */

		public float startingSpawnSize;
		private float spawnSize = 1.0f; /* starting size for the dots, in terms of unity scale */
		private int amountOfDotColors = 1; /* how many different dot colors are there currently in the game */
		private int everyXlevelsAddColor = 2; /* adds a new color every X levels/shrinkings */

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
			return dotPowerupPrefabs [0].gameObject; //this should never happen if the power ups are working correctly
		}

		public void scoreMultiplierIncrease(int delayInSecs) {
			scoreMultiplier++;
			Invoke ("scoreMultiplierDecrease", delayInSecs);
		}

		public void scoreMultiplierDecrease() {
			scoreMultiplier--;
		}

		//spawn dot instantiates a new random dot from prefab array (manual assignment)
		public IEnumerator SpawnDot()
		{
			//picks random location
			Vector2 spawnPos = new Vector2(Random.Range(-main.screenSize.x, main.screenSize.x), Random.Range(-main.screenSize.y, main.screenSize.y));

			GameObject dotObject;
			if (currentMaxDots > 4 && Random.Range(0f, 1f) <= chanceOfPowerupSpawn) { /* rolls the chance of a power up spawn */
				dotObject = Instantiate (getRandomPowerup()) as GameObject;
				dotObject.GetComponent<PowerupController>().powerupManager = powerupManager;
				dotObject.transform.parent = dotContainer.transform;

				Destroy(allDots[dotCount].gameObject);
				allDots[dotCount] = dotObject.GetComponent<Dot>();
			} else {
				dotObject = activateNewDot(dotCount);
			}
			
			//sets scale
			dotObject.transform.localScale = new Vector3 (spawnSize, spawnSize, spawnSize);

			//sets random speed vector based on max speed
			float randyX = Random.Range(0, dotCurrentSpeed);
			float randyY = dotCurrentSpeed - randyX;

			//applies all those parameters to the new dot
			Vector2 randomVector = new Vector2(randyX, randyY);
			dotObject.transform.localPosition = spawnPos;
			dotObject.rigidbody2D.AddForce(randomVector);
			dotObject.GetComponent<Dot>().dotManager = this;

			dotObject.transform.rotation = Quaternion.identity;
			dotCount += 1;
			yield return new WaitForSeconds (2f); /* 2f is a constant 2 second delay minimum between dot spawns (doesnt seem to work) */
		}

		GameObject activateNewDot(int dotPosition) { /* takes the first inactive dot on the list, randomizes a new color for it, activates it, and returns it's gameObject */
			int randy = Mathf.RoundToInt (Random.Range (0, amountOfDotColors));
			allDots[dotPosition].SetColor(arrayOfDotColors[randy]);
			GameObject dotObject = allDots[dotPosition].gameObject;
			dotObject.SetActive(true);
			return dotObject;
			Debug.Log ("I'm happening");
		}

		//increases the difficulty of the game
		public void IncreaseDifficulty()
		{
			if (amountOfDotColors < 3) /* the game starts with 1 color, and adds the other 2 after the first 2 lines, after that it proceeds as normal */
				amountOfDotColors++;

			if (dotCurrentSpeed >= speedDifficultyThreshold && currentLevel <= numberOfLevels) { /* if speed threshold is reached, shrinking happens here */
				audioManager.soundFX [4].Play ();
				currentLevel++;
				if (currentLevel % everyXlevelsAddColor == 0 && amountOfDotColors < arrayOfDotColors.Length) /* one new color every 2 levels, unless there are no more new colors to add */
					amountOfDotColors++;

				StartCoroutine(startShrinking()); /* the intention is to have this shrink the dots to make it harder, but turn down the other difficulty knobs */
			}

			if (currentMaxDots < maxAmountDots) /* if the maximum dot cap hasn't been reached */
				currentMaxDots++; /* increase the max amount of dots by 1 */

			if (dotCurrentSpeed < dotMaxSpeed)
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
		}

		//takes a list of dots, destroys and removes them from the dotlist, 
		private void DestroyDots(List<Dot> ds)
		{
			if (ds.Count >= 2) {
				/* spawns a praise word code */
				if(ds.Count >= 3 && ds.Count < 5)
				{
					//small complement
					complementManager.ComplementPlayer(complementManager.GenerateComplent(1), ds[ds.Count-1].transform.position);
				} else if (ds.Count >= 5 && ds.Count < 7)
				{
					//medium complement
					complementManager.ComplementPlayer(complementManager.GenerateComplent(2), ds[ds.Count-1].transform.position);
				} else if (ds.Count > 7)
				{
					//big complement
					complementManager.ComplementPlayer(complementManager.GenerateComplent(3), ds[ds.Count-1].transform.position);
				}
				/* end of praise code */

				score += Mathf.RoundToInt(10f * (ds.Count*(ds.Count/2f)) * scoreMultiplier); //calculates the score for the line, bigger lines have slightly exponential curves
				scoreLabel.text = score.ToString();

				explosionRenderer.DrawExplosions (ds, lineColor);
				//loops through dots in line, destroys them, removes them from dotlist
				foreach (Dot d in ds) {
					allDots.Remove(d);

					if (d.isPowerup) {
						/* recreates a new inactive neutral dot and adds it to the end of the list */
						GameObject newDot = Instantiate(dotPrefab) as GameObject;
						allDots.Add(newDot.GetComponent<Dot>());
						newDot.transform.parent = dotContainer.transform;
						newDot.SetActive(false);

						Destroy(d.gameObject);
					} else {
						d.gameObject.SetActive(false); /* deactivates the dot's game object */
						allDots.Add(d); /* and this puts the 'destroyed' dot at the end of the list, where it will be reused */
					}
					dotCount -= 1;
					audioManager.Pop ();					
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
		private void LateUpdate()
		{
			if (!DotflowUIManager.isMenuActive) { /* stops in-game user input when the UI is active */

				if (Input.GetMouseButtonDown (0)) {
						lineBeingDrawn = true;
						audioManager.soundFX [1].Play ();
				}

				if (Input.GetMouseButtonUp (0)) {
						DestroyDots (dotsInLine);
						lineBeingDrawn = false;
						audioManager.soundFX [1].Stop ();
				}

				if (allDots.Count >= maxAmountDots && dotCount < currentMaxDots) { /* it only starts to spawn dots if the pool is done being filled, and, if there's less dots than it should */
						StartCoroutine("SpawnDot");
				}

				StartCoroutine (DrawLine ());

				//Debug.Log ("current max dots: " + currentMaxDots + " | dot count: " + dotCount);
				//if(currentMaxDots > dotCount)
				//{
			//		StartCoroutine(SpawnDot());
				//}
			}
		}

		public void CollisionWithLine (Dot collidedDot)
		{
			if (dotsInLine.Count > 0 && !collidedDot.isPowerup && collidedDot.color != lineColor && lineColor != Color.white){

				if(livesClass.currentLives == 0)
				{
					ClearLine();

					/* highscore setting */
					if (PlayerPrefs.HasKey("highScore") && score > PlayerPrefs.GetInt("highScore"))	PlayerPrefs.SetInt("highScore",score);
					PlayerPrefs.Save();
						
					livesClass.SetLifeTotal(0);
					audioManager.soundFX[2].Play();
					//Time.timeScale = 0.1f;
					//guiManager.deathMenuRoot.SetActive(true);
					//guiManager.guiActive = true;

					StartCoroutine(OpenDeathMenu());

				} else {
					livesClass.SetLifeTotal(livesClass.currentLives - 1);
					audioManager.soundFX[2].Play();
					ClearLine();
					lineBeingDrawn = false;
				}
			}
		}

		public void RestartGame() {
			foreach (Dot d in allDots) {
				d.gameObject.SetActive(false); //setting all dots to be inactive
			}

			dotCount = 0;
			score = 0;
			scoreLabel.text = score.ToString();
			livesClass.SetLifeTotal (startingLives);
			currentLevel = 1;
			amountOfDotColors = 1;
			currentMaxDots = startingAmountDots;
			dotCurrentSpeed = dotSlowestSpeed;
			spawnSize = startingSpawnSize;
		}


		private IEnumerator OpenDeathMenu()
		{
			yield return new WaitForEndOfFrame ();

			DotflowUIManager.isMenuActive = true;
			DotflowUIManager.HUD.Close(DotflowUIManager.HUD.hudElements);
			DotflowUIManager.deathMenu.Open(DotflowUIManager.deathMenu.DeathMenuElements);
		}


		private void Start()
		{
			if (!PlayerPrefs.HasKey ("highScore")) PlayerPrefs.SetInt ("highScore", 0);

			livesClass.SetLifeTotal (startingLives);
			currentMaxDots = startingAmountDots;
			spawnSize = startingSpawnSize;
			for (int i = 0; i < maxAmountDots; i++)
			{
				GameObject newDot = Instantiate(dotPrefab) as GameObject;
				allDots.Add(newDot.GetComponent<Dot>());
				newDot.transform.parent = dotContainer.transform;
				newDot.SetActive(false);
			}
		}
	}
}
