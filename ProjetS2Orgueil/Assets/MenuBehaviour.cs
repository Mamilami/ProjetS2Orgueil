using UnityEngine;
using System.Collections;

public class MenuBehaviour : MonoBehaviour {

    public Transform escalier;
    public Transform mur;
    public Transform pont;
    public Transform retour;
	// Use this for initialization
	void Start () {
        ResetMenu(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Activate(Transform minions)
    {
        ResetMenu(true);
        escalier.gameObject.GetComponent<EscalierButton>().initiate(minions);
        mur.gameObject.GetComponent<EscalierButton>().initiate(minions);
        pont.gameObject.GetComponent<EscalierButton>().initiate(minions);
        GameObject[] tmp = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i<tmp.Length ; i++)
        {
            if(tmp[i].name !="Player")
            {
                tmp[i].GetComponent<MinionInteraction>().pause(true);
            }
        }
    }

    public void Deactivate()
    {
        GameObject[] tmp = GameObject.FindGameObjectsWithTag("Player");
        print(tmp.GetLength(0));
        for (int i = 0; i < tmp.Length; i++)
        {
            if (tmp[i].name != "Player")
            {
                print("Coucou je suis "+tmp[i].name);
                tmp[i].GetComponent<MinionInteraction>().pause(false);
            }
        }
    }

    public void ResetMenu(bool toggle)
    {
        escalier.gameObject.SetActive(toggle);
        mur.gameObject.SetActive(toggle);
        pont.gameObject.SetActive(toggle);
        retour.gameObject.SetActive(toggle);
        Deactivate();
    }

}
