using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Dotflow
{
	public class BoosterSelectMenuScript : DotflowMenu {

		public DotflowElement[] boosterMenuElements = new DotflowElement[0];
		public bool childrenMoving = false;
		public AudioManager audioManager;

		public UIButton[] boosters = new UIButton[0];
	
		public UILabel gemLabel;
		public int maxBoostersSelectable;
		public int currentBoostersSelected;

		public Transform[] upperslots = new Transform[0];
		public UIScrollView lowerScrollView;

		public UIGrid upperPanel;
		public UIGrid lowerPanel;

		private List<GameObject> gos = new List<GameObject> ();
		private UIButton[] upperButtons = new UIButton[3];

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
			audioManager.menuFX [0].Play ();
			if (!DotflowUIManager.isMenuMoving) 
			{
				DotflowUIManager.isMenuActive = false;
				Close (boosterMenuElements);
				DotflowUIManager.HUD.Open (DotflowUIManager.HUD.hudElements);
			}
		}

		private void Back(GameObject go)
		{
			audioManager.menuFX [0].Play ();
			if (!DotflowUIManager.isMenuMoving) 
			{
				//DotflowUIManager._dotManager.RestartGame();
				Close (boosterMenuElements);
				DotflowUIManager.mainMenu.Open (DotflowUIManager.mainMenu.mainMenuElements);
			}
		}


		private void ProcessBoosterSelect(GameObject go)
		{
			audioManager.menuFX [0].Play ();
			BoosterItemBehavior bib = go.GetComponent<BoosterItemBehavior> ();

			if(currentBoostersSelected < maxBoostersSelectable && PlayerPrefs.GetInt("gemTotal") >= bib.cost)
			{
				for(int i = 0; i < upperButtons.Length; i++)
				{
					if(upperButtons[i] == null)
					{
						GameObject guu = NGUITools.AddChild(upperslots[i].gameObject, bib.upperButtonPrefab);
						upperButtons[i] =  guu.GetComponent<UIButton>();
						upperButtons[i].GetComponent<UpperButtonBehavior>().boosterScript = this;
						guu.transform.position = upperslots[i].transform.position;
						break;
					}
				}

				PlayerPrefs.SetInt ("gemTotal", PlayerPrefs.GetInt("gemTotal") - bib.cost);
				gemLabel.text = PlayerPrefs.GetInt("gemTotal").ToString();
				currentBoostersSelected += 1;

				gos.Add(go);
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
