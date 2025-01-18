using UnityEngine;

public class Explosion : MonoBehaviour
{
    AudioSource explosionFX;
    void Start()
    {
        explosionFX = GetComponent<AudioSource>();
        explosionFX.PlayOneShot(explosionFX.clip);
    }

}
