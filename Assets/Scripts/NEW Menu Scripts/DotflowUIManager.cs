﻿using UnityEngine;
using System.Collections;

namespace Dotflow
{
	public class DotflowUIManager : MonoBehaviour {

		public DotManager dotManager;

		public MainMenuScript mainMenuObj;
		public SettingsMenuScript settingsMenuObj;
		public CreditsMenuScript creditsMenuObj;
		public DeathMenuScript deathMenuObj;
		public HUDMenuScript HUDObj;
		public StoreMenuScript StoreMenuObj;
		public BoosterSelectMenuScript boosterSelectob;

		public static MainMenuScript mainMenu;
		public static SettingsMenuScript settingsMenu;
		public static CreditsMenuScript creditsMenu;
		public static DeathMenuScript deathMenu;
		public static StoreMenuScript storeMenu;
		public static HUDMenuScript HUD;
		public static BoosterSelectMenuScript boosterMenu;
		public static bool isMenuMoving = false;
		public static bool isMenuActive = true;
		public static DotManager _dotManager;

		//keeps track of whether the menus are moving or not.
		private void Update()
		{
			if (!mainMenuObj.childrenMoving &&
				!settingsMenuObj.childrenMoving &&
				!creditsMenuObj.childrenMoving &&
				!deathMenuObj.childrenMoving &&
				!HUDObj.childrenMoving &&
			    !StoreMenuObj.childrenMoving) 
			{
				isMenuMoving = false;
			} else {
				isMenuMoving = true;
			}
		}


		//sets all the public static objects from the
		//inspector driven objects.
		private void Start()
		{
			mainMenu = mainMenuObj;
			settingsMenu = settingsMenuObj;
			creditsMenu = creditsMenuObj;
			deathMenu = deathMenuObj;
			HUD = HUDObj;
			storeMenu = StoreMenuObj;
			boosterMenu = boosterSelectob;
			_dotManager = dotManager;
		}
	}
}
