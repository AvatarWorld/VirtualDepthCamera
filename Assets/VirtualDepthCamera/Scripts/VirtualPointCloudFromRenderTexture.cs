using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualPointCloudFromRenderTexture : MonoBehaviour {
    [SerializeField]
    RenderTexture colorTexture, depthTexture;
    [SerializeField]
    Shader shader;
    [SerializeField]
    Texture pointTexture;
    Material material;
    
    [SerializeField]
    float pointSize;

    [SerializeField]
    float nearClipPlane, farClipPlane;
    [SerializeField]
    float fov;

    int count;
    Vector2 frustumSize;
    // Use this for initialization
    void Start () {
        material = new Material(shader);
        count = colorTexture.width * colorTexture.height;
        var frustumHeight = 2f * farClipPlane * Mathf.Tan(fov * 0.5f * Mathf.Deg2Rad);
        frustumSize = new Vector2(frustumHeight * ((float)colorTexture.width/ (float)colorTexture.height), frustumHeight);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnRenderObject()
    {
        material.SetTexture("_MainTex", pointTexture);
        material.SetTexture("_ColorTex", colorTexture);
        material.SetTexture("_DepthTex", depthTexture);
        material.SetMatrix("_TRS", Matrix4x4.TRS(this.transform.position, this.transform.rotation, this.transform.localScale));
        material.SetFloat("_PointSize", pointSize);
        material.SetFloat("_NearClipPlane", nearClipPlane);
        material.SetFloat("_FarClipPlane", farClipPlane);
        material.SetVector("_FrustumSize", frustumSize);
        material.SetPass(0);
        Graphics.DrawProcedural(MeshTopology.Points, count);
    }
}
