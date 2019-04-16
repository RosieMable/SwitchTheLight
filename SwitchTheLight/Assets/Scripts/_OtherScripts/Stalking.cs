using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalking : MonoBehaviour
{

    [SerializeField]
    private GameObject target;

    public static bool isStalking = false;

    private void Start()
    {
        target = FindObjectOfType<PlayerMove>().gameObject;
    }

    private void Update()
    { 
        if (isStalking)
        {
            StalkTarget();
        }
    }

    void StalkTarget()
    {
        transform.LookAt(target.transform);
    }

}
