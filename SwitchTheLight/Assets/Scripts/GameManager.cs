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

    private GameObject Player;

    private StartingPosition StartingPos;

    private void OnEnable()
    {
        SceneController.AfterSceneLoad += CheckCameraPostProcessing;
        SceneController.AfterSceneLoad += FindStartingPosForPlayer;
    }

    private void Awake()
    {
        SceneController = FindObjectOfType<SceneController>();

        if (Instance != this)
        {
            Instance = this;
        }

        Player = FindObjectOfType<PlayerMove>().gameObject;
        StartingPos = FindObjectOfType<StartingPosition>();
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
                Player.GetComponent<PlayerMove>().movementSpeed = 2;
                break;

            case 2:
                Camera.main.GetComponent<PostProcessingBehaviour>().profile = CameraProfiles[1];
                Player.GetComponent<PlayerMove>().movementSpeed = 4;
                break;
        }
    }

    void FindStartingPosForPlayer()
    {
        Transform newPCPos = StartingPosition.FindStartingPosition(StartingPos.startingPointName);
        print(newPCPos.gameObject.name);

        Player.transform.position = newPCPos.position;
    }

    private void OnDisable()
    {
        SceneController.AfterSceneLoad -= CheckCameraPostProcessing;
        SceneController.AfterSceneLoad -= FindStartingPosForPlayer;

    }
}
