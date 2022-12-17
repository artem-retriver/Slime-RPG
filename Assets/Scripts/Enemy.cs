using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody rb;
    public int speed;

    private void Start()
    {
        MoveEnemy();
    }

    public void MoveEnemy()
    {
        rb.velocity = Vector3.left * speed;
        Vector3 curRotation = new (0, -90, 0);
        transform.eulerAngles = curRotation;
    }
}
