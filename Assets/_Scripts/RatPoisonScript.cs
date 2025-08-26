using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RatPoisonScript : MonoBehaviour
{
    PlayerInputScript player;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.GetComponent<PlayerInputScript>();
            StartCoroutine(gotPoisoned());
        }
    }

    IEnumerator gotPoisoned()
    {
        Debug.Log("I got hit with poison");
        player.speed--;
        Destroy(gameObject);
        yield return new WaitForSeconds(5f);
        player.speed++;
    }
}
