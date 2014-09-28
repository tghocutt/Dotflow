using UnityEngine;
using System.Collections;

public class FrontCreditsMenu : MonoBehaviour {


	public GameObject frontCreditsRoot;
	public GameObject frontMenuRoot;
	
	public UIButton backButton;
	
	
	
	public void GoBack(GameObject go)
	{
		frontCreditsRoot.SetActive (false);
		frontMenuRoot.SetActive (true);
	}
	
	
	private void Start()
	{
		UIEventListener.Get (backButton.gameObject).onClick += GoBack;
	}
}
