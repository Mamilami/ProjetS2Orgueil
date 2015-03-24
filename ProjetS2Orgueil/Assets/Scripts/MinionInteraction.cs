using System;
using System.Collections;
using TouchScript.Gestures;
using TouchScript.Hit;
using UnityEngine;

public class MinionInteraction : MonoBehaviour {

    public float vit = 3.0f;
    private float lastVit = 3.0f;
    public float valeurRisque = 0.0f;
    public int comport = (int)comportement.marche;
    public int state = 0;
    public Animator anim;
    public float time;
    public bool isPaused = false; 
    Vector3 vitesse = new Vector2(3.0f, 0.0f);
	// Use this for initialization
	void Start () {
        print(transform.GetComponent<Rigidbody2D>().velocity);
	}
	
	// Update is called once per frame
	void Update () {
        if (isPaused == false)
        {
            if (comport == (int)comportement.marche)
            {
                valeurRisque += 0.03f;
                vit = lastVit;
            }
            else
            {
                vit = 0.0f;
            }
            if ((comport == (int)comportement.pont || comport == (int)comportement.mur || comport == (int)comportement.escalier) && gameObject.GetComponent<Rigidbody2D>() != null )
            {
                Destroy(gameObject.GetComponent<Rigidbody2D>());
            }
            if (valeurRisque > 10.0f)
            {
                comport = (int)comportement.peur;
            }
            vitesse = new Vector2(vit, 0.0f);
            transform.position += vitesse * Time.deltaTime;
            anim.SetInteger("comportement", (comport));
        }
	}

    public void go()
    {
        valeurRisque = 0.0f;
        comport = (int)comportement.marche;
    }

    public void escalier()
    {
        state = (int)stateMachine.orderEscalier;
        comport = (int)comportement.escalier;
        gameObject.layer = 12;
    }

    public void pont()
    {
        comport = (int)comportement.pont;
        gameObject.layer = 12;
    }

    public void mur()
    {
        comport = (int)comportement.mur;
        gameObject.layer = 10;
    }

    public void pause(bool pauseState)
    {
        if(pauseState)
        {
            isPaused = true;
        }
        else
        {
            isPaused = false;
        }
    } 

    public enum comportement
    {
        marche = 0,
        peur = 1,
        escalier = 2,
        mur = 3,
        pont = 4
    }

    public enum stateMachine
    {
        normal = 0,
        orderEscalier = 1
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == 10 )
        {
            vit = -vit;
            lastVit = vit;
            vitesse = new Vector2(vit, 0.0f);
            transform.Rotate(0.0f, 180.0f, 0.0f);
        }
        if (col.gameObject.layer==12)
        {
            if ( col.gameObject.transform.position.y >= (transform.position.y))
            {
                print(col.gameObject.transform.position.y + " " + transform.position.y);
                transform.position = transform.position + new Vector3(0.0f, 1.2f, 0.0f);
            }
        }

    }

    private void OnEnable() {
       // GetComponent<TapGesture>().Tapped += tappedHandler;
        GetComponent<PressGesture>().Pressed += pressedHandler;
        GetComponent<ReleaseGesture>().Released += releasedHandler;
    }

    private void OnDisable() {
        //GetComponent<TapGesture>().Tapped -= tappedHandler;
        GetComponent<PressGesture>().Pressed -= pressedHandler;
        GetComponent<ReleaseGesture>().Released -= releasedHandler;
    }


   /* private void tappedHandler(object sender, EventArgs e) {
        
        //Mettre ici les instructions nécessaires pour faire des actions sur le minion
        if (comport != (int)comportement.escalier)
        {
          //  valeurRisque = 0.0f;
            //comport = (int)comportement.marche;
        }
        print("hey");
    }*/

    private void pressedHandler(object sender, EventArgs e)
    {
        var gesture = sender as PressGesture;
        ITouchHit hit;
        gesture.GetTargetHitResult(out hit);

        var hit2d = hit as ITouchHit2D;
        StartCoroutine("countTime");
        
    }

    private void releasedHandler(object sender, EventArgs e)
    {
        //release();
        StopCoroutine("countTime");
        if(time>0.2f)
        {
            if (comport == (int)comportement.marche)
            {
                GameObject.Find("ChoiceMenu").GetComponent<MenuBehaviour>().Activate(this.transform);
            }
        }
        else
        {
            if (comport == (int)comportement.peur)
            {
                go();
            }
        }
        print("le temps est de " + time);
    }

    public IEnumerator countTime()
    {
        time=0.0f;
        while(true)
        {
            time += Time.deltaTime;
            yield return null; 
        }
        
    }
}
