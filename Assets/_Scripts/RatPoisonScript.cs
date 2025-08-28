using UnityEngine;

public class RatPoisonScript : MonoBehaviour
{
    PlayerInputScript player;

    EnemyChaseScript enemyChaseScript;

    private void Start()
    {
        enemyChaseScript = FindAnyObjectByType<EnemyChaseScript>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.GetComponent<PlayerInputScript>();
            enemyChaseScript.callGetPoisoned();
            Destroy(gameObject);
        }
    }
}
