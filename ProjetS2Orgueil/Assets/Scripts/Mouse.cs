using UnityEngine;
using System.Collections;

public class Mouse : MonoBehaviour {
    RaycastHit2D hit;
    Vector3 mousePosition;
    Vector2 mousePosition2D;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        hit=Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition),Vector2.zero);
        if (Input.GetMouseButtonDown(0))
        {
            if(hit.collider != null)
            {
                if (hit.collider.tag == "Player")
                {
                    hit.collider.gameObject.GetComponent<movement>().go();
                }
            }       
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (hit.collider != null)
            {
                if (hit.collider.tag == "Player")
                {
                    hit.collider.gameObject.GetComponent<movement>().escalier();
                }
            }
        }
	}
}
