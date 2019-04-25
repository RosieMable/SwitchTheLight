using System.Collections.Generic;
using UnityEngine;

public class StartingPosition : MonoBehaviour
{
    public string startingPointName;
    private GameObject Player;

    [SerializeField]
    public static List<StartingPosition> allStartingPositions =  new List<StartingPosition> ();


    private void OnEnable ()
    {
        allStartingPositions.Add (this);
        
    }


    //private void OnDisable ()
    //{
    //    OnChangeScene();
    //}

    void Awake()
    {
        Player = FindObjectOfType<PlayerMove>().gameObject;

    }

    public static Transform FindStartingPosition (string pointName)
    {
        foreach (var item in allStartingPositions)
        {
            print(item.gameObject.name);
        }

        for (int i = 0; i < allStartingPositions.Count; i++)
        {
            if (allStartingPositions[i].startingPointName == pointName)
                return allStartingPositions[i].transform;

            if (allStartingPositions[i].transform != null)
            {

                FindObjectOfType<PlayerMove>().transform.position = allStartingPositions[i].transform.position;
            }
        }

        return null;
    }

    public void OnChangeScene()
    {
        allStartingPositions.Remove(this);
    }

}
