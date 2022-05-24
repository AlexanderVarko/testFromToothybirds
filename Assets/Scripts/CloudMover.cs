using UnityEngine;

public class CloudMover : MonoBehaviour
{
    public float speed;
    void Update()
    {
        transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
        if(transform.position.x <= -Camera.main.scaledPixelWidth)
        {
            Destroy(gameObject);
        }
    }
}
