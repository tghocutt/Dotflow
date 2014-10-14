using UnityEngine;
using System.Collections;

public class FrontSettingsMenu : MonoBehaviour {

	public GameObject settingsMenuRoot;
	public GameObject frontMenuRoot;
	
	public UIButton backButton;
	public UISlider volumeSlider;
		
	public void GoBack(GameObject go)
	{
		PlayerPrefs.SetFloat ("volume", volumeSlider.value);
		PlayerPrefs.Save (); /* im saving the prefs to file here, it might not be a good idea */

		settingsMenuRoot.SetActive (false);
		frontMenuRoot.SetActive (true);
	}	
	
	private void FixedUpdate()
	{
		AudioListener.volume = 1.0f - volumeSlider.value;
	}	
	
	private void Start()
	{
		GetFromPlayerPrefs ();
		UIEventListener.Get (backButton.gameObject).onClick += GoBack;
	}

	public void GetFromPlayerPrefs() { /* add here manually any options that this screen needs to pull from prefs */
		if (PlayerPrefs.HasKey("volume"))
		    volumeSlider.value = PlayerPrefs.GetFloat("volume");

	}
}