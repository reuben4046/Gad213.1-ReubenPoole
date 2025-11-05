using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    [SerializeField] private Transform playerPosition;

    [SerializeField] private float yOffsetMultiplier = .9f;
    public List<ParallaxLayer> parallaxLayers = new List<ParallaxLayer>();
    public List<float> parallaxSpeeds = new List<float>();


    // Update is called once per frame
    void Update()
    {
        UpdateParallax();
    }

    void UpdateParallax()
    {
        for (int i = 0; i < parallaxLayers.Count; i++)
        {
            float parallaxSpeed = parallaxSpeeds[i];
            parallaxLayers[i].offset.x = playerPosition.position.x / parallaxSpeed;
            parallaxLayers[i].offset.y = playerPosition.position.y / parallaxSpeed / yOffsetMultiplier;
            parallaxLayers[i].parrallaxMaterial.mainTextureOffset = parallaxLayers[i].offset;
        }
    }
}
