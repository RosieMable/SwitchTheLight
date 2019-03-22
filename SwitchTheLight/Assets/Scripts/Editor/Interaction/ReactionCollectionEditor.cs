using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEditor;


//This script is similar to ConditionCollection Editor, hence it will not contain
//annotations for components that are used in the same way here
//It will, however, contain explenations of functions unique to this subeditor
[CustomEditor(typeof(ReactionCollection))]
public class ReactionCollectionEditor : EditorWithSubEditors<ReactionEditor, Reaction>
{
    private ReactionCollection reactionCollection;
    private SerializedProperty reactionsProperty;

    private Type[] reactionTypes; //Reference to all the different types of reactions that you can have
    private string[] reactionTypeNames; 
    private int selectedIndex;


    private const float dropAreaHeight = 50f;
    private const float controlSpacing = 5f;
    private const string reactionsPropName = "reactions";


    private readonly float verticalSpacing = EditorGUIUtility.standardVerticalSpacing;


    private void OnEnable ()
    {
        reactionCollection = (ReactionCollection)target; //Caching the target of the editor

        reactionsProperty = serializedObject.FindProperty(reactionsPropName);

        CheckAndCreateSubEditors (reactionCollection.reactions); //Creates the subeditors that are needed

        SetReactionNamesArray ();
    }


    private void OnDisable ()
    {
        CleanupEditors ();
    }


    protected override void SubEditorSetup (ReactionEditor editor)
    {
        editor.reactionsProperty = reactionsProperty;
    }


    public override void OnInspectorGUI ()
    {
        serializedObject.Update ();

        CheckAndCreateSubEditors(reactionCollection.reactions);

        for (int i = 0; i < subEditors.Length; i++)
        {
            subEditors[i].OnInspectorGUI ();
        }

        if (reactionCollection.reactions.Length > 0)
        {
            EditorGUILayout.Space();
            EditorGUILayout.Space ();
        }

        //Creating areas on the inspector that we can use
        Rect fullWidthRect = GUILayoutUtility.GetRect(GUIContent.none, GUIStyle.none, GUILayout.Height(dropAreaHeight + verticalSpacing));

        Rect leftAreaRect = fullWidthRect;
        leftAreaRect.y += verticalSpacing * 0.5f;
        leftAreaRect.width *= 0.5f;
        leftAreaRect.width -= controlSpacing * 0.5f;
        leftAreaRect.height = dropAreaHeight;

        Rect rightAreaRect = leftAreaRect;
        rightAreaRect.x += rightAreaRect.width + controlSpacing;

        TypeSelectionGUI (leftAreaRect);
        DragAndDropAreaGUI (rightAreaRect);

        DraggingAndDropping(rightAreaRect, this); //Drag and drop reactions on the component

        serializedObject.ApplyModifiedProperties ();
    }


    private void TypeSelectionGUI (Rect containingRect)
    {
        Rect topHalf = containingRect;
        topHalf.height *= 0.5f;
        
        Rect bottomHalf = topHalf;
        bottomHalf.y += bottomHalf.height;

        selectedIndex = EditorGUI.Popup(topHalf, selectedIndex, reactionTypeNames);

        if (GUI.Button (bottomHalf, "Add Selected Reaction"))
        {
            Type reactionType = reactionTypes[selectedIndex];
            Reaction newReaction = ReactionEditor.CreateReaction (reactionType);
            reactionsProperty.AddToObjectArray (newReaction);
        }
    }


    private static void DragAndDropAreaGUI (Rect containingRect)
    {
        //Drawing a box on the GUI
        GUIStyle centredStyle = GUI.skin.box;
        centredStyle.alignment = TextAnchor.MiddleCenter;
        centredStyle.normal.textColor = GUI.skin.button.normal.textColor;

        GUI.Box (containingRect, "Drop new Reactions here", centredStyle);
    }

    
    private static void DraggingAndDropping (Rect dropArea, ReactionCollectionEditor editor)
    {
        Event currentEvent = Event.current; //Caching the current event (what the mouse/keyboard is currently doing)

        if (!dropArea.Contains (currentEvent.mousePosition))
            return; //If the box doesn't contain any mouse interaction, then dont do anything

        switch (currentEvent.type) //Switch case
        {
            case EventType.DragUpdated: //Already clicked on something in the editor and it is now dragging it on the drop area

                //Is the drag valid?
                DragAndDrop.visualMode = IsDragValid () ?
                    DragAndDropVisualMode.Link : DragAndDropVisualMode.Rejected; //If the drag is valid, then visual mode is link, otherwise it is rejected
                currentEvent.Use (); //Use the event

                break;
            case EventType.DragPerform: //The mouse button has been released, and now the component we have released it is trying to do something
                
                DragAndDrop.AcceptDrag(); //Accept the release (it works only if the visual mode is link)
                
                //Loop through all the objects that have been dragged
                for (int i = 0; i < DragAndDrop.objectReferences.Length; i++)
                {
                    //And cast them as a monoscript
                    MonoScript script = DragAndDrop.objectReferences[i] as MonoScript;

                    //Looks for the type of the monoscript class (reaction type, since the drag and drop only accepts reactions)
                    Type reactionType = script.GetClass();

                    Reaction newReaction = ReactionEditor.CreateReaction (reactionType); //Creates a new reaction of that type
                    editor.reactionsProperty.AddToObjectArray (newReaction);
                }

                currentEvent.Use();

                break;
        }
    }

    //Function to determine if the drag is valid
    private static bool IsDragValid ()
    {
        for (int i = 0; i < DragAndDrop.objectReferences.Length; i++)
        {
            if (DragAndDrop.objectReferences[i].GetType () != typeof (MonoScript))
                return false; //If the object is not a monoscript object, then return false
            
            MonoScript script = DragAndDrop.objectReferences[i] as MonoScript;
            Type scriptType = script.GetClass (); //Get the class of the object

            if (!scriptType.IsSubclassOf (typeof(Reaction))) //if it is not a reaction script, then return false
                return false;

            if (scriptType.IsAbstract) //If the script is abstract, then return false
                return false;
        }

        return true;
    }


    private void SetReactionNamesArray ()
    {
        Type reactionType = typeof(Reaction);

        Type[] allTypes = reactionType.Assembly.GetTypes();

        List<Type> reactionSubTypeList = new List<Type>();

        for (int i = 0; i < allTypes.Length; i++)
        {
            if (allTypes[i].IsSubclassOf(reactionType) && !allTypes[i].IsAbstract)
            {
                reactionSubTypeList.Add(allTypes[i]);
            }
        }

        reactionTypes = reactionSubTypeList.ToArray();

        List<string> reactionTypeNameList = new List<string>();

        for (int i = 0; i < reactionTypes.Length; i++)
        {
            reactionTypeNameList.Add(reactionTypes[i].Name);
        }

        reactionTypeNames = reactionTypeNameList.ToArray();
    }
}
