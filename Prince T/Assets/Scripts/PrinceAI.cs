using UnityEngine;
using System.Collections;

public class PrinceAI : MonoBehaviour
{

    GameObject gO_Player;
    PlayerScript p_Script;
    Rigidbody2D rb_Prince;

    Vector3 PrinceOffset;
    Quaternion PrinceCarriedRotation;

    float m_Speed;
    public float m_ThrowStrength;

    // Use this for initialization
    void Start ()
    {
        gO_Player = GameObject.FindGameObjectWithTag("Player");
        p_Script = gO_Player.GetComponent<PlayerScript>();

        rb_Prince = GetComponent<Rigidbody2D>();

        PrinceOffset = new Vector3(0, 0.5f, 0);
        PrinceCarriedRotation = Quaternion.Euler(0, 0, -90);
    }
	
	// Update is called once per frame
	void Update ()
    {

        //WIthout Prince

        if (p_Script.PrinceState == PlayerScript.PlayerPrinceState.WithoutPrince)
        {

        }

        if (p_Script.PrinceState == PlayerScript.PlayerPrinceState.WithPrince)
        {
            transform.position = gO_Player.transform.position + PrinceOffset;
            transform.rotation = PrinceCarriedRotation;
            rb_Prince.isKinematic = true;
            rb_Prince.gravityScale = 0f;
        }
       else if(p_Script.PrinceState == PlayerScript.PlayerPrinceState.ThrownPrince)
        {
            rb_Prince.isKinematic = false;

            if (p_Script.em_Direction == PlayerScript.PlayerDirection.Left)
            {
                rb_Prince.AddForce(new Vector2(-10, 0), ForceMode2D.Impulse);
            }
            else if (p_Script.em_Direction == PlayerScript.PlayerDirection.Right)
            {
                rb_Prince.AddForce(new Vector2(20,0), ForceMode2D.Impulse);

            }

            StartCoroutine("ThrowEnd");
            p_Script.PrinceState = PlayerScript.PlayerPrinceState.FlyingPrince;                 
        }
        else if(p_Script.PrinceState == PlayerScript.PlayerPrinceState.LobbedPrince)
        {
            rb_Prince.isKinematic = false;
            m_ThrowStrength = p_Script.m_ThrowStrengthStore;

            if (p_Script.em_Direction == PlayerScript.PlayerDirection.Left)
            {
                rb_Prince.AddForce(new Vector2(-3, m_ThrowStrength), ForceMode2D.Impulse);
            }
            else if (p_Script.em_Direction == PlayerScript.PlayerDirection.Right)
            {
                rb_Prince.AddForce(new Vector2(3, m_ThrowStrength), ForceMode2D.Impulse);

            }

            StartCoroutine("ThrowEnd");
            m_ThrowStrength = 0;
            p_Script.PrinceState = PlayerScript.PlayerPrinceState.FlyingPrince;
        }
        else if (p_Script.PrinceState == PlayerScript.PlayerPrinceState.LandingPrince)
        {
            //Speed function
            StartCoroutine("PrinceStandup");
            p_Script.PrinceState = PlayerScript.PlayerPrinceState.WithoutPrince;
        }

 
    }

    IEnumerator ThrowEnd()
    {
        
        yield return new WaitForSeconds(0.6f);
        rb_Prince.gravityScale = 1f;

    }

    IEnumerator PrinceStandup()
    {
        yield return new WaitForSeconds(0.2f);         
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && p_Script.PrinceState == PlayerScript.PlayerPrinceState.FlyingPrince)
        {
            p_Script.PrinceState = PlayerScript.PlayerPrinceState.LandingPrince;
            Debug.Log("Prince Landed");
        }
    }


}
