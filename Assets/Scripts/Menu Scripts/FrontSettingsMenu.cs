using UnityEngine;
using System.Collections;

public class FrontSettingsMenu : MonoBehaviour {


	public GameObject settingsMenuRoot;
	public GameObject frontMenuRoot;
	
	public UIButton backButton;
	
	public UISlider volumeSlider;
	
	
	
	public void GoBack(GameObject go)
	{
		settingsMenuRoot.SetActive (false);
		frontMenuRoot.SetActive (true);
	}
	
	
	private void FixedUpdate()
	{
		AudioListener.volume = 1.0f - volumeSlider.value;
	}
	
	
	private void Start()
	{
		UIEventListener.Get (backButton.gameObject).onClick += GoBack;
	}
}
