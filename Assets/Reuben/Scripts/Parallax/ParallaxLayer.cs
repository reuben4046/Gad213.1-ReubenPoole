using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class ParallaxLayer : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    public Material parrallaxMaterial;
    public Vector2 offset;

    //getting references for the mesh renderer, material and the materials texture offset.
    // these references are then used by the parallax controller  
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        parrallaxMaterial = meshRenderer.material;
        offset = parrallaxMaterial.mainTextureOffset;
    }
}
