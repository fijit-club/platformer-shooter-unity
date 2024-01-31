using System.Collections.Generic;
using UnityEngine;

public class StairSpawner : MonoBehaviour
{
    public static int CurrentStairIndex;
    public static readonly List<StairHandler> StairCases = new List<StairHandler>();
    
    [SerializeField] private float yIncrement;
    [SerializeField] private GameObject prefab;
    [SerializeField] private StairHandler firstStairs;
    [SerializeField] private float xMin;
    [SerializeField] private float xMax;
    [SerializeField] private Transform[] playerLocations;
    
    private float _yOffset;
    private int _direction = 1;
    private int _currentOrderIndex = -1;
    private Vector3 _playerLocation;
    
    private void Start()
    {
        StairCases.Add(firstStairs);
        for (int i = 0; i < 5; i++)
            Spawn();
    }

    public void Spawn()
    {
        _yOffset += yIncrement;
        var pos = new Vector3(Random.Range(xMin, xMax), _yOffset, 0f);
        _direction *= -1;
        
        var temp = Instantiate(prefab, pos, Quaternion.identity);

        var playerLocationObject = temp.transform.GetChild(1);
        var enemyLocationObject = temp.transform.GetChild(2);
        playerLocationObject.parent = null;
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
        
        StairCases.Add(stairHandler);
        
        UpdateColors();
    }

    public void UpdateColors()
    {
        float factor = 1f;
        for (int i = CurrentStairIndex; i < StairCases.Count; i++)
        {
            StairCases[i].SetColor(factor);
            factor -= .2f;
        }
    }
}
