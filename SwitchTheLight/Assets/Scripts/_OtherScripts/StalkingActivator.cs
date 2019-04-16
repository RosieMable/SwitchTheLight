using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalkingActivator : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerMove>())
        {
            Stalking.isStalking = true;
            this.gameObject.SetActive(false);
        }
    }

}
