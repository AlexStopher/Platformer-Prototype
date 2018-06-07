using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    

    public enum PlayerPrinceState
    {
        WithoutPrince,
        WithPrince,
        ThrownPrince,
        LobbedPrince,
        FlyingPrince,
        LandingPrince
    };

    public enum PlayerDirection
    {
        Left,
        Right
    };

    float m_Speed = 6f;
    bool m_CanJump = true;
    Rigidbody2D rb_Player;
    Vector3 Down;
    public float m_ThrowStrength = 0;
    public float m_ThrowStrengthStore = 0;
    public GameObject Main;
    public GameObject Prince;

    public PlayerPrinceState PrinceState;
    public PlayerDirection em_Direction;
   

    // Use this for initialization
    void Start ()
    {     
       
        PrinceState = PlayerPrinceState.WithoutPrince;

        rb_Player = GetComponent<Rigidbody2D>();
        Main = GameObject.FindWithTag("MainCamera");
        Down = transform.TransformDirection(Vector3.down);

    }
	
	// Update is called once per frame
	void Update ()
    {


        //Player movement - Replace with Forces?
        if (Input.GetKey(KeyCode.A))
        {
            transform.position = new Vector3(transform.position.x - m_Speed * Time.deltaTime, transform.position.y, transform.position.z);
            em_Direction = PlayerDirection.Left;
        }
        else if(Input.GetKey(KeyCode.D))
        {
            transform.position = new Vector3(transform.position.x + m_Speed * Time.deltaTime, transform.position.y, transform.position.z);
            em_Direction = PlayerDirection.Right;
        }

        if(Physics2D.Raycast(transform.position, Down , 1f))
        {
            m_CanJump = true;
           // Debug.Log("Something below!");
            
        }

        if(Input.GetKeyDown(KeyCode.E) && m_CanJump == true)
        {
            rb_Player.AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
            m_CanJump = false;
        }

        if (Input.GetKeyUp(KeyCode.R) && PrinceState == PlayerPrinceState.WithPrince) 
        {
            PrinceState = PlayerPrinceState.ThrownPrince;
        }
        else if(Input.GetKey(KeyCode.F) && PrinceState == PlayerPrinceState.WithPrince)
        {
            //Magic number, change
            m_ThrowStrength = m_ThrowStrength + 0.1f;
        }
        else if (Input.GetKeyUp(KeyCode.F) && PrinceState == PlayerPrinceState.WithPrince)
        {
            StartCoroutine("StoreThrowPower");
            PrinceState = PlayerPrinceState.LobbedPrince;

            m_ThrowStrength = 0;
        }

      
    }

    IEnumerator StoreThrowPower()
    {
        m_ThrowStrengthStore = m_ThrowStrength;
        yield return new WaitForEndOfFrame();
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (!Physics2D.Raycast(transform.position, Down, 1f))
            {
                DestroyObject(collision.gameObject);
            }
            else
            {
                DestroyObject(this.gameObject);
                //run game over animations
                //Run game over script
            }
            
        }   
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Prince Tosser")
        {
            if (Input.GetKeyDown(KeyCode.Q) && PrinceState == PlayerPrinceState.WithoutPrince)
            {
                PrinceState = PlayerPrinceState.WithPrince;
            }
        }
        
    }
}
