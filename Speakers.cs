using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speakers : MonoBehaviour
{
    private AudioSource audioSource;
    private bool isRecording = false;
    private string selectedDevice;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        string[] microphones = Microphone.devices;
        if (microphones.Length == 0)
        {
            Debug.LogError("No microphones found");
        }
        else
        {
            selectedDevice = microphones[0];
            Debug.Log("Selected Microphone: " + selectedDevice);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isRecording)
        {
            Debug.Log("Recording...");
            StartRecording();
        }

        if (Input.GetKeyDown(KeyCode.T) && isRecording)
        {
            Debug.Log("Stopped recording...");
            StopRecording();
        }
    }

    void StartRecording()
    {
        audioSource.clip = Microphone.Start(selectedDevice, true, 10, 44100);
        audioSource.loop = true;
        while (!(Microphone.GetPosition(selectedDevice) > 0)) { }
        audioSource.Play();
        isRecording = true;
    }

    void StopRecording()
    {
        Microphone.End(selectedDevice);
        audioSource.Stop();
        isRecording = false;
    }
}