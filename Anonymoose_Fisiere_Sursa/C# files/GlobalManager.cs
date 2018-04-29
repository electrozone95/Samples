using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalManager : MonoBehaviour {
	public user currentUser;
	public user otherUser = null;
	public GameObject threadScreen;
	public GameObject profileScreen;
	public GameObject dashboardScreen;//objects that contain the different pages/menus

	public int globalState = 0;//state of the program; 0 - profileScreen,1 - dashboard,2 - profileShow

	// Use this for initialization
	void Start () {
		SetState (0);//pornim programul in Profile Screen( starea 0)
	}
	
	public void SetState(int state)// functia aceasta gestioneaza starile programului pentru a afisa pagina dorita
	{
		threadScreen.SetActive (false);
		profileScreen.SetActive (false);
		dashboardScreen.SetActive (false);
		switch (state) {
		case 0:
			profileScreen.SetActive (true);
			globalState = 0;
			break;
		case 1:
			dashboardScreen.SetActive (true);
			globalState = 1;
			break;
		case 2:
			threadScreen.SetActive (true);
			globalState = 2;
			break;
		default:
			profileScreen.SetActive (true);
			globalState = 0;
			break;
		}
	}
	public void ResetScene()// resetam programul cand ne delogam pentru a sterge campurile scrise
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
