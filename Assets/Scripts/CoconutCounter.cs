using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoconutCounter : MonoBehaviour
{

    public TextMeshProUGUI coconuts;

    private int total = 0;

    public void SetCoconuts(int amount)
    {
        coconuts.text = amount.ToString();
    }

    public void IncrementCoconuts()
    {
        total++;
        Debug.Log(total);
        coconuts.text = total.ToString();
    }

    public int GetCoconuts()
    {
        return total;
    }
}
