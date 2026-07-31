using NUnit.Framework;
using System.Collections;

//using System;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    
    public Vector3 spikeOrigin;
    public Vector3 spikeLiveSpawn;
    public Vector3 spikePoint;
    public GameObject alienSpawner;
    public GameObject spikeSpawner;
    public Vector3 alienOrigin;
    public Vector3 alienLiveSpawn;
    public Vector3 alienPoint;
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
    public float timeUntilSpawn;
    public float objSpawnTime;
    public bool canSpawn;
    public bool hazardSpawner;
    public bool platformSpawner;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        // gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GM>();
        if (hazardSpawner)
        {
            spikeOrigin = spikeSpawner.transform.position;
            alienOrigin = alienSpawner.transform.position;
        }
        if (hazardSpawner)
        {
            
            //float rand = Random.Range(0f, 1f);
            aliens = new List<GameObject>();
            spikes = new List<GameObject>();
            hazards = new List<GameObject>();
            for (int i = 0; i < hazCount / 2; i++)
            {
                GameObject newSpike = Instantiate(spikePrefab, spikeSpawner.transform.position, Quaternion.identity);
                //newSpike.transform.SetParent(spikeSpawnpoint.transform);
                newSpike.SetActive(false);
                spikes.Add(newSpike);

                GameObject newAlien = Instantiate(alienPrefab, alienSpawner.transform.position, Quaternion.identity);
                //newAlien.transform.SetParent(alienSpawnpoint.transform);
                newAlien.SetActive(false);
                aliens.Add(newAlien);
                hazards.Add(newAlien);
                hazards.Add(newSpike);
            }
        }
        if (platformSpawner)
        {
            lastXpos = new Vector3(-1, -1.5f, 1);
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
            timeUntilSpawn += Time.deltaTime;
            alienLiveSpawn = alienSpawner.transform.position;
            spikeLiveSpawn = spikeSpawner.transform.position;
            spikePoint = new Vector3(spikeLiveSpawn.x, spikeOrigin.y, spikeLiveSpawn.z);
            alienPoint = new Vector3(alienLiveSpawn.x, alienOrigin.y, alienLiveSpawn.z);

            if (timeUntilSpawn > objSpawnTime)
            {
                GameObject next = GetInactive(hazards);
                if (next != null)
                {
                    next.SetActive(true);
                    timeUntilSpawn = 0f;
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

    GameObject GetInactive(List<GameObject> pool)
    {
        foreach (GameObject obj in pool)
        {
            //timeUntilSpawn += Time.deltaTime;
            //if (timeUntilSpawn > objSpawnTime)
            {
                if (!obj.activeInHierarchy)
                {
                    timeUntilSpawn = 0;
                    if (obj.CompareTag("Death"))
                    {
                        obj.transform.position = spikePoint;
                    }
                    else
                    {
                        obj.transform.position = alienPoint;
                    }
                        return obj;
                }

            }
        }
        return null; // pool exhausted
    }

    public IEnumerator DelaySpawn()
    {
        yield return new WaitForSeconds(3f);
    }
}
