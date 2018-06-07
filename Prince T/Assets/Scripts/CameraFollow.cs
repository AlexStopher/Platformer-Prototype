using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    GameObject Player;
    public Vector3 Offset;

	// Use this for initialization
	void Start ()
    {
        Player = GameObject.FindWithTag("Player");
        Offset = transform.position - Player.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Player.transform.position.x > transform.position.x + 1 || Player.transform.position.x < transform.position.x - 1)
        {

            transform.position = new Vector3(Mathf.Lerp(transform.position.x, Player.transform.position.x + Offset.x, 0.1f), transform.position.y, Player.transform.position.z + Offset.z);

            
        }

        if(Player.transform.position.y > transform.position.y + 1 || Player.transform.position.y < transform.position.y - 1)
        {
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, Player.transform.position.y + Offset.x, 0.1f), transform.position.z);
        }

    }


}
