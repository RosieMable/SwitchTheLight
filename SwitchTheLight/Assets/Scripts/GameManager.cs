using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.PostProcessing;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    PostProcessingProfile[] CameraProfiles;

    public static GameManager Instance;

    private SceneController SceneController;

    private void OnEnable()
    {
        SceneController.AfterSceneLoad += CheckCameraPostProcessing;
    }

    private void Awake()
    {
        SceneController = FindObjectOfType<SceneController>();

        if (Instance != this)
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void CheckCameraPostProcessing()
    {
        int y = SceneManager.GetActiveScene().buildIndex;

        switch (y)
        {
            case 1:
                Camera.main.GetComponent<PostProcessingBehaviour>().profile = CameraProfiles[0];
                break;

            case 2:
                Camera.main.GetComponent<PostProcessingBehaviour>().profile = CameraProfiles[1];
                break;
        }
    }

    private void OnDisable()
    {
        SceneController.AfterSceneLoad -= CheckCameraPostProcessing;
    }
}
