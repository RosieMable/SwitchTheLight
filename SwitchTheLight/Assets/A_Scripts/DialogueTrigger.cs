using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : UIDialogueManager {


    protected GameObject PC;

    protected override void Start()
    {
        base.Start();
        PC = FindObjectOfType<PlayerController>().gameObject;
    }

    protected void FixedUpdate()
    {
        CheckRadius(PC.gameObject.transform.position, this.transform.position);
    }
}
