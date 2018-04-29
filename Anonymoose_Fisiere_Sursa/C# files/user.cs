using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class user : MonoBehaviour {

	public Text debugText;
	
    //user info variables
	public int persoanaID;
	public string nume;
	public string prenume;
	public string datanasterii;
	public char sex;
	public string locatie;
	public string serie;
	public string grupa;
	public int facultateid;
	public int avatarid;
	public string username;
	public string parola;

	//implementation variables
	public string facultate_nume = "error";


	void Start () {

		persoanaID = 0;
		nume = "";
		prenume = "";
		datanasterii = "000x-0x-0x";
		sex = 'M';
		locatie = "";
		serie = "";
		grupa = "";
		avatarid = 0;
		facultateid = 0;
		parola = "";
		username = "";
	}
	void Update () {
		
	}
	public void LoadById(int i)// incarcam un utilizator in functie de ID-ul acestuia( pentru functia de testare de incarcare a utilizatorului)
	{
		StartCoroutine (lbi (i));
	}
	public IEnumerator lbi (int i){
		WWWForm form = new WWWForm ();
		form.AddField ("persoanaidPost", i);
		WWW www = new WWW ("http://localhost:81/socialnet/userbyid.php",form);
		yield return www;
		string dataString = www.text;
		string[] items = dataString.Split(';');
		if (items [0] != "error") {
			persoanaID = i;
			nume = GetDataValue (items [1], "Nume:");
			prenume = GetDataValue (items [2], "Prenume:");
			datanasterii = GetDataValue (items [3], "Data nasterii:");
			sex = GetDataValue (items [4], "Sex:") [0];
			locatie = GetDataValue (items [5], "Locatie:");
			serie = GetDataValue (items [6], "Serie:");
			grupa = GetDataValue (items [7], "Grupa:");
			avatarid = int.Parse (GetDataValue (items [8], "AvatarID:"));
			username = GetDataValue (items [9], "Username:");
			parola = GetDataValue (items [10], "Password:");
			facultateid = int.Parse (GetDataValue (items [11], "FacultateID:"));
			form = new WWWForm ();
			form.AddField ("facultateidPost", facultateid);
			www = new WWW("http://localhost:81/socialnet/getFacultate.php",form);
			yield return www;
			facultate_nume = www.text;

			PrintUserProfile();
		}
	}

	public void DeleteUser()// Stergem utilizatorul curent logat
	{
		StartCoroutine (delet());
	}
	public IEnumerator delet (){
		WWWForm form = new WWWForm ();
		form.AddField ("persoanaidPost", persoanaID);// cautam utilizatorul curent logat pentru stergere in functie de ID-ul sau
		WWW www = new WWW ("http://localhost:81/socialnet/deletepers.php",form);
		yield return www;
	}
	public void PrintUserProfile()// afisam informatiile pentru utilizatorul ce are asociat acest script
	{
		debugText.text ="\n" + "\nNume: " + nume + "\nPrenume: " + prenume
			+ "\nSex: " + sex.ToString ()+ "\nData nasterii: " + datanasterii
			+ "\nLocatie: " + locatie + "\nSerie: " + serie
			+ "\nGrupa: " + grupa + "\nFacultate: " + facultate_nume;
	}
	string GetDataValue(string data,string tag){
		string value = data.Substring (data.IndexOf (tag) + tag.Length);
		return value;
	}
}
