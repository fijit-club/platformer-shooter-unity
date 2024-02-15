using System.Collections;
using DG.Tweening;
using UnityEngine;

public class ClimbUp : MonoBehaviour
{
    public bool movable;
    public bool headshot;
    public bool died;
    public GunAim gunAim;
    public GunSelection gun;
    public Shooting shooting;
    public Quaternion initGunRotation;
    
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float rayDistance;
    [SerializeField] private Transform gunTransform;
    [SerializeField] private GameObject[] rays;
    [SerializeField] private float rayOffset;
    [SerializeField] private StairSpawner stairSpawner;
    [SerializeField] private CameraMovement cameraMovement;
    [SerializeField] private float cameraIncrementY;
    [SerializeField] private ScoringSystem scoring;
    [SerializeField] private int increment;
    [SerializeField] private int coinIncrement;
    [SerializeField] private ParticleSystem blood;
    [SerializeField] private CameraInit cameraInit;
    [SerializeField] private GameObject shieldSpark;
    [SerializeField] private AudioSource bloodSound;
    [SerializeField] private AudioSource reload;
    
    private Rigidbody2D _rb;
    private int _moveDir = -1;
    private Vector3 _startPos;
    private Quaternion _startRot;
    private bool scoreIncreased;

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
        _moveDir = -1;
        movable = false;
        initGunRotation = Quaternion.identity;
        shooting.headshotStreak = 0;
        died = false;
        shooting.headshotStreakText.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        if (blood == null) return;
        blood.transform.position = transform.position;
        if (bloodSound != null)
            bloodSound.Play();
        if (blood.transform.position.x < 0f)
            blood.transform.localScale = -Vector3.one;
        else
            blood.transform.localScale = Vector3.one;
        blood.Play();
        if (gunAim)
            gunAim.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("StairsTop"))
        {
            reload.Play();
            shieldSpark.SetActive(false);
            shooting.lives = 1;
            movable = false;
            _rb.velocity = Vector2.zero;

            var rot = gunTransform.eulerAngles;
            rot.y -= 180f;
            rot.z = 0f;
            gunTransform.eulerAngles = rot;
            initGunRotation = gunTransform.rotation;


            int stairIndex = StairSpawner.CurrentStairIndex++;
            stairSpawner.Spawn();
            var stairHandler = StairSpawner.StairCases[stairIndex + 1];
            stairHandler.TurnOnColliders();
            stairHandler.enemy.Move();

            var pos = cameraMovement.transform.position;
            pos.y = transform.position.y + cameraIncrementY;
            cameraMovement.UpdateCamera(pos.y);
            StartCoroutine(DelayReachedTop());
        }
    }

    private IEnumerator DelayReachedTop()
    {
        yield return new WaitForSeconds(1f);
        //ReachedTop();
    }


    
    public void UpdateCamera()
    {
        cameraInit.SetCamera(transform.position - (Vector3.up * .3f));
    }

    public void ReachedTop()
    {
        gunAim.IncreaseSpeed();
        shooting.disableShooting = false;
        gunAim.StartAim();
        
        foreach (var ray in rays)
        {
            ray.SetActive(true);
        }
        _moveDir *= -1;
    }

    private void FixedUpdate()
    {
        gunTransform.position = transform.position;

        if (movable)
        {
            if (!scoreIncreased)
            {
                if (headshot)
                    Headshot();
                else
                    scoring.UpdateScore(increment);
                scoreIncreased = true;
            }
            var gunRotation = gunAim.transform.eulerAngles;
            gunRotation.z = 0f;
            gunAim.transform.eulerAngles = gunRotation;

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
        else
        {
            scoreIncreased = false;
        }
    }

    private void Headshot()
    {
        if (shooting.headshotStreak == 2)
            scoring.UpdateScore(increment + 20 * 2);
        else if (shooting.headshotStreak == 3)
            scoring.UpdateScore(increment + 20 * 4);
        else if (shooting.headshotStreak == 4)
            scoring.UpdateScore(increment + 20 * 8);
        else
            scoring.UpdateScore(increment + 20);
            
        scoring.UpdateCoins(coinIncrement);
    }
}
