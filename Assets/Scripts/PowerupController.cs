using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Dotflow
{
	public class PowerupController : MonoBehaviour {

		public enum Powerups : byte {ScoreMultiplier=1, ExtraLife, TimeSlow, TimeFreeze};
		public Powerups typeofPowerup;
		public float powerupChanceWeight; /* from 0 to 1, what's the % chance that this powerup shows up instead any other */

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

		void PowerScoreMultiplier() {
			Debug.Log ("Score!");
		}

		void PowerExtraLife() {
			Debug.Log ("Life!");
		}

		void PowerTimeSlow() {
			Debug.Log ("Slow!");
		}

		void PowerTimeFreeze() {
			Debug.Log ("Freeze!");
		}

		void OnDestroy() {
			ActivatePowerup ();
		}
	}
}