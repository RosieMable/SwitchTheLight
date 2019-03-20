using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ConditionCollection))] //Targets the type of object that this custom editor it is going to be used for
public class ConditionCollectionEditor : EditorWithSubEditors<ConditionEditor, Condition>
{
    public SerializedProperty collectionsProperty; //It represents the condition collection array


    private SerializedProperty descriptionProperty; //These represents the fields of the condition collections scriptable object
    private SerializedProperty conditionsProperty;
    private SerializedProperty reactionCollectionProperty;
    private ConditionCollection conditionCollection; //reference the condition collection object that this editor is targeting


    //Constants that have the same name as the condition collection scriptable objects fields and values for the size of buttons
    private const float conditionButtonWidth = 30f;
    private const float collectionButtonWidth = 125f;
    private const string conditionCollectionPropDescriptionName = "description";
    private const string conditionCollectionPropRequiredConditionsName = "requiredConditions";
    private const string conditionCollectionPropReactionCollectionName = "ReactionCollection";


    private void OnEnable ()
    {
        if (target == null) //if the target of this editor doesn't exist, then destroy yourself
        {
            DestroyImmediate (this);
            return;
        }

        descriptionProperty = serializedObject.FindProperty(conditionCollectionPropDescriptionName);
        conditionsProperty = serializedObject.FindProperty(conditionCollectionPropRequiredConditionsName);
        reactionCollectionProperty = serializedObject.FindProperty(conditionCollectionPropReactionCollectionName);

        conditionCollection = (ConditionCollection)target; //casting the type of target that the editor it is going to use and stores it

        CheckAndCreateSubEditors(conditionCollection.requiredConditions); //Looks at the condition collection and if it has to create sub editors (for the conditions) then it creates them
    }


    private void OnDisable ()
    {
        CleanupEditors (); //When the condition collection related to this subeditor is destroyed, then the subeditor needs to be destroyed as well
    }


    protected override void SubEditorSetup (ConditionEditor editor) //Making sure that the condition editor it is using the correct values
    {
        editor.editorType = ConditionEditor.EditorType.ConditionCollection;
        editor.conditionsProperty = conditionsProperty;
    }


    public override void OnInspectorGUI () //It is called every frame, it updates the GUI in the IDE
    {
        serializedObject.Update();

        CheckAndCreateSubEditors(conditionCollection.requiredConditions);

        EditorGUILayout.BeginVertical(GUI.skin.box);
        EditorGUI.indentLevel++;

        EditorGUILayout.BeginHorizontal();

        descriptionProperty.isExpanded = EditorGUILayout.Foldout(descriptionProperty.isExpanded, descriptionProperty.stringValue);

        if (GUILayout.Button("Remove Collection", GUILayout.Width(collectionButtonWidth))) //Creates a button to remove an object from the array of condition collections
        {
            //Extension Method, adds to a class that has already been compiled
            collectionsProperty.RemoveFromObjectArray(conditionCollection); //It removes the target of this subeditor
        }

        EditorGUILayout.EndHorizontal();

        if (descriptionProperty.isExpanded)
        {
            ExpandedGUI();
        }

        EditorGUI.indentLevel--;
        EditorGUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
    }


    private void ExpandedGUI ()
    {
        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(descriptionProperty);

        EditorGUILayout.Space();

        float space = EditorGUIUtility.currentViewWidth / 3f;

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Condition", GUILayout.Width(space));
        EditorGUILayout.LabelField("Satisfied?", GUILayout.Width(space));
        EditorGUILayout.LabelField("Add/Remove", GUILayout.Width(space));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginVertical(GUI.skin.box);
        for (int i = 0; i < subEditors.Length; i++)
        {
            subEditors[i].OnInspectorGUI();
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace ();
        if (GUILayout.Button("+", GUILayout.Width(conditionButtonWidth)))
        {
            Condition newCondition = ConditionEditor.CreateCondition();
            conditionsProperty.AddToObjectArray(newCondition);
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(reactionCollectionProperty);
    }


    public static ConditionCollection CreateConditionCollection() //creates a condition collection within the editor
    {
        ConditionCollection newConditionCollection = CreateInstance<ConditionCollection>();

        newConditionCollection.description = "New ConditionCollection";
        newConditionCollection.requiredConditions = new Condition[1];
        newConditionCollection.requiredConditions[0] = ConditionEditor.CreateCondition();

        return newConditionCollection;
    }
}
