using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBeh : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        var spider = GameObject.FindGameObjectWithTag("Spider");
        if (spider != null)
        {
            var spiderPosition = spider.transform.position;
            var zombiePosition = transform.position;
            var distance = Vector3.Distance(spiderPosition, zombiePosition);
            if (distance < 5)
            {
                PlaySound();
            }
        }
    }

    private void PlaySound()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
