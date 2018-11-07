using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDialogueManager : MonoBehaviour {


    [SerializeField]
    protected float delay;

    [SerializeField]
    protected float radius = 3f;


    [SerializeField]
    protected string fullText;

    protected string EmptyText = "";


    [SerializeField]
    protected Text DialogueText;

    // Use this for initialization
    protected virtual void Start() {

        DialogueText = GameObject.FindGameObjectWithTag("DialogueText").GetComponent<Text>();

    }

    protected virtual void CheckRadius(Vector3 PCPosition, Vector3 GameObjectPsition)
    {
        float DistanceFromInteractable = Vector3.Distance(PCPosition, GameObjectPsition);

        if (DistanceFromInteractable <= radius)
        {
            Debug.Log("I am in range");
            DialogueText.gameObject.SetActive(true);
            ActionsInRadius();
        }
        else
        {
            DialogueText.gameObject.SetActive(false);
        }
    }
     

protected virtual void ActionsInRadius()
    {
           
      StartCoroutine(TypeSentence(fullText, DialogueText));
    }

    protected virtual IEnumerator TypeSentence(string sentence, Text textPanel)
    {
        textPanel.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            Debug.Log(sentence.ToCharArray());
            textPanel.text += letter;
            yield return new WaitForSeconds(delay);
        }
    }
}
