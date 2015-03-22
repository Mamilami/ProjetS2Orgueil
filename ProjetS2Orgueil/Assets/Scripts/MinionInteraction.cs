using System;
using UnityEngine;
using TouchScript.Gestures;
using TouchScript.Hit;

public class MinionInteraction : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnEnable() {
        GetComponent<TapGesture>().Tapped += tappedHandler;
    }

    private void OnDisable() {
        GetComponent<TapGesture>().Tapped -= tappedHandler;
    }


    private void tappedHandler(object sender, EventArgs e) {
        
        //Mettre ici les instructions nécessaires pour faire des actions sur le minion
        print("hey");
    }
}
