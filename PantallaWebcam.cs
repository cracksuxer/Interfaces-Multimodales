using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PantallaWebcam : MonoBehaviour
{
    private WebCamTexture webcamTexture;
    private Renderer tvRenderer;
    private Material tvMaterial;
    private int captureCounter = 1;

    void Start()
    {
        tvRenderer = GetComponent<Renderer>();
        tvMaterial = tvRenderer.material;

        WebCamDevice[] devices = WebCamTexture.devices;
        if (devices.Length == 0)
        {
            Debug.LogError("No cameras found");
        }
        else
        {
            string selectedCameraName = devices[0].name;
            Debug.Log("Selected Camera: " + selectedCameraName);
            webcamTexture = new WebCamTexture(selectedCameraName);
            tvMaterial.mainTexture = webcamTexture;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown("s"))
        {
            if (!webcamTexture.isPlaying)
            {
                webcamTexture.Play();
            }
        }
        if (Input.GetKeyDown("p"))
        {
            if (webcamTexture.isPlaying)
            {
                webcamTexture.Pause();
            }
        }
        if (Input.GetKeyDown("x"))
        {
            if (webcamTexture.isPlaying)
            {
                CaptureFrame();
            }
        }
    }

    void CaptureFrame()
    {
        Texture2D snap = new Texture2D(webcamTexture.width, webcamTexture.height);
        snap.SetPixels(webcamTexture.GetPixels());
        snap.Apply();

        System.IO.File.WriteAllBytes("capturedFrame" + captureCounter.ToString() + ".png", snap.EncodeToPNG());
        captureCounter++;
    }
}