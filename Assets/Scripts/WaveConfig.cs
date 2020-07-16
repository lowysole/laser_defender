using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy WaveConfig")]
public class WaveConfig : ScriptableObject
{
    // Start is called before the first frame update
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float spawnRate = 0.5f;
    [SerializeField] float spawnRateRandom = 0.3f;
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] int numEnemies= 5;

    public GameObject GetEnemyPrefab() { return enemyPrefab; }

    public List<Transform> GetWayPoints() {
        var waypoints = new List<Transform>();
        foreach(Transform child in pathPrefab.transform)
        {
            waypoints.Add(child);
        }
        return waypoints;
    }
    public float GetSpawnRate() { return spawnRate; }

    public float GetSpawnRateRandom() { return spawnRateRandom; }

    public float GetMoveSpeed() { return moveSpeed; }

    public int GetNumEnemies() { return numEnemies; }
}

