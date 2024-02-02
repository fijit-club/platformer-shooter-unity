using System.Collections.Generic;
using UnityEngine;

public class StairSpawner : MonoBehaviour
{
    public static int CurrentStairIndex;
    public static readonly List<StairHandler> StairCases = new List<StairHandler>();
    public EnemyMovement currentEnemy;
    
    [SerializeField] private GameObject[] prefabs;
    [SerializeField] private StairHandler firstStairs;
    [SerializeField] private float xMin;
    [SerializeField] private float xMax;
    [SerializeField] private Transform[] playerLocations;
    [SerializeField] private Transform spawnedObjHolder;
    
    private float _yOffset = 2;
    private int _direction = 1;
    private int _currentOrderIndex = -1;

    public void InitialSpawn()
    {
        StairCases.Add(firstStairs);
        for (int i = 0; i < 5; i++)
            Spawn();
    }

    public void ResetSpawnData()
    {
        _yOffset = 2;
        _direction = 1;
        _currentOrderIndex = -1;
        CurrentStairIndex = 0;
        StairCases.Clear();
    }

    public void Spawn()
    {
        int r = Random.Range(0, prefabs.Length);
        var pos = new Vector3(Random.Range(xMin, xMax), _yOffset, 0f);
        _direction *= -1;

        var temp = Instantiate(prefabs[r], pos, Quaternion.identity, spawnedObjHolder);
        temp.GetComponent<StairHandler>().enemy.spawnedObjHolder = spawnedObjHolder;
        _yOffset += prefabs[r].GetComponent<StairHandler>().yIncrement;

        var playerLocationObject = temp.transform.GetChild(1);
        var enemyLocationObject = temp.transform.GetChild(2);
        playerLocationObject.parent = spawnedObjHolder;
        //enemyLocationObject.parent = null;
        var playerPos = playerLocationObject.position;
        var enemyPos = enemyLocationObject.position;
        if (_direction == 1)
        {
            playerPos.x = playerLocations[1].position.x;
            enemyPos.x = playerLocations[1].position.x;
        }
        else
        {
            playerPos.x = playerLocations[0].position.x;
            enemyPos.x = playerLocations[0].position.x;
        }

        playerLocationObject.position = playerPos;
        //enemyLocationObject.position = enemyPos;
        var scale = temp.transform.localScale;
        scale.x = _direction;
        temp.transform.localScale = scale;
        enemyLocationObject.parent = temp.transform;
        
        var stairHandler = temp.GetComponent<StairHandler>();
        _currentOrderIndex--;
        stairHandler.SetLayerOrder(_currentOrderIndex);

        currentEnemy = StairCases[CurrentStairIndex].GetComponent<StairHandler>().enemy;
        
        StairCases.Add(stairHandler);
        
        UpdateColors();
    }

    public void UpdateColors()
    {
        float factor = 1f;
        for (int i = CurrentStairIndex; i < StairCases.Count; i++)
        {
            StairCases[i].SetColor(factor);
            if (factor > 0f)
                factor -= .2f;
        }
    }
}
