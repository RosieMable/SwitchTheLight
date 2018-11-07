using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour
{
    private CharacterController PC;
    private AudioSource audioSource;
    private PlayerController player;
    [SerializeField]
    private AudioClip walk;

	// Use this for initialization
	void Start ()
    {        
        player = GetComponent<PlayerController>();
        audioSource = GetComponent<AudioSource>();
	}

    private void FixedUpdate()
    {
        PlayWalkSound();
    }

    void PlayWalkSound()
    {
        if (player.iswalking == false)
        {
            audioSource.clip = walk;
            audioSource.Play();
        }
    }
}
