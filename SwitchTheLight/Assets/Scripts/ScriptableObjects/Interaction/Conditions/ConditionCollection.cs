using UnityEngine;

public class ConditionCollection : ScriptableObject
{
    public string description;
    public Condition[] requiredConditions = new Condition[0];
    public ReactionCollection ReactionCollection;
    public bool CheckAndReact()
    {
        for (int i = 0; i < requiredConditions.Length; i++)
        {
            if (! AllConditions.CheckCondition(requiredConditions[i])) //Checks if all the conditions are met, if they are not then it returns false
            {
                return false;
            }
        }

        if (ReactionCollection)
        {
            ReactionCollection.React(); //if the conditions have been met and there are some reactions, then call them
        }

        return true;
    }
}