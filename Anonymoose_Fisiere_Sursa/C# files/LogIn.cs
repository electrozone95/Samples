using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogIn : MonoBehaviour {
	public user currentUser;// referinta publica la obiectul ce va retine informatiile despre utilizatorul curent logat
	string siteURL = "http://localhost:81/socialnet/login.php";

	public Text username;
	public Text parola;

	public Text newPassword;

	string[] items;

	// Use this for initialization
	void Start () {
		//debugText.text = "";
		parola.text = "";
		username.text = "";
		
	}

	public void logInUser(){
		StartCoroutine(getUser());
	}
	public IEnumerator getUser (){// incarcam datele utilizatorului ce se logheaza; acestea persista pana la delogare
		WWWForm form = new WWWForm ();
		form.AddField ("usernamePost", username.text);// folosim numele si parola utilizatorului pentru a-l identifica
		form.AddField ("parolaPost", parola.text);
		WWW www = new WWW (siteURL,form);
		yield return www;
		string dataString = www.text;
		items = dataString.Split(';');
		if (items [0] != "error") {
			currentUser.persoanaID = int.Parse (GetDataValue (items [0], "PersoanaID:"));
			currentUser.nume = GetDataValue (items [1], "Nume:");
			currentUser.prenume = GetDataValue (items [2], "Prenume:");
			currentUser.datanasterii = GetDataValue (items [3], "Data nasterii:");
			currentUser.sex = GetDataValue (items [4], "Sex:")[0];
			currentUser.locatie = GetDataValue (items [5], "Locatie:");
			currentUser.serie = GetDataValue (items [6], "Serie:");
			currentUser.grupa = GetDataValue (items [7], "Grupa:");
			currentUser.avatarid = int.Parse(GetDataValue (items [8], "AvatarID:"));
			currentUser.username = GetDataValue (items [9], "Username:");
			currentUser.parola = GetDataValue (items [10], "Password:");
			currentUser.facultateid = int.Parse(GetDataValue (items [11], "FacultateID:"));

			form = new WWWForm ();
			form.AddField ("facultateidPost", currentUser.facultateid);
			www = new WWW("http://localhost:81/socialnet/getFacultate.php",form);
			yield return www;
			currentUser.facultate_nume = www.text;

			GetComponent<GlobalManager> ().SetState (1);
			currentUser.PrintUserProfile ();
		} else {
			print ("erroare: nu exista userul");
		}
	}
	public void changePassword(){
		StartCoroutine(changePasswordPHP());
	}
	public IEnumerator changePasswordPHP (){//schimba parola utilizatorului curent logat
		WWWForm form = new WWWForm ();
		form.AddField ("usernamePost", currentUser.username);
		form.AddField ("parolaPost", newPassword.text);
		WWW www = new WWW ("http://localhost:81/socialnet/changepassword.php,form",form);
		yield return www;
	}

	string GetDataValue(string data,string tag){
		string value = data.Substring (data.IndexOf (tag) + tag.Length);
		return value;
	}
}
