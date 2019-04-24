public class SceneReaction : Reaction
{
    public string sceneName;
    public string startingPointInLoadedScene;
    public SaveData playerSaveData;
    public float delay;

    private SceneController sceneController;
    private StartingPosition startPos;

    protected override void SpecificInit()
    {
        sceneController = FindObjectOfType<SceneController> ();
    }


    protected override void ImmediateReaction()
    {
        StartingPosition.FindStartingPosition(startingPointInLoadedScene);
        sceneController.FadeAndLoadSceneWithDelay (this, delay);
    }
}