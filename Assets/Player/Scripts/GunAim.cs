using UnityEngine;

public class GunAim : MonoBehaviour
{
    public bool stopAiming;
    
    [SerializeField] private float minRot;
    [SerializeField] private float maxRot;
    [SerializeField] private float rotationSpeed;

    private float _localRotationSpeed;
    
    private void Start()
    {
        _localRotationSpeed = rotationSpeed;
    }

    private void Update()
    {
        if (stopAiming)
        {
            transform.rotation = Quaternion.identity;
            return;
        }
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
        
        if (transform.eulerAngles.z < minRot)
            rotationSpeed = _localRotationSpeed;
        if (transform.eulerAngles.z > maxRot)
            rotationSpeed = -_localRotationSpeed;
    }
}
