using System;
using System.Collections;
using TouchScript.Gestures;
using TouchScript.Hit;
using UnityEngine;

public class ScreenTapPlayer : MonoBehaviour {

    public GameObject player;
    int axis = 0;
    public float deadZone; //Zone au milieu de l'écran insensible au touché pour les déplacements du joueur

    private void OnEnable() {
        GetComponent<PressGesture>().Pressed += pressedHandler;
        GetComponent<ReleaseGesture>().Released += releasedHandler;
    }

    private void OnDisable() {
        GetComponent<PressGesture>().Pressed -= pressedHandler;
        GetComponent<ReleaseGesture>().Released -= releasedHandler;
    }   

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void pressedHandler(object sender, EventArgs e) {
        var gesture = sender as PressGesture;
        ITouchHit hit;
        gesture.GetTargetHitResult(out hit);

        var hit2d = hit as ITouchHit2D;
        if (hit2d == null) return;

        if (hit2d.Point.x - deadZone > 0) {
            axis = 1;
        }
        else if (hit2d.Point.x + deadZone < 0) {
            axis = -1;
        }
        else {
            axis = 0;
        }
        
        StartCoroutine("MovePlayer");
    }

    private void releasedHandler(object sender, EventArgs e) {
        //release();
        StopCoroutine("MovePlayer");
    }

    private IEnumerator MovePlayer() {
        while (true) {
            player.GetComponent<PlayerMovement>().Move(axis);
            yield return null;
        }
    }
}
