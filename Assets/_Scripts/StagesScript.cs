using Cinemachine;
using UnityEngine;

public class StagesScript : MonoBehaviour
{
    public int currentStageLevel = 1;
    public string[] stageNames;
    public Transform[] playerSpawnLocations;
    public Collider2D[] cameraBounds;
    public CinemachineConfiner2D confiner;

    private int lastStageLevel = -1;

    BiscuitSpawnerScript biscuitSpawnerScript;
    GameOverScript gameOverScript;

    [System.Serializable]
    public class Stage
    {
        public int totalSpawnLocation;
        public Transform[] spawnPositions;
    }

    public Stage[] stages;

    void Start()
    {
        biscuitSpawnerScript = FindAnyObjectByType<BiscuitSpawnerScript>();
        gameOverScript = FindAnyObjectByType<GameOverScript>();
    }

    void Update()
    {
        if (currentStageLevel != lastStageLevel)
        {
            grabSpawnPos();
            updateCameraBounds();
            Debug.Log("Spawning Cookies at Stage");
            biscuitSpawnerScript.callSpawnCookies();
            lastStageLevel = currentStageLevel;
        }

        if (currentStageLevel == 4)
        {
            gameOverScript.playerWon();
            currentStageLevel = 1;
        }
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

    public void updateCameraBounds()
    {
        confiner.m_BoundingShape2D = cameraBounds[currentStageLevel - 1];
        confiner.InvalidateCache();
    }
}