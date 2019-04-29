using System.Collections;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public ConditionCollection[] conditionCollections = new ConditionCollection[0]; //Public reference to the condition collection (where all conditions for the interactable to do something are put)
    public ReactionCollection defaultReactionCollection; //Default reaction, this will happen if there are no condition collections
    private bool isCooldown = false; //to check if the interactable is on cooldown
    private int interaction;


    public void Interact () //Public method that is called by the field of view script
    {
        for (interaction = 0; interaction < 1; interaction++) 
        {
            if (isCooldown == false) 
            {
                for (int i = 0; i < conditionCollections.Length; i++) //Goes through the conditions
                {
                    if (conditionCollections[i].CheckAndReact()) //If they are satisfied, then there is the according reaction
                        return;
                }
                defaultReactionCollection.React(); // otherwise there is the default reaction
                interaction++;
                StartCoroutine(CoolDown(3)); //starts the cooldown
            }
            else
            {
                continue;
            }
        }

    }


    IEnumerator CoolDown(float timer) //Cool down coroutine to avoid too many interactions per second
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
