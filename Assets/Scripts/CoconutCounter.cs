using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoconutCounter : MonoBehaviour
{

    public TextMeshProUGUI coconuts;

    public void SetCoconuts(int amount)
    {
        coconuts.text = amount.ToString();
    }
}
