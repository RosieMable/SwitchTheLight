using System.Collections;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Transform interactionLocation;
    public ConditionCollection[] conditionCollections = new ConditionCollection[0];
    public ReactionCollection defaultReactionCollection;
    public bool isCooldown = false;
    private int interaction;


    public void Interact ()
    {
        for (interaction = 0; interaction < 1; interaction++)
        {
            if (isCooldown == false)
            {
                for (int i = 0; i < conditionCollections.Length; i++)
                {
                    if (conditionCollections[i].CheckAndReact())
                        return;
                }
                defaultReactionCollection.React();
                interaction++;
                StartCoroutine(CoolDown(3));
            }
            else
            {
                continue;
            }
        }

    }


    IEnumerator CoolDown(float timer)
    {
        isCooldown = true;
        print("cooldown...");

        yield return new WaitForSeconds(timer);

        isCooldown = false;
        interaction = 0;
        print("Ready To use");

        yield return null;
    }

  

}
