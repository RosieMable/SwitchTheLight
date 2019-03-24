using UnityEngine;
using UnityEditor; //It is an editor script

//Abstract cause this is a base class, it cannot be instantieted by itself
//The brachets are the types of the editor -> Type of editor and Type of Target
public abstract class EditorWithSubEditors<TEditor, TTarget> : Editor //angle brakets to say what subeditor is targeting and what that subeditor it is going to modify
    where TEditor : Editor //Needs to be something that inherets from the editor class
    where TTarget : Object
    //Where is a restriction on a generic type
{
    protected TEditor[] subEditors; //Array of the subeditors


    protected void CheckAndCreateSubEditors(TTarget[] subEditorTargets) //Function that decides if there is the need to create subeditors for the targets
    {
        if (subEditors != null && subEditors.Length == subEditorTargets.Length)
            return;

        CleanupEditors();

        subEditors = new TEditor[subEditorTargets.Length]; //creates a new array of subeditors and the amount it is going to be the exact amount of subeditors that we need for the amount of targets

        for (int i = 0; i < subEditors.Length; i++)
        {
            subEditors[i] = CreateEditor(subEditorTargets[i]) as TEditor; //Creates an editor of that type
            SubEditorSetup(subEditors[i]);
        }
    }
    //


    protected void CleanupEditors() //Gets rid off the old editors
    {
        if (subEditors == null)
            return;

        for (int i = 0; i < subEditors.Length; i++)
        {
            DestroyImmediate(subEditors[i]);
        }

        subEditors = null; //set the array to null
    }


    protected abstract void SubEditorSetup(TEditor editor); //This function, being abstract, it needs to be created from the inhereted classes 
    //All the subeditors will have their own setup function
}