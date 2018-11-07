using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : UIGameManager {

    private GameObject PC;

    protected override void Start()
    {
        base.Start();
        PC = FindObjectOfType<PlayerController>().gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == PC)
        {
            if (fullText != "")
            {
                StartCoroutine(ShowText(fullText));
            }
            else
            {
                fullText = "There is no text, you fuck";
                StartCoroutine(ShowText(fullText));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == PC)
        {
            fullText = "  ";
            StartCoroutine(ShowText(fullText));
        }
    }
}
