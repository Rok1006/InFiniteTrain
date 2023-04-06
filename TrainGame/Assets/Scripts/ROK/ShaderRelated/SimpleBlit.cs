using System.Collections;
using UnityEngine;
 
[ExecuteInEditMode]
public class SimpleBlit: MonoBehaviour
{
    [SerializeField]
    private Material material;
    
    public void Start()
    {
        Camera.main.depthTextureMode = DepthTextureMode.DepthNormals;// depthnormals are needed for outline effect
    }
 
    // Postprocess the image
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {      
        Graphics.Blit(source, destination, material);
    }
}
