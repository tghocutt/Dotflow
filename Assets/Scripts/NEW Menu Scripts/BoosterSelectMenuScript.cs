﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Dotflow
{
	public class BoosterSelectMenuScript : DotflowMenu {

		public DotflowElement[] boosterMenuElements = new DotflowElement[0];
		public bool childrenMoving = false;

		public UIButton[] boosters = new UIButton[0];
		public UILabel gemLabel;
		public int maxBoostersSelectable;
		public int currentBoostersSelected;

		public UIGrid upperPanel;
		public UIGrid lowerPanel;

		private List<GameObject> gos = new List<GameObject> ();

		//implemented from the base class
		public override void Open(DotflowElement[] elements)
		{
			base.Open (elements);
			gemLabel.text = PlayerPrefs.GetInt("gemTotal").ToString();
		}
		
		//implemented from the base class
		public override void Close(DotflowElement[] elements)
		{
			base.Close (elements);
			foreach(GameObject go in gos)
			{
				go.transform.parent = lowerPanel.transform;
				go.GetComponent<BoosterItemBehavior>().selected = false;
			}
			lowerPanel.Reposition();
			currentBoostersSelected = 0;
		}


		//resumes the current game
		private void Play(GameObject go)
		{
			if (!DotflowUIManager.isMenuMoving) 
			{
				DotflowUIManager.isMenuActive = false;
				Close (boosterMenuElements);
				DotflowUIManager.HUD.Open (DotflowUIManager.HUD.hudElements);
			}
		}

		private void Back(GameObject go)
		{
			if (!DotflowUIManager.isMenuMoving) 
			{
				//DotflowUIManager._dotManager.RestartGame();
				Close (boosterMenuElements);
				DotflowUIManager.mainMenu.Open (DotflowUIManager.mainMenu.mainMenuElements);
			}
		}


		private void ProcessBoosterSelect(GameObject go)
		{
			BoosterItemBehavior bib = go.GetComponent<BoosterItemBehavior> ();
			if(bib.selected)
			{
				PlayerPrefs.SetInt ("gemTotal", PlayerPrefs.GetInt("gemTotal") + bib.cost);
				gemLabel.text = PlayerPrefs.GetInt("gemTotal").ToString();
				go.transform.parent = lowerPanel.transform;
				currentBoostersSelected -= 1;
				bib.selected = !bib.selected;
				for(int i = 0; i < gos.Count; i++)
				{
					if(gos[i] == go)
					{
						gos.RemoveAt(i);
					}
				}
			} else {
				if(currentBoostersSelected < maxBoostersSelectable && PlayerPrefs.GetInt("gemTotal") >= bib.cost)
				{
					PlayerPrefs.SetInt ("gemTotal", PlayerPrefs.GetInt("gemTotal") - bib.cost);
					gemLabel.text = PlayerPrefs.GetInt("gemTotal").ToString();
					go.transform.parent = upperPanel.transform;
					currentBoostersSelected += 1;
					bib.selected = !bib.selected;
					gos.Add(go);
				}
			}
			lowerPanel.Reposition ();
			upperPanel.Reposition();
		}


		private void Update()
		{
			upperPanel.Reposition ();
		}

		
		private void Start () 
		{
			UIEventListener.Get (boosterMenuElements [0].gameObject).onClick += Back;
			UIEventListener.Get (boosterMenuElements [1].gameObject).onClick += Play;

			UIEventListener.Get (boosters [0].gameObject).onClick += ProcessBoosterSelect;
			UIEventListener.Get (boosters [1].gameObject).onClick += ProcessBoosterSelect;
			UIEventListener.Get (boosters [2].gameObject).onClick += ProcessBoosterSelect;
			UIEventListener.Get (boosters [3].gameObject).onClick += ProcessBoosterSelect;
			UIEventListener.Get (boosters [4].gameObject).onClick += ProcessBoosterSelect;
		}
	}
}
