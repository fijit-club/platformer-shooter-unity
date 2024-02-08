using UnityEngine;

public class ShieldDestroy : MonoBehaviour
{
    [SerializeField] private AudioSource shieldDestroyAudio;
    
    private void OnDisable()
    {
        if (gameObject != null)
            shieldDestroyAudio.Play();
    }
}
