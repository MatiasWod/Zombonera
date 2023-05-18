using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private float music_volume = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        // Get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();

        // Set the volume of the AudioSource
        audioSource.volume = music_volume;

        // Play the AudioSource
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
