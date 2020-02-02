using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    float YSpawnLoc;
    float XSpawnLoc;

    public GameObject[] EnemyTypes;
    public float time;
    public float tillspawn;
    float rollforEnemy;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        SpawnFreshEnemy(2);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(tillspawn <= time)
        {
            tillspawn = Random.Range(3, 10);
            //10 - 4, 20 - 3, 30 - 2, 40 - 1
            rollforEnemy = Random.Range(0, 100);
            if (rollforEnemy <= 40)
            {
                SpawnFreshEnemy(2);
            }
            else if (rollforEnemy <= 70 && rollforEnemy < 41)
            {
                SpawnFreshEnemy(3);
            }
            else if (rollforEnemy <= 90 && rollforEnemy < 71)
            {
                SpawnFreshEnemy(4);
            }
            else if (rollforEnemy <= 100 && rollforEnemy < 91)
            {
                SpawnFreshEnemy(5);
            }
            time = 0;
        }
    }

    void SpawnFreshEnemy(int amountspawning)
    {
        if (EnemyTypes.Length != 0)
        {
            for (int i = 0; i < amountspawning; i++)
            {
                YSpawnLoc = Random.Range(-140, 140);
                XSpawnLoc = Random.Range(-140, 140);


                Instantiate(EnemyTypes[Random.Range(0, EnemyTypes.Length)], new Vector3(XSpawnLoc, 0, YSpawnLoc), Quaternion.identity);           
            }
        }
        else
        {
            print(amountspawning);
        }
    }
}
