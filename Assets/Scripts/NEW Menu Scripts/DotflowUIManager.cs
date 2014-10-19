using UnityEngine;
using System.Collections;

namespace Dotflow
{
	public class DotflowUIManager : MonoBehaviour {

		public DotflowMenu mainMenuObj;
		public DotflowMenu settingsMenuObj;
		public DotflowMenu creditsMenuObj;
		public DotflowMenu deathMenuObj;
		public DotflowMenu HUDObj;

		public static DotflowMenu mainMenu;
		public static DotflowMenu settingsMenu;
		public static DotflowMenu creditsMenu;
		public static DotflowMenu deathMenu;
		public static DotflowMenu HUD;
		public static bool isMenuActive = true;

		private void Start()
		{
			mainMenu = mainMenuObj;
			settingsMenu = settingsMenuObj;
			creditsMenu = creditsMenuObj;
			deathMenu = deathMenuObj;
			HUD = HUDObj;
		}
	}
}
