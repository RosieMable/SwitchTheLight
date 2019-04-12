using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowards : MonoBehaviour
{

    [SerializeField]
    float speed = 2;

    [SerializeField]
    Transform target; // drag the player here float speed = 5.0f; // move speed

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }
}
