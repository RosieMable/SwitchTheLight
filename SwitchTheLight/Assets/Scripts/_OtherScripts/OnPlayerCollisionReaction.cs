using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPlayerCollisionReaction : MonoBehaviour
{
    public ReactionCollection reaction;

    private MoveTowards WallParent;



    void Start()
    {
        WallParent = GetComponentInParent<MoveTowards>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerMove>())
        {
            reaction.React();
        }
    }


   private void OnTriggerEnter(Collider collider)
    {
        if (WallParent.enabled)
        {
            if (collider.gameObject.GetComponent<PlayerMove>())
            {
                reaction.React();
                print("Reaction time!");
            }
        }
        else
        {
            if (collider.gameObject.GetComponent<PlayerMove>())
            {
               Physics.IgnoreCollision(this.GetComponent<Collider>(), collider);
            }
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        if (WallParent.enabled)
        {
            if (collider.gameObject.GetComponent<PlayerMove>())
            {
                reaction.React();
                print("Reaction time!");
            }
        }
        else
        {
            if (collider.gameObject.GetComponent<PlayerMove>())
            {
                Physics.IgnoreCollision(this.GetComponent<Collider>(), collider);
            }
        }
    }


}
