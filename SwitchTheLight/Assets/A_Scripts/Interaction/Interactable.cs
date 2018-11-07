using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    [SerializeField]
    protected float delay;

    [SerializeField]
    protected float radius = 3f;

    [SerializeField]
    protected string ShowInteractionText;

    [SerializeField]
    protected Text InteractionText;

    [SerializeField]
    protected GameObject InteractionPanel;

    [SerializeField]
    protected Text Interact;

    protected GameObject PC;

    private bool Interacting = false;
    private static bool InRangeOfSomeone = false;

    private List<GameObject> InteractableObjects = new List<GameObject>();


    protected void Awake()
    {
        print("I am running on " + gameObject.name);
        InteractionText = GameObject.FindGameObjectWithTag("Interactable").GetComponent<Text>();
        PC = FindObjectOfType<PlayerController>().gameObject;
        InteractionPanel = GameObject.FindGameObjectWithTag("InteractionPanel");
        Interact = GameObject.FindGameObjectWithTag("Interact").GetComponent<Text>();

        InteractableObjects.Add(gameObject);
    }

    protected virtual void Start()
    {
        InteractionPanel.SetActive(false);

        Interact.text = gameObject.name + "(E)";
        Interact.enabled = false;
    }

    protected virtual void FixedUpdate()
    {
        GetInRadius();
    }

    virtual protected void DoSomething()
    {
        //Here goes what you do when interacting with the object
    }

    virtual protected void StepAway()
    {
        InteractionPanel.SetActive(false);
        Interact.enabled = false;
    }

    protected virtual void InRadius()
    {
        //Here goes the text and the buttons that will be shown when getting close to an interactable
        InteractionPanel.SetActive(true);

        StartCoroutine(ShowText(ShowInteractionText, InteractionText));
    }


    virtual protected void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, radius);
    }


    protected virtual void GetInRadius()
    {
        /*
         * If we ARE in range we need to display the interaction panel.
         * If we ARE NOT in range we need to hide the interaction panel.
         * If we ARE in range, we should flag a boolean so that all other objects know we are in range, thus they do not hide the interaction panel.
         * We could do this by doing a range check and flagging the boolean
         */



        float DistanceFromInteractable = Vector3.Distance(PC.transform.position, gameObject.transform.position);
        
        foreach (GameObject Object in InteractableObjects)
        {
            float DistanceFromObject = Vector3.Distance(PC.transform.position, Object.transform.position);

            if (DistanceFromObject <= radius)
            {
                print("In range of " + Object.name);
                InRangeOfSomeone = true;
            }
            else
            {
                print("Not in range of " + Object.name);
                InRangeOfSomeone = false;
            }
        }

        if (DistanceFromInteractable <= radius)
        {
            if (!Interacting)
            {
                Interact.enabled = true;
            }
            else
            {
                Interact.enabled = false;
            }

            if (Input.GetKeyUp(KeyCode.E))
            {
                Interacting = true;
                InRadius();
            }

        }
        else if(DistanceFromInteractable > radius && !InRangeOfSomeone)
        {
            Interacting = false;
            Interact.enabled = false;
            InteractionPanel.SetActive(false);
        }
    }

    protected virtual IEnumerator ShowText(string YourText, Text textPanel)
    {


        for (int i = 0; i < YourText.Length + 1; i++)
        {
           YourText = YourText.Substring(0, i);
            textPanel.text = YourText;

            yield return new WaitForSeconds(delay);
        }

    }


}
