using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Dotflow
{
	public class PowerupController : MonoBehaviour {

		public enum Powerups : byte {ScoreMultiplier=1, ExtraLife, TimeSlow, TimeFreeze};
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
				dot.dotManager.livesClass.SetLifeTotal(lifeTotal + 1);
			}
		}

		void PowerTimeSlow() 
		{
			if (powerupManager.slowTimeIsRunning)
			{
				powerupManager.timePowerUp += 5;
			} else
			{
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
	

		void OnDestroy() 
		{
			ActivatePowerup ();
		}
	}
}