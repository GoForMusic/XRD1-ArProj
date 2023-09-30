using UnityEngine;

public class WallTransparency : MonoBehaviour
{
    public Transform cameraTransform; // AR Camera
    public Transform wallTransform; // Reference to the Wall
    public float transparencyDistance = 1.0f; // Distance at which the wall becomes transparent

    private Renderer wallRenderer; // Renderer component of the wall
    
    void Start()
    {
        wallRenderer = wallTransform.GetComponent<Renderer>();
    }
    
    void Update()
    {
        float distanceToWall = Vector3.Distance(cameraTransform.position, wallTransform.position);
        
        if (distanceToWall < transparencyDistance)
        {
            float alpha = distanceToWall / transparencyDistance;
            
            Color wallColor = wallRenderer.material.color;
            
            wallRenderer.material.color = new Color(wallColor.r, wallColor.g, wallColor.b, alpha);
        }
        else
        {
            Color wallColor = wallRenderer.material.color;
            wallRenderer.material.color = new Color(wallColor.r, wallColor.g, wallColor.b, 1.0f);
        }
    }
}
