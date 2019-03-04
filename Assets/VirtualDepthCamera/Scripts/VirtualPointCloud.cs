using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualPointCloud : MonoBehaviour {
    [SerializeField]
    VirtualDepthCamera virtualDepthCamera;
    [SerializeField]
    Shader shader;
    [SerializeField]
    Texture pointTexture;
    Material material;
    int count;
    [SerializeField]
    float pointSize;
	// Use this for initialization
	void Start () {
        material = new Material(shader);
        count = virtualDepthCamera.ResolutionW * virtualDepthCamera.ResolutionH;
        print(virtualDepthCamera.FrustumSize);
    }

#if UNITY_EDITOR
    void Reset()
    {
        if (shader == null)
        {
            shader = Shader.Find("Custom/VirtualPointCloudView");
        }
    }
#endif

    // Update is called once per frame
    void Update () {
		
	}

    void OnRenderObject()
    {
        material.SetTexture("_MainTex", pointTexture);
        material.SetTexture("_ColorTex", virtualDepthCamera.ColorTexture);
        material.SetTexture("_DepthTex", virtualDepthCamera.DepthTexture);
        material.SetMatrix("_TRS", Matrix4x4.TRS(this.transform.position, this.transform.rotation, this.transform.localScale));
        material.SetFloat("_PointSize", pointSize);
        material.SetFloat("_NearClipPlane", virtualDepthCamera.NearClipPlane);
        material.SetFloat("_FarClipPlane", virtualDepthCamera.FarClipPlane);
        material.SetVector("_FrustumSize", virtualDepthCamera.FrustumSize);
        material.SetPass(0);
        Graphics.DrawProcedural(MeshTopology.Points, count);
    }
}
