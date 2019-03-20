using UnityEngine;

//It inheret from scriptable object because it will be shown on the IDE 
//the type of reaction that it is (audio, text, etc)
//It is important to be able to manipulate the different type
//of reaction in the inspector, this is because scriptable objects
//allow polymorphic serialization
public abstract class Reaction : ScriptableObject
{
    public void Init ()
    {
        SpecificInit ();
    }


    protected virtual void SpecificInit()
    {}


    public void React (MonoBehaviour monoBehaviour)
    {
        ImmediateReaction ();
    }


    protected abstract void ImmediateReaction ();
}
