using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] levelPrefabs;
    public GameObject[] enemyPrefabs;

    public Slime slimeCharacter;

    public List<GameObject> activeEnemy = new List<GameObject>();
    public List<GameObject> activeLevel = new List<GameObject>();
    public Transform slime;

    public int posSpawnLevel = 0;
    public int posSpawnEnemy = 0;
    public int posFight = 0;

    private void Start()
    {
        SpawnLevel();
    }

    private void Update()
    {
        if(slime.transform.position.x >= posFight)
        {
            slimeCharacter.isBattle = true;
            SpawnEnemy();
            posFight += 20;
        }

        if(slime.transform.position.x - 10 >= activeLevel[0].transform.position.x)
        {
            SpawnLevel();
            DeleteLevel(activeLevel[0]);
            activeLevel.RemoveAt(0);
        }
    }

    public void DeleteLevel(GameObject curLevel)
    {
        Destroy(curLevel);
    }

    public void SpawnEnemy()
    {
        Vector3 posEnemy = new(0, 6, -2.4f);
        //Vector3 rotEnemy = new(0, -90, 0);

        GameObject spawn = Instantiate(enemyPrefabs[0],posEnemy + transform.right * posSpawnEnemy, transform.rotation);
        activeEnemy.Add(spawn);
        posSpawnEnemy += 20;
    }

    public void SpawnLevel()
    {
        GameObject spawn = Instantiate(levelPrefabs[0], transform.right * posSpawnLevel, transform.rotation);
        activeLevel.Add(spawn);
        posSpawnLevel += 20;
    }
}
