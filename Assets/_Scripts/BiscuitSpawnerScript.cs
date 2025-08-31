using System.Collections;
using UnityEngine;

public class BiscuitSpawnerScript : MonoBehaviour
{
    public int[] totalAmountToBeSpawned;
    public GameObject biscuitPrefab;
    public GameObject cookiesParent;
    public int spawnedCookies = 0;
    public LayerMask obstacleMask;

    StagesScript stagesScript;

    void Start()
    {
        stagesScript = FindAnyObjectByType<StagesScript>();
    }

    public void callSpawnCookies()
    {
        spawnedCookies = 0;
        StartCoroutine(DelayedSpawn());
    }

    IEnumerator DelayedSpawn()
    {
        yield return null;
        yield return StartCoroutine(spawnCookies());
    }

    IEnumerator spawnCookies()
    {
        int toSpawn = totalAmountToBeSpawned[stagesScript.currentStageLevel - 1];
        while (spawnedCookies < toSpawn)
        {
            Collider2D currentCollider = stagesScript.cameraBounds[stagesScript.currentStageLevel - 1];
            float x = Random.Range(currentCollider.bounds.min.x, currentCollider.bounds.max.x);
            float y = Random.Range(currentCollider.bounds.min.y, currentCollider.bounds.max.y);
            Vector2 randomPos = new Vector2(x, y);

            if (!currentCollider.OverlapPoint(randomPos))
            continue; 

            float radius = 0.3f;

            Collider2D hit = Physics2D.OverlapCircle(randomPos, radius, obstacleMask);
            if (hit == null)
            {
                Instantiate(biscuitPrefab, randomPos, Quaternion.identity, cookiesParent.transform);
                spawnedCookies++;
            }
        }
        yield return new WaitForSeconds(0.2f);
    }

}
