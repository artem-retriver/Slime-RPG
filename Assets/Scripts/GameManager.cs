using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour, IUpdateATK, IUpdateSPD, IUpdateHP
{
    public TextMeshProUGUI textASPD;
    public TextMeshProUGUI textATK;
    public TextMeshProUGUI textHP;
    public TextMeshProUGUI textCoins;
    public TextMeshProUGUI textLevel;

    public UI uI;
    public GameObject[] levelPrefabs;
    public Enemy[] enemyPrefabs;

    public Slime slimeCharacter;

    public List<Enemy> activeEnemy = new();
    public List<GameObject> activeLevel = new();
    public Transform slime;

    public int posSpawnLevel = 0;
    public int posSpawnEnemy = 0;
    public int posFight = 0;
    public int coins = 0;
    public int timeInsEnemy = 2;

    public int countLevel = 0;
    public int startCountEnemy = 5;
    public int endCountEnemy = 5;

    private void Start()
    {
        SpawnLevel();
    }

    private void Update()
    {
        textASPD.text = slimeCharacter.spd.ToString();
        textATK.text = slimeCharacter.atk.ToString();
        textHP.text = slimeCharacter.hp.ToString();
        textLevel.text = countLevel.ToString();
        textCoins.text = coins.ToString();

        if(slimeCharacter.hp <= 0)
        {
            uI.ShowLoseScreen();
        }

        if (slime.transform.position.x >= posFight)
        {
            slimeCharacter.InvokeBattle();
            slimeCharacter.isBattle = true;
            InvokeRepeating(nameof(SpawnEnemy), timeInsEnemy, timeInsEnemy);          
            posFight += 20;
        }

        if (startCountEnemy <= 0)
        {
            CancelInvoke(nameof(SpawnEnemy));

            if (activeEnemy.Count <= 0)
            {
                slimeCharacter.CancelInvokeBattle();
                slimeCharacter.isBattle = false;
                endCountEnemy += 5;
                startCountEnemy = endCountEnemy;
                posSpawnEnemy += 20;
            }
        }

        if (slime.transform.position.x - 10 >= activeLevel[0].transform.position.x)
        {
            SpawnLevel();
            DeleteLevel(activeLevel[0]);
            activeLevel.RemoveAt(0);
        }

        for (int i = 0; i < activeEnemy.Count; i++)
        {
            if (activeEnemy[i].hp == 0)
            {
                slimeCharacter.isEnemy = false;
            }
        }

        for (int i = 0; i < activeEnemy.Count; i++)
        {
            if (activeEnemy[i].hp <= 0)
            {
                DeleteEnemy(activeEnemy[i]);
                activeEnemy.RemoveAt(i);
                coins++;
            }
        }
    }

    public void DeleteLevel(GameObject curLevel)
    {
        Destroy(curLevel);
    }

    public void DeleteEnemy(Enemy enemy)
    {
        Destroy(enemy.gameObject);
    }

    public void SpawnEnemy()
    {
        Vector3 posEnemy = new(0, 6, -2.4f);

        Enemy spawn = Instantiate(enemyPrefabs[0],posEnemy + transform.right * posSpawnEnemy, transform.rotation);
        activeEnemy.Add(spawn);
        startCountEnemy--;
    }

    public void SpawnLevel()
    {
        GameObject spawn = Instantiate(levelPrefabs[0], transform.right * posSpawnLevel, transform.rotation);
        activeLevel.Add(spawn);
        posSpawnLevel += 20;
        countLevel++;
    }

    public void UpdateATK()
    {
        if(coins >= 5)
        {
            coins -= 5;
            slimeCharacter.atk += 25;
        }
    }

    public void UpdateSPD()
    {
        if(coins >= 5)
        {
            coins -= 5;
            slimeCharacter.spd -= 0.1f;
        }
    }

    public void UpdateHP()
    {
        if(coins >= 5)
        {
            coins -= 5;
            slimeCharacter.hp += 50;
        }   
    }
}
