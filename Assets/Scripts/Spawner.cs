using NUnit.Framework;
using System.Collections;

//using System;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject alienSpawnpoint;
    public GameObject spikeSpawnpoint;
    public Vector3 lastXpos;
    public GameObject alienPrefab;
    public GameObject spikePrefab;
    public GameObject platformPrefab;
    public List<GameObject> aliens;
    public List<GameObject> spikes;
    public List<GameObject> hazards;
    public List<GameObject> platforms;
    public int platCount = 4;
    public int hazCount = 12;

    public bool canSpawn;
    public bool hazardSpawner;
    public bool platformSpawner;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        lastXpos = new Vector3(-1, -1.5f, 0);
        if (hazardSpawner)
        {
            //float rand = Random.Range(0f, 1f);
            aliens = new List<GameObject>();
            spikes = new List<GameObject>();
            hazards = new List<GameObject>();
            for (int i = 0; i < hazCount / 2; i++)
            {
                GameObject newSpike = Instantiate(spikePrefab, spikeSpawnpoint.transform.position, Quaternion.identity);
                spikes.Add(newSpike);
            }
        }
        if (platformSpawner)
        {
            platforms = new List<GameObject>();
            for (int i = 0; i < platCount; i++)
            {
                GameObject newPlat = Instantiate(platformPrefab, lastXpos, Quaternion.identity);
                platforms.Add(newPlat);
                lastXpos.x = lastXpos.x + 2f;
                //lastXpos = new Vector3(lastXpos.x + 2, lastXpos.y, lastXpos.z);
            }
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (hazardSpawner)
        {
            for (int i = 0; i < hazards.Count; i++)
            {
                if (hazards[i] != null && !hazards[i].activeInHierarchy)
                {
                    hazards[i].SetActive(true);
                }
            }
        }
        if (platformSpawner)
        {
            for (int i = 0; i < platforms.Count; i++)
            {
                if (platforms[i] != null)
                {
                    if (platforms[i].transform.position.x < lastXpos.x && !platforms[i].activeInHierarchy)
                    {
                        platforms[i].transform.position = lastXpos;
                        platforms[i].SetActive(true);
                        lastXpos.x = lastXpos.x + 2;
                    }
                }
            }
        }
    }

    public IEnumerator DelaySpawn()
    {
        yield return new WaitForSeconds(3f);
    }
}
