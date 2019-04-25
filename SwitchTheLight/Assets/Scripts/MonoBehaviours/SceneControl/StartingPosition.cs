using System.Collections.Generic;
using UnityEngine;

public class StartingPosition : MonoBehaviour
{
    public string startingPointName;
    private GameObject Player;

    private static List<StartingPosition> allStartingPositions =  new List<StartingPosition> ();


    private void OnEnable ()
    {
        allStartingPositions.Add (this);
    }


    private void OnDisable ()
    {
        allStartingPositions.Remove (this);
    }

    void Awake()
    {
        Player = FindObjectOfType<PlayerMove>().gameObject;
    }

    public static Vector3 FindStartingPosition (string pointName)
    {

        allStartingPositions.Clear();

        for (int i = 0; i < allStartingPositions.Count; i++)
        {
            if (allStartingPositions[i].startingPointName == pointName)
                return allStartingPositions[i].transform.position;
        }

        return Vector3.zero;
    }

}
