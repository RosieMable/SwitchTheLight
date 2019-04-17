public class SceneReaction : Reaction
{
    public string sceneName;
    public string startingPointInLoadedScene;
    public SaveData playerSaveData;
    public float delay;

    private SceneController sceneController;


    protected override void SpecificInit()
    {
        sceneController = FindObjectOfType<SceneController> ();
    }


    protected override void ImmediateReaction()
    {
        sceneController.FadeAndLoadSceneWithDelay (this, delay);
    }
}