using UnityEngine;
using System.Collections;

public class movement : MonoBehaviour {

    public float vit = 3.0f;
    private float lastVit = 3.0f;
    public float valeurRisque = 0.0f;
    public int comport = (int)comportement.marche;
    public int state = 0;
    public Animator anim; 
    Vector3 vitesse = new Vector2(3.0f, 0.0f);

    void start()
    {
        print(transform.rigidbody2D.velocity);
    }
	
	// Update is called once per frame
	void Update () {
        if(comport == (int)comportement.marche)
        {
            valeurRisque += 0.03f;
            vit = lastVit;
        }
        else if(comport == (int)comportement.peur)
        {
            vit = 0.0f;
        }
        else if (comport == (int)comportement.escalier)
        {
            vit = 0.0f;
        }
        if(valeurRisque > 10.0f)
        {
            comport = (int)comportement.peur;
        }
        vitesse = new Vector2(vit, 0.0f);
        transform.position += vitesse * Time.deltaTime;
        anim.SetInteger("comportement", (comport));

	}
    public void go()
        {
            valeurRisque = 0.0f;
            comport = (int)comportement.marche; 
        }

    public void escalier()
    {
        state = (int)stateMachine.orderEscalier;
    }

    public enum comportement
    {
        marche =0,
        peur = 1,
        escalier = 2
    }

    public enum stateMachine
    {
        normal = 0,
        orderEscalier = 1
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "wall")
        {
            vit = -vit;
            lastVit = vit;
            vitesse = new Vector2(vit, 0.0f);
            transform.Rotate(0.0f, 180.0f,0.0f);
        }
        if(col.gameObject.tag == "Player" && col.gameObject != this .gameObject && comport != (int) comportement.escalier)
        {
            Debug.Log(col.gameObject.GetComponent<movement>().state);
            if(col.gameObject.GetComponent<movement>().state== (int)stateMachine.orderEscalier && col.gameObject.transform.position.y>=(transform.position.y-0.5f))
            {
                Debug.Log(col.gameObject.transform.position.y+" "+transform.position.y);
              transform.position = transform.position + new Vector3(0.0f, 1.2f, 0.0f);
            }
            else
            {
                vit = -vit;
                lastVit = vit;
                vitesse = new Vector2(vit, 0.0f);
                transform.Rotate(0.0f, 180.0f, 0.0f);
            }
        }
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
         if (col.gameObject.tag == "escalier" && state == (int)stateMachine.orderEscalier)
        {
            comport = (int)comportement.escalier;
        }
    }

   
}
