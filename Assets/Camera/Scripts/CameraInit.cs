using UnityEngine;

public class CameraInit : MonoBehaviour
{
    [SerializeField] private Vector3 initPos;
    [SerializeField] private float orthographicSize;
    [SerializeField] private float orthographicSizeInGame = 5f;
    [SerializeField] private Camera mainCam;
    
    private Vector3 _camPos;
    private float _size;
    
    public void RevertCamera()
    {
        _camPos = initPos;
        _size = orthographicSize;
    }

    public void SetCamera(Vector3 pos)
    {
        _camPos = pos;
        _size = orthographicSize;
    }
    
    public void SetCamera()
    {
        _camPos = Vector3.zero;
        _size = orthographicSizeInGame;
    }

    private void Update()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, _camPos, 10f * Time.deltaTime);
        mainCam.orthographicSize = Mathf.Lerp(mainCam.orthographicSize, _size, 10f * Time.deltaTime);
    }
}
