using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SocialPlatforms.Impl;
using Random = UnityEngine.Random;

public class SpawnBot : Singleton<SpawnBot>
{
    public int totalBotRemain;
    public int botAlive;
    public GameObject botPrefab;
    public bool spawnStart;
    public Player player;
    public Vector3 RandomPosition()
    {
        float radius = Random.Range(25f, 50f);
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, radius, 1);
        Vector3 finalPosition = hit.position;
        return finalPosition;
    }

    public void Spawn(int num)
    {
        //int numberOfBotSpawnInWave = Random.Range(minBotInWave, totalBotRemain / 2);
        Debug.Log("======Wave======");
        Debug.Log("Total:" + totalBotRemain);
        Debug.Log("Alive:" + botAlive);
        if (totalBotRemain > 0)
        {
            for (int i = 0; i < num; i++)
            {
                GameObject GO = Instantiate(botPrefab);
                GO.transform.position = RandomPosition();
            }
        }
        botAlive += num;
        totalBotRemain -= num;
    }

    private void Update()
    {
        if (spawnStart)
        {
            if (player.gameObject.activeInHierarchy)
            {
                if (botAlive <= 3)
                {
                    int num = Random.Range(2, 5);
                    if (num >= totalBotRemain)
                    {
                        num = totalBotRemain;
                    }
                    Spawn(num);
                }

                if (botAlive == 0 && totalBotRemain == 0)
                {
                    UIManager.Ins.OpenUI<Win>();
                }
            }
            else
            {
                UIManager.Ins.OpenUI<Lose>();
            }

        }
    }
}
