using UnityEngine;

public class RatPoisonScript : MonoBehaviour
{
    PlayerInputScript player;

    EnemyChaseScript enemyChaseScript;
    AudioManagerScript audioManagerScript;

    private void Start()
    {
        audioManagerScript = FindAnyObjectByType<AudioManagerScript>();
        enemyChaseScript = FindAnyObjectByType<EnemyChaseScript>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.GetComponent<PlayerInputScript>();
            enemyChaseScript.callGetPoisoned();
            audioManagerScript.PlaySFX(audioManagerScript.PoisonedSFX);
            Destroy(gameObject);
        }
    }
}
