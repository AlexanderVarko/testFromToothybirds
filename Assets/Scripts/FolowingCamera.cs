using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FolowingCamera : MonoBehaviour
{
    public Transform objectToFollow;
    public float cameraDistance;
    public float velocity;
    
    void Start()
    {
        
    }


    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(0, 0, objectToFollow.position.z - cameraDistance), velocity*Time.deltaTime);
    }
}
