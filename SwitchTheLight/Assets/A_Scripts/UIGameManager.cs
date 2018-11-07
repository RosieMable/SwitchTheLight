﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameManager : MonoBehaviour {


    [SerializeField]
    protected float delay;


    [SerializeField]
    protected string fullText;


    [SerializeField]
    protected Text DialogueText;

	// Use this for initialization
	protected virtual void Start () {

        DialogueText = GameObject.FindGameObjectWithTag("DialogueText").GetComponent<Text>();
		
	}
	
   protected IEnumerator ShowText(string currentText) {

            for (int i = 0; i < fullText.Length + 1; i++)
            {
                currentText = fullText.Substring(0, i);
                DialogueText.text = currentText;
                yield return new WaitForSeconds(delay);

            }
}
}
