using UnityEngine;

public class AlarmOffBtn : MonoBehaviour
{
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        Invoke("Delete", audioSource.clip.length);
    }

    public void Delete()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if ((audioSource != null) && (audioSource.isPlaying)) audioSource.Stop();      
    }
}
