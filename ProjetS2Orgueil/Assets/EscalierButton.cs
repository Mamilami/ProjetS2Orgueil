using System;
using UnityEngine;
using TouchScript.Gestures;
using TouchScript.Hit;

public class EscalierButton : MonoBehaviour {

    public Transform minion;
    public Transform choiceMenu;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnEnable()
    {
        GetComponent<TapGesture>().Tapped += tappedHandler;
    }

    private void OnDisable()
    {
        GetComponent<TapGesture>().Tapped -= tappedHandler;
    }


    private void tappedHandler(object sender, EventArgs e)
    {

        //Mettre ici les instructions nécessaires pour faire des actions sur le minion
        if (transform.gameObject.name != "reset")
        {
            choiceMenu.gameObject.GetComponent<MenuBehaviour>().ResetMenu(false);
        }
        if (transform.gameObject.name == "escalier")
        {
            minion.gameObject.GetComponent<MinionInteraction>().escalier();
        }
        else if (transform.gameObject.name == "mur")
        {
            minion.gameObject.GetComponent<MinionInteraction>().mur();
        }
        else if (transform.gameObject.name == "pont")
        {
            minion.gameObject.GetComponent<MinionInteraction>().pont();
        }
        else if (transform.gameObject.name == "reset")
        {
            Application.LoadLevel(0);
        }
        else if (transform.gameObject.name == "retour")
        {

        }
    }
    

    public void initiate(Transform minions)
    {
        minion = minions;
    }
}
