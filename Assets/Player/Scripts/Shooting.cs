using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shooting : MonoBehaviour
{
    public GameObject shield;
    
    public bool disableShooting;

    public int headshotStreak;

    public TMP_Text headshotStreakText;

    public int lives = 1;

    public bool DisableShooting
    {
        set => disableShooting = value;
    }

    [SerializeField] private AudioSource[] gunAudios;
    
    [SerializeField] private ClimbUp climbUp;
    [SerializeField] private GunAim gunAim;
    [SerializeField] private StairSpawner stairSpawner;
    [SerializeField] private Animator headshotPopup;
    [SerializeField] private GameObject[] rays;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject bulletBazooka;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private GunSelection gun;
    [SerializeField] private Transform firstBlood;
    [SerializeField] private Transform[] shotgunDirs;
    [SerializeField] private AudioSource bloodSound;
    
    private Quaternion _initRot;
    private Vector3 _initPos;

    private void Start()
    {
        _initPos = firstBlood.position;
    }

    public void ResetLives()
    {
        lives = 1;
        firstBlood.position = _initPos;
    }

    public void EnableRay()
    {
        rays[^1].SetActive(true);
    }
    
    public void Shoot()
    {
        if (disableShooting) return;

        foreach (var gunAudio in gunAudios)
        {
            if (gunAudio.gameObject.activeInHierarchy)
                gunAudio.Play();
        }
        
        if (gun.currentGunIndex == 3)
            Instantiate(bulletBazooka, bulletSpawnPoint.position, transform.rotation);
        else if (gun.currentGunIndex == 2)
        {
            foreach (var shotgunDir in shotgunDirs)
            {
                Instantiate(bullet, shotgunDir.position, shotgunDir.rotation);
            }
        }
        else
            Instantiate(bullet, bulletSpawnPoint.position, transform.rotation);
        
        foreach (var ray in rays)
        {
            ray.SetActive(false);
        }
        gunAim.stopAiming = true;
        disableShooting = true;

        stairSpawner.currentEnemy.Shoot(false);
        enabled = false;
    }

    public void CheckLives()
    {
        if (gun.currentGunIndex == 4)
        {
            if (lives == 1)
            {
                lives--;
                transform.rotation = climbUp.initGunRotation;
                disableShooting = true;
                shield.SetActive(true);
                
            }
        }
    }

    private void PerformEnemyShot(bool miss)
    {
        var currentEnemy = stairSpawner.currentEnemy;
        currentEnemy.Shoot(miss);
    }

    public void EnemyHit(bool headshot)
    {
        bloodSound.Play();
        
        climbUp.headshot = headshot;
        if (!headshot)
        {
            headshotStreak = 0;
            headshotPopup.transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            CameraShake.Shake();
            if (headshotStreak < 4)
                headshotStreak++;
            if (headshotStreak >= 2)
                headshotPopup.transform.GetChild(0).gameObject.SetActive(true);

            if (headshotStreak == 1)
            {
                headshotPopup.Play("Headshot Popup", -1, 0f);
            }
            else if (headshotStreak == 2)
            {
                headshotPopup.Play("Head2x", -1, 0f);
                headshotStreakText.text = "x" + headshotStreak;
            }
            else if (headshotStreak == 3)
            {
                headshotPopup.Play("Head3x", -1, 0f);
                headshotStreakText.text = "x" + 4;
            }
            else if (headshotStreak == 4)
            {
                headshotPopup.Play("Head3x", -1, 0f);
                headshotStreakText.text = "x" + 8;
            }
        }

        StartCoroutine(EnableMovement());
    }

    private IEnumerator EnableMovement()
    {
        yield return new WaitForSeconds(.3f);
        climbUp.movable = true;
    }
}
