using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public Bullet[] bulletPrefabs;

    public List<Bullet> activeBullet = new List<Bullet>();

    public int speedSlime = 5;
    public int spd = 1;
    public int atk = 1;
    public int hp = 1;
    public int posDelete = 0;

    public bool isBattle = false;
    public bool isBulletOn = false;
    public bool isEnemy = false;
    public bool isAlive = true;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(WaitShoot());
    }

    private void Update()
    {
        for (int i = 0; i < activeBullet.Count; i++)
        {
            if (activeBullet[i].transform.position.x >= transform.position.x + 7)
            {
                Destroy(activeBullet[i]);
                activeBullet.RemoveAt(i);
            }

            if (activeBullet[i].isBullet == true)
            {
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
        rb.velocity = Vector3.right * speedSlime;
    }

    public void UnMoveCharacter()
    {
        rb.velocity = Vector3.right * 0;
    }

    public IEnumerator WaitShoot()
    {
        yield return new WaitForSeconds(1.8f);
        InvokeRepeating(nameof(Battle), spd, spd);
        Battle();
    }
}
