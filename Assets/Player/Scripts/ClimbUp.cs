using UnityEngine;

public class ClimbUp : MonoBehaviour
{
    public bool movable;
    
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float rayDistance;
    [SerializeField] private Transform gunTransform;
    [SerializeField] private GameObject ray;
    [SerializeField] private Shooting shooting;
    [SerializeField] private float rayOffset;
    [SerializeField] private StairSpawner stairSpawner;
    [SerializeField] private CameraMovement cameraMovement;
    [SerializeField] private float cameraIncrementY;
    [SerializeField] private GunAim gunAim;
    
    private Rigidbody2D _rb;
    private int _moveDir = 1;
    private Vector3 _startPos;
    private Quaternion _startRot;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        cameraIncrementY = cameraMovement.transform.position.y - transform.position.y;

        _startPos = transform.position;
        _startRot = transform.rotation;
    }

    public void ResetPosition()
    {
        transform.position = _startPos;
        transform.rotation = _startRot;
        _moveDir = 1;
        movable = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("StairsTop"))
        {
            gunAim.IncreaseSpeed();
            gunAim.stopAiming = false;
            shooting.disableShooting = false;
            movable = false;
            _rb.velocity = Vector2.zero;
            
            var rot = gunTransform.eulerAngles;
            rot.y -= 180f;
            rot.z = 0f;
            gunTransform.eulerAngles = rot;
            
            ray.SetActive(true);
            _moveDir *= -1;

            int stairIndex = StairSpawner.CurrentStairIndex++;
            stairSpawner.Spawn();

            var stairHandler = StairSpawner.StairCases[stairIndex + 1];
            stairHandler.TurnOnColliders();
            stairHandler.enemy.move = true;

            var pos = cameraMovement.transform.position;
            pos.y = transform.position.y + cameraIncrementY;
            cameraMovement.UpdateCamera(pos.y);
        }
    }

    private void FixedUpdate()
    {
        gunTransform.position = transform.position;

        if (movable)
        {
            var gunRotation = gunAim.transform.eulerAngles;
            gunRotation.z = 0f;
            gunAim.transform.eulerAngles = gunRotation;
            print(gunAim.transform.eulerAngles.z);

            var transform1 = transform;
            var right = transform1.right;
            _rb.velocity = _moveDir * right * movementSpeed;

            var raycastHit2D = Physics2D.Raycast(transform1.position - (Vector3.up * rayOffset), right * _moveDir, rayDistance);
            if (!raycastHit2D) return;
            if (raycastHit2D.transform.CompareTag("Platform"))
            {
                _rb.velocity = Vector2.up * jumpForce;
            }
        }
    }
}
