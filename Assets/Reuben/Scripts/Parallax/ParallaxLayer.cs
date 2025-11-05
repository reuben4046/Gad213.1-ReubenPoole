using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class ParallaxLayer : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    public Material parrallaxMaterial;
    public Vector2 offset;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        parrallaxMaterial = meshRenderer.material;
        offset = parrallaxMaterial.mainTextureOffset;
    }
}
