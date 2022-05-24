using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    public GameObject[] clouds;
    public float spawnDuration;
    [SerializeField]
    private float timer;
    private SphereCollider spawnerCollider;
    void Start()
    {
        spawnerCollider = GetComponent<SphereCollider>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(spawnDuration < timer && NoCloudsAroundSpawner())
        {
            CloadSpawn();
        }
    }
    private bool NoCloudsAroundSpawner()
    {
        RaycastHit hit;
        Physics.Raycast(spawnerCollider.bounds.center, Vector3.left, out hit, spawnerCollider.bounds.extents.y);
        return hit.collider == null;
    }
    private void CloadSpawn()
    {
        float CloudSpawnDistance = Random.Range(10, 200);
        float cloudSpawnHeight = Random.Range(100, 300);
        GameObject cloud = Instantiate(clouds[Random.Range(0, clouds.Length)], CloudSpawnPos(CloudSpawnDistance, cloudSpawnHeight), Quaternion.identity);
        cloud.transform.parent = gameObject.transform;
        cloud.GetComponent<CloudMover>().speed = Random.Range(1, 10);
            timer = 0;
    }
    private Vector3 CloudSpawnPos(float CloudSpawnDistance, float cloudSpawnHeight)
    {
        float CamWidth = Camera.main.scaledPixelWidth;
        float CamHeight = Camera.main.scaledPixelHeight;
        float distanceCoeficient = CamWidth/CloudSpawnDistance;
        return Camera.main.ScreenToWorldPoint(new Vector3(CamWidth + 30 *  distanceCoeficient, CamHeight- cloudSpawnHeight, CloudSpawnDistance));

    }
}
