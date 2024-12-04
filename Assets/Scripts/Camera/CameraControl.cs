using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControl : MonoBehaviour
{
    [Header("控制旋转")]
    public float sensitivityHor = 5.0f;
    public float sensitivityVert = 5.0f;

    private float rotationX = 0.0f;
    private float rotationY = 0.0f;

    public float minimunVert = -45f;
    public float maximunVert = 45f;

    //右键旋转视角
    public bool canControl = true;

    [Header("控制缩放")]
    public float fovChangeSpeed = 1f;
    public float minZoomSize = 10f;
    public float maxZoomSize = 100f;
    public CinemachineVirtualCamera vcam;

    // 中键缩放视角
    public bool canShift = true;
    public GameObject player;

    // 摄像机缩放控制
    public float minCameraDistance = 1f;
    public float maxCameraDistance = 4f;
    private CinemachineFramingTransposer framingTransposer;

    // Start is called before the first frame update
    void Start()
    {
        // 获取Cinemachine的FramingTransposer
        if (vcam != null)
        {
            framingTransposer = vcam.GetCinemachineComponent<CinemachineFramingTransposer>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        CameraRot();
        CameraZoom();
    }

    private void CameraRot()
    {
        if (canControl)
        {
            if (Input.GetMouseButton(1))
            {
                rotationY = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityHor;
                player.transform.localEulerAngles = new Vector3(player.transform.rotation.x, rotationY, 0);
                rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
                rotationX = Mathf.Clamp(rotationX, minimunVert, maximunVert);
                transform.localEulerAngles = new Vector3(rotationX, rotationY, 0.0f);
            }
        }
    }

    private void CameraZoom()
    {
        if (canShift)
        {
            if (framingTransposer != null)
            {
                float scrollInput = Input.GetAxis("Mouse ScrollWheel");
                if (scrollInput != 0)
                {
                    float currentDistance = framingTransposer.m_CameraDistance;
                    currentDistance -= scrollInput * fovChangeSpeed;
                    currentDistance = Mathf.Clamp(currentDistance, minCameraDistance, maxCameraDistance);

                    // CameraDistance
                    framingTransposer.m_CameraDistance = currentDistance;
                }
            }
        }
    }
}
