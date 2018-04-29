using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InsertUser : MonoBehaviour {

	string siteURL = "http://localhost:81/socialnet/insertuser.php";
    // variabile ce preiau informatia despre noul utilizator din interfata
	public Text nume;
	public Text prenume;
	public Text datanasterii;
	public Text sex;
	public Text locatie;
	public Text serie;
	public Text grupa;
	public Text facultateid;
	public Text avatarid;
	public Text username;
	public Text parola;

	// Use this for initialization
	void Start () {
		nume.text = "";
    	prenume.text = "";
    	datanasterii.text = "";
		sex.text = "";
		locatie.text = "";
		serie.text = "";
		grupa.text = "";
		avatarid.text = "";
		facultateid.text = "";
		parola.text = "";
		username.text = "";
	}

	public void CreateUser(){// creem un utilizator nou cu informatiile precizate.( pentru functia de creere a unui nou uti.izator din interfata)
		WWWForm form = new WWWForm ();
		form.AddField ("numePost", nume.text);
		form.AddField ("prenumePost", prenume.text);
		form.AddField ("datanasteriiPost", datanasterii.text);
		form.AddField ("sexPost", sex.text);
		form.AddField ("locatiePost", locatie.text);
		form.AddField ("seriePost", serie.text);
		form.AddField ("grupaPost", grupa.text);
		form.AddField ("avataridPost", avatarid.text);
		form.AddField ("facultateidPost", facultateid.text);
		form.AddField ("usernamePost", username.text);
		form.AddField ("parolaPost", parola.text);

		WWW www = new WWW (siteURL, form);
	}
}
