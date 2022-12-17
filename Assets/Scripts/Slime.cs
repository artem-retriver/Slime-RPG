using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slime : MonoBehaviour
{
    public AnimationController controller;
    public GameManager gameManager;
    public Bullet[] bulletPrefabs;
    public Slider slider;

    public List<Bullet> activeBullet = new();

    public int speedSlime = 5;
    public float spd = 1;
    public int atk = 1;
    public int hp = 1;

    public bool isBattle = false;
    public bool isEnemy = false;
    public bool isAlive = true;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    private void Update()
    {
        slider.value = hp;

        for (int i = 0; i < activeBullet.Count; i++)
        {
            if (activeBullet[i].transform.position.x >= transform.position.x + 6)
            {
                Destroy(activeBullet[i].gameObject);
                activeBullet.RemoveAt(i);
            }
        }

        for (int i = 0; i < activeBullet.Count; i++)
        {
            if (activeBullet[i].isBullet == true)
            {
                gameManager.activeEnemy[i].hp -= atk;
                Destroy(activeBullet[i].gameObject);
                activeBullet.RemoveAt(i);
            }
        }

        if(isBattle == true)
        {
            UnMoveCharacter();
        }
        else
        {
            MoveCharacter();
        }

        if(isEnemy == false)
        {
            UnAttackSlime();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Enemy _))
        {
            isEnemy = true;
            AttackSlime();
        }
    }

    public void Battle()
    {
        Vector3 posBullet = new(0, 0.2f, 0);
        Bullet spawn = Instantiate(bulletPrefabs[0], posBullet + transform.position, transform.rotation);
        activeBullet.Add(spawn);
        isBattle = true;
    }

    public void MoveCharacter()
    {
        controller.SetWalkTrigger();
        rb.velocity = Vector3.right * speedSlime;
    }

    public void AttackSlime()
    {
        InvokeRepeating(nameof(Attack), spd, spd);
        controller.SetDamageTrigger();
    }

    public void UnAttackSlime()
    {
        CancelInvoke(nameof(Attack));
    }

    public void Attack()
    {
        hp -= gameManager.activeEnemy[0].atk;
    }

    public void UnMoveCharacter()
    {
        controller.SetAttackTrigger();
        rb.velocity = Vector3.right * 0;
    }

    public void CancelInvokeBattle()
    {
        CancelInvoke(nameof(Battle));
    }

    public void InvokeBattle()
    {
        InvokeRepeating(nameof(Battle), 3.5f, spd);
        
    }
}
