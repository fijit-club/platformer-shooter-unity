using UnityEngine;

public class GunAim : MonoBehaviour
{
    public bool stopAiming;
    
    [SerializeField] private float minRot;
    [SerializeField] private float maxRot;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float speedIncrement;
    [SerializeField] private float maxSpeed;
    
    private float _localRotationSpeed;
    private Vector3 _startPos;
    private Quaternion _startRot;

    public void ResetGun()
    {
        _localRotationSpeed = rotationSpeed;
        stopAiming = false;
        transform.position = _startPos;
        transform.rotation = _startRot;
    }
    
    private void Start()
    {
        _localRotationSpeed = rotationSpeed;
        _startPos = transform.position;
        _startRot = transform.rotation;
    }

    public void IncreaseSpeed()
    {
        if (_localRotationSpeed < maxSpeed)
            _localRotationSpeed += speedIncrement;
    }

    private void Update()
    {
        if (stopAiming) return;
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
        
        if (transform.eulerAngles.z < minRot)
            rotationSpeed = _localRotationSpeed;
        if (transform.eulerAngles.z > maxRot)
        {
            enabled = false;
        }
    }
}
