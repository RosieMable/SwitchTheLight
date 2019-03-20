using UnityEngine;

//Attached to a game object

public class ReactionCollection : MonoBehaviour
{
    //It has an array of reactions, these reactions have can be of different types
    //Since all different reactions (text reaction, audio reaction, etc) all inheret
    //from the base class reaction, hence polymorphism
    // Polymorphism => All things of that derive from a base class can be treated as instance of that base class
    public Reaction[] reactions = new Reaction[0];

    //Looks through all the reactions and calls their init function
    private void Start ()
    {
        for (int i = 0; i < reactions.Length; i++)
        {
            //Delayedreaction => function hiding
            DelayedReaction delayedReaction = reactions[i] as DelayedReaction;

            if (delayedReaction)
                delayedReaction.Init ();
            else
                reactions[i].Init ();
        }
    }

    //Looping through all the reactions and it calls all the react function of that reaction
    public void React ()
    {
        for (int i = 0; i < reactions.Length; i++)
        {
            DelayedReaction delayedReaction = reactions[i] as DelayedReaction;

            if(delayedReaction)
                delayedReaction.React (this);
            else
                reactions[i].React (this);
        }
    }
}
