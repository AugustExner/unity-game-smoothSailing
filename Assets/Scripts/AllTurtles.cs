using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AllTurtles : MonoBehaviour
{
    public List<GameObject> AllObjects;

    // Start is called before the first frame update
    void Start()
    {
        AllObjects = GameObject.FindGameObjectsWithTag("Turtle").ToList();
        
    }

    public List<GameObject> GetTurtlesList()
    {
        return AllObjects;
    }

    public void TurtleDead(GameObject obj)
    {
        AllObjects.Remove(obj);
    }
}
