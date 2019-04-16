using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowards : MonoBehaviour
{

    [SerializeField]
    float speed = 2;

    [SerializeField]
    Transform target; // drag the player here float speed = 5.0f; // move speed


    public ReactionCollection reaction;

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            print("true");
            reaction.React();
        }
    }
}
