using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoconutHelpScript : MonoBehaviour
{
    public GameObject player;
    public float carryDistanceThreshold = 2f; // Distance within which the player can carry the coconut
    private HelpingTextScript helpingTextScript;

    // Start is called before the first frame update
    void Start()
    {
        helpingTextScript = player.GetComponent<HelpingTextScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (GetDistance(player.transform.position) < carryDistanceThreshold)
        {
            helpingTextScript.AttemptHelp("Press E to pick up the coconut");
        } else if (GetDistance(player.transform.position) > carryDistanceThreshold && GetDistance(player.transform.position) < carryDistanceThreshold + 1f)
        {
            helpingTextScript.NoMore();
        }
    }

    float GetDistance(Vector3 obj)
    {
        return Vector3.Distance(transform.position, obj);
    }
}
