using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Thread : MonoBehaviour {
	//loading thread list variables
	public Transform threadsListPos = null;
	public Button listPrefab;

	//loading thread variables
	public int threadid;
	public Text subiect;
	public Text poster;
	public Text mesaj;
	public Text replies;

	//reply variables
	public Text nouMesaj;

	string[] items;

	public void loadThread(){// incarcam o discutie din tabelul DiscussionThreads in functie de ThreadID; functia e apelata cand alegem o discutie din interfata
		StartCoroutine(loadThreadPHP());
		StartCoroutine(loadRepliesPHP());
	}
	public IEnumerator loadThreadPHP () {
		print ("debug0");
		WWWForm form = new WWWForm ();
		form.AddField ("threadidPost",threadid);
		WWW www = new WWW("http://localhost:81/socialnet/getthread.php",form);
		yield return www;
		string dataString = www.text;
		items = dataString.Split(';');
		subiect.text = "Subiect: " + GetDataValue (items [1], "Subiect:");// formatam informatia extrasa pentru a o afisa in interfata
		if ((GetDataValue (items [2], "OPID:")) != "") {
			poster.text = "PosterID " + (GetDataValue (items [2], "OPID:"));
		} else {
			poster.text = "PosterID: Anonymoose";
		}
		mesaj.text = "Mesaj: " + GetDataValue (items [3], "Mesaj:")+"\n_________________________________________________________________________";
		print ("debug1");
	}
	public IEnumerator loadRepliesPHP () {// incarcam raspunsurile la discutia curent deschisa; aceasta functie e apelata doar de functia de incarcare a thread-ului
		print ("debug2");
		WWWForm form = new WWWForm ();
		form.AddField ("threadidPost", threadid);
		WWW www = new WWW("http://localhost:81/socialnet/getthreadreplies.php",form);
		yield return www;
		string dataString = www.text;
		replies.text = "";
		items = dataString.Split(';');
		for (int i = 0; i < items.Length-2; i+=4) {
			if ((GetDataValue (items [i+2], "OPID:")) != "") {
				replies.text += "PosterID: " + (GetDataValue (items [i+2], "OPID:"));
			} else {
				replies.text += "PosterID: Anonymoose";
			}
			replies.text += "\nMesaj: " + GetDataValue (items [i + 3], "Mesaj:") + "\n_________________________________________________________________\n";
		}
		print ("debug3");
	}

	public void loadAllThreads(){// incarcam o lista a tuturor discutiilor deschise si afisam doar subiectul acesteia;
		StartCoroutine(loadAllThreadsPHP());
	}
	public IEnumerator loadAllThreadsPHP () {
		WWW www = new WWW("http://localhost:81/socialnet/getallthreads.php");
		yield return www;
		string dataString = www.text;
		items = dataString.Split(';');
		Button[] threadList = new Button[(items.Length + 4) / 4];// lista este sb forma de butoane ce incarca discutia deschisa cu functia de mai sus si ne trimite la pagina de discutii pentru a o putea vizualiza
		for (int i = 0; i < items.Length - 3; i=i+4) {
			threadList [i/4] = Instantiate(listPrefab) as Button;
			threadList [i/4].transform.SetParent(threadsListPos.transform, false);// formatam butoanele
			threadList [i / 4].GetComponent<RectTransform> ().sizeDelta.Set (500, 30);
			threadList [i / 4].GetComponentInChildren<Text> ().text = (((i+1)/4)+1).ToString() + "); Subiect: "+ GetDataValue (items [i + 1], "Subiect:");
			int newThreadId;
			int.TryParse (GetDataValue (items [i], "ThreadID:"), out newThreadId);
			threadList [i / 4].onClick.AddListener(delegate { threadid =  newThreadId; loadThread(); this.GetComponent<GlobalManager>().SetState(2); });
		}
	}

	public void insertReply(){// functia aceasta insereaza un nou raspuns in tabelul Raspunsuri pe care il leaga de discutia curent deschisa
		WWWForm form = new WWWForm ();
		form.AddField ("threadidPost", threadid);
		form.AddField ("opidPost", GetComponent<GlobalManager>().currentUser.persoanaID);
		form.AddField ("mesajPost", nouMesaj.text);

		WWW www = new WWW ("http://localhost:81/socialnet/insertReply.php", form);
	}

	//GeneralUse
	string GetDataValue(string data,string tag){
		string value = data.Substring (data.IndexOf (tag) + tag.Length);
		return value;
	}
}