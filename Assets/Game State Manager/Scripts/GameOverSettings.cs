using System.Collections;
using UnityEngine;

public class GameOverSettings : MonoBehaviour
{
    [SerializeField] private Transform spawnedObjects;
    [SerializeField] private Transform enemy;
    [SerializeField] private Transform gun;
    [SerializeField] private Transform emptyTransform;
    [SerializeField] private MainMenuState mainMenuState;
    [SerializeField] private StairSpawner stairSpawner;
    [SerializeField] private ClimbUp climbUp;
    [SerializeField] private GunAim gunAim;
    [SerializeField] private EnemyMovement enemyMovement;
    [SerializeField] private CameraMovement cameraMovement;
    
    private Vector3 _initEnemyGunPos;
    private Quaternion _initEnemyGunRot;
    
    public void ReplayGame()
    {
        for (int i = 0; i < spawnedObjects.childCount; i++)
            Destroy(spawnedObjects.GetChild(i).gameObject);
        
        stairSpawner.ResetSpawnData();
        
        climbUp.ResetPosition();
        gunAim.ResetGun();
        enemyMovement.ResetEnemy();
        gun.parent = enemy;
        emptyTransform.parent = enemy;
        cameraMovement.ResetCameraPosition();
        
        enemyMovement.gameObject.SetActive(false);
        
        GameStateManager.ChangeState(mainMenuState);
    }
}
