using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Dotflow
{
	public class PowerupController : MonoBehaviour {

		public enum Powerups : byte {ScoreMultiplier=1, ExtraLife, TimeSlow, TimeFreeze, LifeGem};
		public Powerups typeofPowerup;
		public float powerupChanceWeight; /* from 0 to 1, what's the % chance that this powerup shows up instead any other */

		public PowerupManager powerupManager;

		public int scoreMultDelayInSecs = 10;

		private Dot dot;

		public void ActivatePowerup() {
			switch (typeofPowerup) 
			{
				case Powerups.ScoreMultiplier:
					PowerScoreMultiplier();
					break;

				case Powerups.ExtraLife:
					PowerExtraLife();
					break;

				case Powerups.TimeSlow:
					PowerTimeSlow();
					break;

				case Powerups.TimeFreeze:
					PowerTimeFreeze();
					break;

				case Powerups.LifeGem:
					PowerLifeGem();
					break;

				default:
					Debug.Log("Power up #" + this.GetInstanceID().ToString() + " has no power here.");
					break;
			}
		}


		private void Start()
		{
			dot = gameObject.GetComponent<Dot> () as Dot;
		}

		void PowerScoreMultiplier() {
			dot.dotManager.scoreMultiplierIncrease(scoreMultDelayInSecs);
		}

		void PowerExtraLife() 
		{
			int lifeTotal = dot.dotManager.livesClass.currentLives;
			if(lifeTotal < dot.dotManager.livesClass.maxLives)
			{
				dot.dotManager.livesClass.AddLife();
			}
		}

		void PowerTimeSlow() 
		{
			if (powerupManager.slowTimeIsRunning)
			{
				powerupManager.timePowerUp += 5;
			} else
			{
				dot.dotManager.audioManager.soundFX[3].Play();
				powerupManager.SlowTimeGo (0.4f);
			}

		}

		void PowerTimeFreeze() /* not being used right now */
		{
//			if (powerupManager.slowTimeIsRunning)
//			{
//				powerupManager.timePowerUp += 5;
//			} else
//			{
//				powerupManager.SlowTimeGo (0.01f);
//			}
		}

		void PowerLifeGem()
		{
			PlayerPrefs.SetInt("gemTotal", PlayerPrefs.GetInt("gemTotal") + 1);
			Debug.Log (PlayerPrefs.GetInt ("gemTotal"));
			powerupManager.dotManager.gemLabel.text = PlayerPrefs.GetInt("gemTotal").ToString();
		}

		void OnDestroy() 
		{
			ActivatePowerup ();
		}
	}
}