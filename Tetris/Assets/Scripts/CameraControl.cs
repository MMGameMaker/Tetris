using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private DeviceOrientation currentOreientation;

    private Camera mainCam;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = GetComponent<Camera>();

        GameManager.Instance.DeviceOrientationChangeHandler += ChangeCamerasizeOnDeviceOrientation;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void ChangeCamerasizeOnDeviceOrientation(DeviceOrientation deviceOrientation)
    {
        switch (deviceOrientation)
        {
            case DeviceOrientation.Unknown:
            case DeviceOrientation.FaceUp:
            case DeviceOrientation.FaceDown:
                break;
            case DeviceOrientation.Portrait:
                mainCam.orthographicSize = 5;
                break;
            case DeviceOrientation.PortraitUpsideDown:
                mainCam.orthographicSize = 5;
                break;
            case DeviceOrientation.LandscapeLeft:
                mainCam.orthographicSize = 3;
                break;
            case DeviceOrientation.LandscapeRight:
                mainCam.orthographicSize = 3;
                break;
        }
    }


}
