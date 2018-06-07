using UnityEngine;
using System.Collections;

public class WallButton : MonoBehaviour
{
    public bool BeenPressed;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	   
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Prince Tosser")
            BeenPressed = true;

        if (collision.tag == "Player")
            BeenPressed = true;

    }
}
