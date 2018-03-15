// This script manages data collection regarding current user friends
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetDataPHP : MonoBehaviour {
	public Text friendsText;// text showing ifo about friends
	public Text searchText;
	public Text grupa;
	public Text serie;
	public Text facultateId;
	string[] items;
	
	// make public sync functions that start the coroutines so that we can attach them to in-engine buttons
	public void load_friends () {
		StartCoroutine (load_friendsPHP ());
	}
	public void searchByUni () {
		StartCoroutine (searchPHP ());
	}
	
	// load info about friends of the currently logged user; friends are those who share a group with you (from database, by user id)
	public IEnumerator load_friendsPHP(){
		WWWForm form = new WWWForm ();
		form.AddField ("persoanaidPost", GetComponent<GlobalManager>().currentUser.persoanaID);
		WWW downData = new WWW("http://localhost:81/socialnet/getgroupsbyuser.php",form);
		yield return downData;
		string dataString = downData.text;
		friendsText.text = "";
		items = dataString.Split(';');// info split with ';' on the page
		friendsText.text = "Lista prieteni( cu grupuri comune):\n";
		for (int i = 0; i < items.Length-1; i+=2) {
			friendsText.text += "\n" + (i/2+1).ToString() + ") " + GetDataValue (items [i], "Nume:") +
				" " + GetDataValue (items [i + 1], "Prenume:")+
				"\n-------------------------------";// format the info in a legible way
		}
	}
	
	// search user in database by parameters(grupa/serie/facultate)
	public IEnumerator searchPHP(){
		WWWForm form = new WWWForm ();
		int gr;// stores 'grupa' as an integer
		int.TryParse (grupa.text, out gr);
		int fid;// stores 'facultateId' as an integer
		int.TryParse (facultateId.text, out fid);
		form.AddField ("grupaPost", gr);
		form.AddField ("seriePost", serie.text);
		form.AddField ("facultateidPost", fid);
		WWW downData = new WWW("http://localhost:81/socialnet/usersbyuni.php",form);
		yield return downData;
		string dataString = downData.text;
		searchText.text = "";
		items = dataString.Split(';');
		if (items.Length > 1) {
			searchText.text = "Lista persoane gasite:\n";
			for (int i = 0; i < items.Length - 1; i += 2) {
				searchText.text += "\n" + (i / 2 + 1).ToString () + ") " + GetDataValue (items [i], "Nume:") +
				" " + GetDataValue (items [i + 1], "Prenume:") +
				"\n-------------------------------";// format the info in a legible way
			}
		} else {
			searchText.text = "Nu s-a gasit nicio persoana :(";// print soft error message when no friends are found
		}
	}

	// returns substrings that start with the tag given
	string GetDataValue(string data,string tag){
		string value = data.Substring (data.IndexOf (tag) + tag.Length);
		return value;
	}
}
