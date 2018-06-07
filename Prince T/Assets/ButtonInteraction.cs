using UnityEngine;
using System.Collections;

public class ButtonInteraction : MonoBehaviour
{
    public GameObject[] Button;
    public GameObject gO_Interactable;
    WallButton[] ButtonScript;
    bool AllButtonsOn = false;
    // Use this for initialization
    void Start ()
    {
        

        ButtonScript = new WallButton[Button.Length];

	    for(int i = 0; i < Button.Length; i++)
        {
            ButtonScript[i] = Button[i].GetComponent<WallButton>();
        }
                
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Do check for each button
        //Make a bool for all clear
        
        if (AllButtonsOn == false)
        {
            if (HaveAllButtonsBeenPressed() == true)
                AllButtonsOn = true;

            
        }

        if (AllButtonsOn == true)
        {
            
            if (gO_Interactable.activeInHierarchy == true)
            {
                gO_Interactable.SetActive(false);                
            }
            else if (gO_Interactable.activeInHierarchy == false)
            {
                gO_Interactable.SetActive(true);              
            }

            Debug.Log("AllButtonsOn");

            //for loop
            TurnButtonsOff();

            AllButtonsOn = false;
        }
	}

    private bool HaveAllButtonsBeenPressed()
    {
        for(int i = 0; i < ButtonScript.Length; i++)
        {
            if (ButtonScript[i].BeenPressed == false)
                return false;
        }

        return true;
    }
    private void TurnButtonsOff()
    {
        for (int i = 0; i < ButtonScript.Length; i++)
        {
            ButtonScript[i].BeenPressed = false;
        }
    }
}
