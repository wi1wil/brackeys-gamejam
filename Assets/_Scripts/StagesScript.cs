using UnityEngine;

public class StagesScript : MonoBehaviour
{
    public int currentStageLevel = 1;
    public string[] stageNames;
    public Transform[] playerSpawnLocations;
    public Collider2D[] cameraBounds;

    [System.Serializable]
    public class Stage
    {
        public int totalSpawnLocation;
        public Transform[] spawnPositions;
    }

    public Stage[] stages;

    void Update()
    {
        grabSpawnPos();
    }

    public void grabSpawnPos()
    {
        if (currentStageLevel > 3)
        {
            currentStageLevel = 1;
            return;
        }
        GameObject currentStageParent = GameObject.FindGameObjectWithTag(stageNames[currentStageLevel - 1]);

        int totalChild = currentStageParent.transform.childCount;
        stages[currentStageLevel - 1].totalSpawnLocation = totalChild;
        stages[currentStageLevel - 1].spawnPositions = new Transform[totalChild];

        int count = 0;
        foreach (Transform child in currentStageParent.transform)
        {
            stages[currentStageLevel - 1].spawnPositions[count] = child;
            count++;
        }
    }
}