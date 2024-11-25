using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HelpingTextScript : MonoBehaviour
{
    private GameObject rightHand;
    private GameObject leftHand;
    private bool axeEquipped;
    private bool thingEquipped;
    private TextMeshProUGUI interactionText; // Static to ensure only one instance controls the UI
    public GameObject interactionImage; // Static to ensure only one instance controls the UI

    // Start is called before the first frame update
    void Start()
    {
        rightHand = GameObject.FindWithTag("RightHand");
        leftHand = GameObject.FindWithTag("LeftHand");
        interactionText = interactionImage.GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        HasAxe();
        HasCarry();
    }



    void HasAxe()
    {
        if (!axeEquipped)
        {
            if (rightHand != null)
            {
                if (rightHand.transform.childCount >= 6)
                {
                    axeEquipped = true;
                }
            }
            else
            {
                Debug.LogError("rightHand object not found!");
            }
        }
    }

    void HasCarry()
    {
        if (leftHand != null)
        {
            if (leftHand.transform.childCount >= 6)
            {
                thingEquipped = true;
                HideInteractionText();
            }
            else
            {
                thingEquipped = false;
            }

        }
        else
        {
            Debug.LogError("LeftHand object not found!");
        }
    }

    void HideInteractionText()
    {
        interactionImage.SetActive(false);
    }

    void ShowInteractionText()
    {
        interactionImage.SetActive(true);
    }

    public void AttemptHelp(string text)
    {
        if (thingEquipped)
        {
            HideInteractionText();
        } else    
        {
            ShowInteractionText();
            interactionText.text = text; 
        }        
    }

    public void NoMore()
    {
        HideInteractionText();
    }

    float GetDistance(Vector3 obj)
    {
        return Vector3.Distance(transform.position, obj);
    }
}
