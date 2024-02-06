using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shooting : MonoBehaviour
{
    public bool disableShooting;

    public int headshotStreak;

    public TMP_Text headshotStreakText;

    public bool DisableShooting
    {
        set => disableShooting = value;
    }

    [SerializeField] private ClimbUp climbUp;
    [SerializeField] private GunAim gunAim;
    [SerializeField] private StairSpawner stairSpawner;
    [SerializeField] private Animator headshotPopup;
    [SerializeField] private GameObject ray;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletSpawnPoint;
    

    public void Shoot()
    {
        if (disableShooting) return;
        Instantiate(bullet, bulletSpawnPoint.position, transform.rotation);
        ray.SetActive(false);
        gunAim.stopAiming = true;
        disableShooting = true;
        
        var currentEnemy = stairSpawner.currentEnemy;
        currentEnemy.Shoot();
    }

    public void EnemyHit(bool headshot)
    {
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
