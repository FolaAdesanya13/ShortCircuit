using UnityEngine;

public class ViewRecoil : MonoBehaviour
{

    public Vector3 currRotation, targetRotation;
    public float recoilX = -1.5f, recoilY = 0.25f, recoilZ = 2.0f;
    public float snappiness = 7.5f, returnStrength = 15.0f;

    void Update()
    {
        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, returnStrength * Time.deltaTime);
        currRotation = Vector3.Slerp(currRotation, targetRotation, snappiness * Time.fixedDeltaTime);
        transform.localRotation = Quaternion.Euler(currRotation);
    }

    public void AddRecoil()
    {
        targetRotation += new Vector3(recoilX, Random.Range(-recoilY, recoilY), Random.Range(-recoilZ, recoilZ));
    }
}
