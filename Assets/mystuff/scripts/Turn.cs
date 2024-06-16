using UnityEngine;

public class Turn : MonoBehaviour
{
    public float rotationSpeed = 1f; // Speed at which the skybox rotates

    void Update()
    {
        // Rotate the skybox by adjusting the rotation of the material
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * rotationSpeed);
    }
}