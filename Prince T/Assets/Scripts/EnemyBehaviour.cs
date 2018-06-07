using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour
{
    Rigidbody2D rb_Enemy;
    Vector2 Speed;
    Vector3 Direction;
    // Use this for initialization
    void Start()
    {
        Speed = new Vector2(-5, 0);
        Direction = new Vector3(-1, 0, 0);
        rb_Enemy = GetComponent<Rigidbody2D>();
	}   
	
	// Update is called once per frame
	void Update ()
    {
        rb_Enemy.AddForce(Speed, ForceMode2D.Force);
	
        if(Physics2D.Raycast(transform.position, Direction, 1f))
        {
            Speed *= -1;
            Direction *= -1;
            Debug.Log("Enemy!");
        }

	}
}
