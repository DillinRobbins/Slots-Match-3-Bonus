using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AspectRatioManager : MonoBehaviour
{
    private float targetAspectRatio = 9f / 16f;
    [SerializeField] private List<Camera> cameras = new List<Camera>();

    private Canvas canvas;

    void Start()
    {
        cameras = Camera.allCameras.ToList();
        canvas = GetComponent<Canvas>();
        canvas.worldCamera = cameras[0];

        float currentAspectRatio = (float)Screen.width / Screen.height;
        Debug.Log("Current Aspect Ratio: " + currentAspectRatio);

        float ratioDifference = currentAspectRatio / targetAspectRatio;
        Debug.Log("Ratio Difference: " + ratioDifference);

        if (currentAspectRatio > targetAspectRatio)
        {
            if(ratioDifference - 1 > .1 || ratioDifference - 1 < -.1)
            {
                float normalizedWidth = 1f / ratioDifference;
                float barThickness = (1f - normalizedWidth) / 2f;

                foreach(Camera cam in cameras)
                {
                    cam.rect = new Rect(barThickness, 0f, normalizedWidth, 1f);
                }
                
                Debug.Log("Aspect Ratio Changed.");
            }
        }
    }
}
