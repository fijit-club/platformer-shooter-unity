using UnityEngine;

public class FirstEnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform spawnedObjHolder;
    [SerializeField] private Transform enemyLoc;
    [SerializeField] private StairHandler stairHandler;
    
    private void OnEnable()
    {
        var temp = Instantiate(enemyPrefab, transform.position, transform.rotation, spawnedObjHolder);
        var stairHandlerEnemy = temp.GetComponent<EnemyMovement>();
        stairHandlerEnemy.enemyLocation = enemyLoc;
        stairHandlerEnemy.first = true;
        stairHandler.enemy = stairHandlerEnemy;
    }
}
