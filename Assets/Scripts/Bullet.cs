using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody rb;
    public int speed;

    public bool isBullet = false;

    private void Start()
    {
        MoveBullet();
    }

    public static new T Instantiate<T>(T original, Vector3 position, Quaternion rotation) where T : Object
    {
        return (T)Instantiate((Object)original, position, rotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Enemy _))
        {
            isBullet = true;
        }
    }

    public void MoveBullet()
    {
        rb.velocity = Vector3.right * speed;
        Vector3 curRotation = new (0, 0, 90);
        transform.eulerAngles = curRotation;
    }
}
