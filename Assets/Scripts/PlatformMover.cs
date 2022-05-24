using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    #region Variables
    public bool isUpDownPlatform;
    public bool isLeftRightPlatform;
    public enum MoveDirections { left,right,up,down};
    public MoveDirections nowDirection;
    public float leftLimiter;
    public float rightLimiter;
    public float upLimiter;
    public float downLimiter;
    public float platformSpeed;
    public Transform platformTransform;
    public Rigidbody rb;
    #endregion
    void Start()
    {
    }
    private void FixedUpdate()
    {
        MoveObject(nowDirection, platformTransform);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody gameObjectRB = other.GetComponent<Rigidbody>();
            MoveObject(nowDirection, gameObjectRB.transform);
        }
    }
    private void OnTriggerExit(Collider other)
    {
    }
    public void MoveObject(MoveDirections value,Transform gameObjectTransform)
    {
        switch (value)
        {
            case MoveDirections.left:
                gameObjectTransform.position += Vector3.left * platformSpeed*Time.fixedDeltaTime;
                break;
            case MoveDirections.right:
                gameObjectTransform.position += Vector3.right * platformSpeed * Time.fixedDeltaTime;
                break;
            case MoveDirections.up:
                gameObjectTransform.position += Vector3.up * platformSpeed * Time.fixedDeltaTime;
                break;
            case MoveDirections.down:
                gameObjectTransform.position += Vector3.down * platformSpeed * Time.fixedDeltaTime;
                break;
        }
        if (isLeftRightPlatform)
        {
            if (gameObjectTransform.position.x <= leftLimiter)
            {
                nowDirection = MoveDirections.right;
            }
            if (gameObjectTransform.position.x >= rightLimiter)
            {
                nowDirection = MoveDirections.left;
            }
        }
        if (isUpDownPlatform)
        {
            if (gameObjectTransform.position.y >= upLimiter)
            {
                nowDirection = MoveDirections.down;
            }
            if (gameObjectTransform.position.y <= downLimiter)
            {
                nowDirection = MoveDirections.up;
            }
        }
    }
}
