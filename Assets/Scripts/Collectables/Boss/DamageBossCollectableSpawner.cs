using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBossCollectableSpawner : Singleton<DamageBossCollectableSpawner>
{
    public Vector2[] possiblePositions;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnNew() {
        createDamageBossCollectableAt(getRandomPosition());
    }

    private Vector2 getRandomPosition() {
        int randomIndex = Random.Range(0, possiblePositions.Length);
        return possiblePositions[randomIndex];
    }

    private void createDamageBossCollectableAt(Vector2 position) {
        Instantiate(Resources.Load<GameObject>("Boss/DamageBossCollectable"), position, Quaternion.identity);
    }
}
