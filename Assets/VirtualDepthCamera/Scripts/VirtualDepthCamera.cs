using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualDepthCamera : MonoBehaviour
{
    [SerializeField]
    Camera colorCamera, depthCamera;
    [SerializeField]
    float fov;

    [SerializeField]
    int resolutionW, resolutionH;
    public int ResolutionW
    {
        get
        {
            return resolutionW;
        }
    }

    public int ResolutionH
    {
        get
        {
            return resolutionH;
        }
    }

    private RenderTexture colorTexture, depthTexture;
    public RenderTexture ColorTexture
    {
        get
        {
            return colorTexture;
        }
    }

    public RenderTexture DepthTexture
    {
        get
        {
            return depthTexture;
        }
    }

    public Vector2 FrustumSize
    {
        get;
        private set;
    }

    [SerializeField]
    float nearClipPlane, farClipPlane;

    public float NearClipPlane
    {
        get
        {
            return nearClipPlane;
        }
    }

    public float FarClipPlane
    {
        get
        {
            return farClipPlane;
        }
    }




    private void Awake()
    {
        Init();
    }

    void Init()
    {
        {
            colorCamera.nearClipPlane = nearClipPlane;
            colorCamera.farClipPlane = farClipPlane;
            colorTexture = new RenderTexture(resolutionW, resolutionH, 0);
            colorCamera.targetTexture = colorTexture;
            colorCamera.fieldOfView = fov;
        }
        {
            depthCamera.depthTextureMode |= DepthTextureMode.Depth;
            depthCamera.nearClipPlane = nearClipPlane;
            depthCamera.farClipPlane = farClipPlane;
            depthTexture = new RenderTexture(resolutionW, resolutionH, 24, RenderTextureFormat.ARGBFloat);
            depthCamera.targetTexture = depthTexture;
            depthCamera.fieldOfView = fov;
        }

        var frustumHeight = 2f * farClipPlane * Mathf.Tan(fov * 0.5f * Mathf.Deg2Rad);
        FrustumSize = new Vector2(frustumHeight * colorCamera.aspect, frustumHeight);
    }
}
