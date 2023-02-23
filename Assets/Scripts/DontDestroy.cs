using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    AudioSource audioSource;
    float volumeStart = 0.5f;
    public float Volume => volumeStart;

    void Awake()
    {
        AudioSource[] gameObjects = FindObjectsOfType<AudioSource>();
        if (gameObjects.Length > 1)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        ChangeBgmVolume(volumeStart);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeBgmVolume(float volume)
    {
        volumeStart = volume;
        audioSource.volume = volume;
    }
}
