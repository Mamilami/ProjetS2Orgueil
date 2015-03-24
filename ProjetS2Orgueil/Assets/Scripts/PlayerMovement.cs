using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public Transform PreventFallBox;
    public bool cantFall = false;
    float boxRadius = 0.2f;
    public LayerMask whatIsToPrevent;
    public Animator anim;

    public float speed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        cantFall = Physics2D.OverlapCircle(PreventFallBox.position, boxRadius, whatIsToPrevent);
	}

    public void Move(int axis) {
        if (cantFall) {
            transform.position += new Vector3(axis * speed * Time.deltaTime, 0);
        }

        //if I move my character the opposite direction he was facing
        bool oppositeDirection = (transform.localScale.x < 0 && axis > 0) || (transform.localScale.x > 0 && axis < 0);
        //print(oppositeDirection);
        if (oppositeDirection) {
            Vector3 newOrientation = new Vector3(-transform.localScale.x, transform.localScale.y);
            transform.localScale = newOrientation;
        }

    }

    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.layer == 12 && col.gameObject != this.gameObject)
        {
            if (col.gameObject.transform.position.y >= (transform.position.y - 0.02))
            {
                print(col.gameObject.transform.position.y + " " + transform.position.y);
                transform.position = transform.position + new Vector3(0.0f, 1.2f, 0.0f);
            }
        }

    }

}
