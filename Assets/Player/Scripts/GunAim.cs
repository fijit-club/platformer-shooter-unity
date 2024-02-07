using UnityEngine;

public class GunAim : MonoBehaviour
{
    public bool stopAiming;
    
    [SerializeField] private float maxRot;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float speedIncrement;
    [SerializeField] private float maxSpeed;
    [SerializeField] private StairSpawner stairSpawner;
    
    private float _localRotationSpeed;
    private Vector3 _startPos;
    private Quaternion _startRot;
    private float _initSpeed;

    public void ResetGun()
    {
        _localRotationSpeed = rotationSpeed;
        StartAim();
        transform.position = _startPos;
        transform.rotation = _startRot;
        _localRotationSpeed = _initSpeed;
        rotationSpeed = _initSpeed;
    }
    
    private void Start()
    {
        _localRotationSpeed = rotationSpeed;
        _startPos = transform.position;
        _startRot = transform.rotation;
        _initSpeed = rotationSpeed;
    }

    public void IncreaseSpeed()
    {
        if (_localRotationSpeed < maxSpeed)
            _localRotationSpeed += speedIncrement;
    }

    public void StartAim()
    {
        stopAiming = false;
        rotationSpeed = _localRotationSpeed;
    }

    private void Update()
    {
        if (stopAiming) return;
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);

        var z = transform.eulerAngles.z;
        if (z > maxRot)
        {
            stairSpawner.currentEnemy.Shoot();
            enabled = false;
        }
    }
}
