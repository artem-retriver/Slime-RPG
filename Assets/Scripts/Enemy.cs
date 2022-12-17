using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public AnimationController controller;
    public Slider slider;
    public Rigidbody rb;
    public int speed;
    public int hp;
    public int atk;
    public float spd;

    public bool isDead;

    private void Start()
    {
        MoveEnemy();
    }

    private void Update()
    {
        slider.value = hp;
    }

    public static new T Instantiate<T>(T original, Vector3 position, Quaternion rotation) where T : Object
    {
        return (T)Instantiate((Object)original, position, rotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Slime _))
        {
            controller.SetAttackTrigger();
            UnMoveEnemy();
        }
    }

    public void MoveEnemy()
    {
        rb.velocity = Vector3.left * speed;
        Vector3 curRotation = new (0, -90, 0);
        transform.eulerAngles = curRotation;
    }

    public void UnMoveEnemy()
    {
        rb.velocity = Vector3.zero;
    }
}
