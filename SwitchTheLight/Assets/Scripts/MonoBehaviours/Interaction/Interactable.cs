using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Transform interactionLocation;
    public ConditionCollection[] conditionCollections = new ConditionCollection[0];
    public ReactionCollection defaultReactionCollection;


    GameObject PCPos;

    private void Start()
    {
        PCPos = GameObject.FindGameObjectWithTag("Player");

    }

    public void Interact ()
    {
        Debug.Log("LALALALA");

        for (int i = 0; i < conditionCollections.Length; i++)
        {
            if (conditionCollections[i].CheckAndReact ())
                return;
        }

        defaultReactionCollection.React ();
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, PCPos.transform.position);
        print(distance);

        if (distance <= 20)
        {
            if (Input.GetKey(KeyCode.E))
            {
                print("MEOW");
                Interact();
            }
        }

    }
}
