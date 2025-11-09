using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    [SerializeField] private Transform playerPosition;

    //Dampens the y offset for when the player jumps
    [SerializeField] private bool yMovement = false;
    [SerializeField] private float yOffsetMultiplier = 10f;

    //list of the background layers 
    public List<ParallaxLayer> parallaxLayers = new List<ParallaxLayer>();
    
    //list of the parallax speeds
    public List<float> parallaxSpeeds = new List<float>();


    // Update is called once per frame
    void Update()
    {
        UpdateParallax();
    }

    //Updates the material offsets of each of the parralax layers, based on the player position. 
    //The offset change is based on the parallax speed list
    void UpdateParallax()
    {
        for (int i = 0; i < parallaxLayers.Count; i++)
        {
            float parallaxSpeed = parallaxSpeeds[i];
            parallaxLayers[i].offset.x = playerPosition.position.x / parallaxSpeed;
            // parallaxLayers[i].offset.y = playerPosition.position.y / parallaxSpeed / yOffsetMultiplier;
            parallaxLayers[i].offset.y = yMovement ? playerPosition.position.y / parallaxSpeed / yOffsetMultiplier : parallaxLayers[i].offset.y;

            parallaxLayers[i].parrallaxMaterial.mainTextureOffset = parallaxLayers[i].offset;
        }
    }
}
