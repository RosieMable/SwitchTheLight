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

    [SerializeField]
    private Transform[] startingPoints;

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

        Player = FindObjectOfType<PlayerMove>().gameObject;
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
            case 1: //DarkScene
                Camera.main.GetComponent<PostProcessingBehaviour>().profile = CameraProfiles[0];
                Player.GetComponent<PlayerMove>().movementSpeed = 2;
                Player.gameObject.transform.position = startingPoints[1].position;

                break;

            case 2: //Memory1
                Camera.main.GetComponent<PostProcessingBehaviour>().profile = CameraProfiles[1];
                Player.GetComponent<PlayerMove>().movementSpeed = 4;
                Player.gameObject.transform.position = startingPoints[0].position;

                break;

            case 3: //AM1
                Camera.main.GetComponent<PostProcessingBehaviour>().profile = CameraProfiles[0];
                Player.GetComponent<PlayerMove>().movementSpeed = 2;
                Player.gameObject.transform.position = startingPoints[1].position;

                break;

            case 4: //M2
                Camera.main.GetComponent<PostProcessingBehaviour>().profile = CameraProfiles[2];
                Player.GetComponent<PlayerMove>().movementSpeed = 6;
                Player.gameObject.transform.position = startingPoints[2].position;

                break;

            case 5: //AM2
                Camera.main.GetComponent<PostProcessingBehaviour>().profile = CameraProfiles[0];
                Player.GetComponent<PlayerMove>().movementSpeed = 2;
                Player.gameObject.transform.position = startingPoints[1].position;
                break;



        }
    }

    private void OnDisable()
    {
        SceneController.AfterSceneLoad -= CheckCameraPostProcessing;

    }
}
