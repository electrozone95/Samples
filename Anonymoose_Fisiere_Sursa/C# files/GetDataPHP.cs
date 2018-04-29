using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetDataPHP : MonoBehaviour {
	public Text friendsText;
	public Text searchText;
	public Text grupa;
	public Text serie;
	public Text facultateId;
	string[] items;
	// Use this for initialization
	public void load_friends () {
		StartCoroutine (load_friendsPHP ());
	}

	public void searchByUni () {// Functie folosita sa porneasca o co-rutina( co-rutinele ruleaza in paralel cu programul principal. Astfel, ele au timp sa preia informatia de la server,nefiind limitate de durata cadrelor)
		StartCoroutine (searchPHP ());
	}

	public IEnumerator load_friendsPHP(){// cauta si preia informatiile despre utilizatorii prieteni cu utilizatorul logat, in functie de grupurile lor comune
		WWWForm form = new WWWForm ();
		form.AddField ("persoanaidPost", GetComponent<GlobalManager>().currentUser.persoanaID);// cauta in functie de id-ul persoanelor
		WWW downData = new WWW("http://localhost:81/socialnet/getgroupsbyuser.php",form);// preia informatia de la adresa url precizata
		yield return downData;
		string dataString = downData.text;// introduce informatia preluata intr-o variabila string
		friendsText.text = "";
        items = dataString.Split(';');//despartim informatia preluata in "iteme" in functie de caracterul ";"
		friendsText.text = "Lista prieteni( cu grupuri comune):\n";// prelucreaza informatia si o formateaza pentru a o afisa in interfata
		for (int i = 0; i < items.Length-1; i+=2) {
			friendsText.text += "\n" + (i/2+1).ToString() + ") " + GetDataValue (items [i], "Nume:") +
				" " + GetDataValue (items [i + 1], "Prenume:")+
				"\n-------------------------------";
		}
	}

	public IEnumerator searchPHP(){// cauta si preia informatiile despre utilizatorii de la grupa, seria si facultatea precizate
		WWWForm form = new WWWForm ();
		int gr;
		int.TryParse (grupa.text, out gr);
		int fid;
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
				"\n-------------------------------";
			}
		} else {
			searchText.text = "Nu s-a gasit nicio persoana :(";
		}
	}

	string GetDataValue(string data,string tag){// cauta cuvinte in functie de tag-uri precizate
		string value = data.Substring (data.IndexOf (tag) + tag.Length);
		return value;
	}
}
