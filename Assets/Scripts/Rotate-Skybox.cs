using UnityEngine;

public class RotateSkybox : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 12f;

    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * rotateSpeed);
    }
}