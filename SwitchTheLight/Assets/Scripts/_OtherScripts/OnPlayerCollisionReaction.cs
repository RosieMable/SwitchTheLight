using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPlayerCollisionReaction : MonoBehaviour
{
    public ReactionCollection reaction;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerMove>())
        {
            reaction.React();
        }
    }


}
