using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; //Used because we are dealing with scenes data

public class SceneController : MonoBehaviour
{
    public event Action BeforeSceneUnload; //Action is a type of delegate of C#, it doesn't return any type and it is basically a variable that stores a function
    public event Action AfterSceneLoad; //Other classes will subscribe to these actions, which will be called in this script 
    //If there are any listeners subscribed, they will act when these functions are called within this script


    public CanvasGroup faderCanvasGroup; //used canvas group to directly affect the alpha channel of the image to have the fade effect
    //It is also used because during the fading the player shouldn't be able to do anything and the canvas group allows to block every raycast
    public float fadeDuration = 1f; //Fade duration - how long it takes to go from transparent to black and viceversa
    public string startingSceneName = ""; //Reference to the starting scene, so it is certain that the system loads the correct one
    public string initialStartingPositionName = ""; 
    public SaveData playerSaveData;
    
    
    private bool isFading; //Flag to know if the screen is fading or not, if it is fading then the player and the system are not allowed to do anything or start any new coroutines


    private IEnumerator Start ()
    {
        faderCanvasGroup.alpha = 1f;

        yield return StartCoroutine (LoadSceneAndSetActive (startingSceneName));

        StartCoroutine (Fade (0f));
    }


    public void FadeAndLoadScene (SceneReaction sceneReaction)
    {
        if (!isFading)
        {
            StartCoroutine (FadeAndSwitchScenes (sceneReaction.sceneName));
        }
    }


    //Function that initiate the fading process, it unloads the current scene while loading the new one and switching to it.
    private IEnumerator FadeAndSwitchScenes (string sceneName)
    {
        yield return StartCoroutine (Fade (1f));

        if (BeforeSceneUnload != null)
            BeforeSceneUnload ();

        yield return SceneManager.UnloadSceneAsync (SceneManager.GetActiveScene ().buildIndex);

        yield return StartCoroutine (LoadSceneAndSetActive (sceneName));

        if (AfterSceneLoad != null)
            AfterSceneLoad ();  

        yield return StartCoroutine(Fade(0f));
    }


    //Coroutine that loads a specific scene (decided by the string sceneName) and sets it to active
    private IEnumerator LoadSceneAndSetActive (string sceneName)
    {
        //Loads the specific scene asynchronously, which means it happens in the background and does not create stutter to the game flow
        //The scene it is loaded as additive, because it is going to be added and set active in the persistent scene
        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        //Set active the most recently loaded scene, so there is the need to find out which one is the scene that has been loaded more recently
        Scene newlyLoadedScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(newlyLoadedScene); //Sets the scene to active

    }


    private IEnumerator Fade (float finalAlpha) //Coroutine to fade both in and out
    {
        //Nothing should happen whilst the screen is fading
        isFading = true; //As the coroutine starts, the system knows the scene is fading
        faderCanvasGroup.blocksRaycasts = true; //Whilst fading, the player is not allowed to do anything, thus all raycast tracking is stopped

        //It works the difference between the current alpha level and the alpha level that is going to be reached
        //Then it finds the absolute value of it (number with no sign in front of it)
        //Divides that by the fadeDuration (think of Speed = Distance/Time)
        float fadeSpeed = Mathf.Abs(faderCanvasGroup.alpha - finalAlpha)/ fadeDuration;

        //The current alpha is not approximately equal to the final Alpha, keep doing this while loop
        while (!Mathf.Approximately(faderCanvasGroup.alpha, finalAlpha))
        {
            faderCanvasGroup.alpha = Mathf.MoveTowards(faderCanvasGroup.alpha, finalAlpha, fadeSpeed * Time.deltaTime);
            yield return null; //It exits the function and repeats the while loop until the condition is no longer met
            //This is done in order to avoid the movetowards function to be repeated constantly, but happen over time

        }

        //Once the fading finishes
        isFading = false;
        faderCanvasGroup.blocksRaycasts = false;
    }
}
