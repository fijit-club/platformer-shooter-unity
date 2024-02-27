using System.Collections;
using ShooterGame;
using UnityEngine;

public class GunAim : MonoBehaviour
{
    public bool stopAiming;
    public bool inTutorial;
    
    [SerializeField] private float maxRot;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float speedIncrement;
    [SerializeField] private float maxSpeed;
    [SerializeField] private StairSpawner stairSpawner;
    [SerializeField] private GunSelection gun;
    [SerializeField] private Shooting shooting;
    [SerializeField] private ClimbUp climb;
    [SerializeField] private float tutorialDelay;
    [SerializeField] private GameObject tutorialText1;
    [SerializeField] private GameObject tutorialText2;
    [SerializeField] private Transform muzzle;
    [SerializeField] private Transform headHighlight;
    
    private float _localRotationSpeed;
    private Vector3 _startPos;
    private Quaternion _startRot;
    private float _initSpeed;
    private int _tutorialIndex;
    private bool _tutHeadshotRay;
    
    public void RestartShootingAfterShield()
    {
        StartCoroutine(DelayedRestart());
    }

    private IEnumerator DelayedRestart()
    {
        yield return new WaitForSeconds(.3f);
        var shooting = transform.root.GetComponent<Shooting>();
        shooting.EnableRay();
        enabled = true;
        stopAiming = false;
        shooting.disableShooting = false;
    }

    public void ResetGun()
    {
        _localRotationSpeed = rotationSpeed;
        stopAiming = true;
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

    public void CheckTutorial()
    {
        if (Bridge.GetInstance().thisPlayerInfo.highScore == 0)
        {
            inTutorial = true;
            _tutorialIndex = 0;
        }
        else
        {
            inTutorial = false;
        }
    }

    public void StartAim()
    {
        stopAiming = false;
        rotationSpeed = _localRotationSpeed;

        print(inTutorial);

        if (inTutorial)
        {
            shooting.disableShooting = true;

            if (_tutorialIndex == 0)
                StartCoroutine(DisplayTutorial());
            else if (_tutorialIndex == 1)
            {
                _tutHeadshotRay = true;
                inTutorial = false;
            }

            _tutorialIndex++;
        }
        else
        {
            shooting.disableShooting = false;
            _tutHeadshotRay = false;
        }
    }

    private IEnumerator DisplayTutorial()
    {
        yield return new WaitForSeconds(tutorialDelay);
        stopAiming = true;
        tutorialText1.SetActive(true);
        shooting.disableShooting = false;
    }

    private void Update()
    {
        if (stopAiming) return;
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);

        if (shooting.freeShot)
        {
            RaycastHit2D hit = Physics2D.Raycast(muzzle.position, muzzle.right, Mathf.Infinity);
            if (hit)
            {
                shooting.disableShooting = true;
                rotationSpeed = 120f;
                CheckHead(hit);
            }
        }

        if (_tutHeadshotRay)
        {
            RaycastHit2D hit = Physics2D.Raycast(muzzle.position, muzzle.right, Mathf.Infinity);
            if (hit)
            {
                if (hit.transform.CompareTag("Enemy"))
                {
                    if (hit.point.y > hit.transform.parent.GetComponent<EnemyMovement>().enemyHeadLocation.position.y)
                    {
                        stopAiming = true;
                        tutorialText2.SetActive(true);
                        var rot = muzzle.rotation;
                        tutorialText2.transform.rotation = rot;
                        shooting.disableShooting = false;
                        headHighlight.gameObject.SetActive(true);
                        headHighlight.position = stairSpawner.currentEnemy.head.position;
                    }
                }
            }
        }
        
        var z = transform.eulerAngles.z;
        if (z > maxRot)
        {
            if (gun.currentGunIndex == 4)
            {
                if (shooting.lives == 1)
                {
                    shooting.lives--;
                    transform.rotation = climb.initGunRotation;
                    shooting.disableShooting = true;
                    shooting.shield.SetActive(true);
                }
            }
            stairSpawner.currentEnemy.Shoot(false);
            enabled = false;
        }
    }

    private void CheckHead(RaycastHit2D hit)
    {
        if (hit.transform.CompareTag("Enemy"))
        {
            if (hit.point.y > hit.transform.parent.GetComponent<EnemyMovement>().enemyHeadLocation.position.y)
            {
                stopAiming = true;
                shooting.freeShot = false;
                StartCoroutine(DelayedShoot());
            }
        }
    }

    private IEnumerator DelayedShoot()
    {
        yield return new WaitForSeconds(.3f);
        shooting.disableShooting = false;
        shooting.Shoot();
        shooting.crosshairAnimation.Play("Idle", -1, 0f);
    }
}
