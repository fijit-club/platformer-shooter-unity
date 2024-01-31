using System.Collections.Generic;
using UnityEngine;

public class StairSpawner : MonoBehaviour
{
    public static int CurrentStairIndex;
    public static readonly List<StairHandler> StairCases = new List<StairHandler>();
    
    [SerializeField] private float yIncrement;
    [SerializeField] private GameObject prefab;
    [SerializeField] private StairHandler firstStairs;
    
    private float _yOffset;
    private int _direction = 1;
    private int _currentOrderIndex = -1;
    
    private void Start()
    {
        StairCases.Add(firstStairs);
        for (int i = 0; i < 5; i++)
            Spawn();
    }

    public void Spawn()
    {
        _yOffset += yIncrement;
        var pos = new Vector3(0f, _yOffset, 0f);
        _direction *= -1;
        
        var temp = Instantiate(prefab, pos, Quaternion.identity);
        var scale = temp.transform.localScale;
        scale.x = _direction;
        temp.transform.localScale = scale;

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
