using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private int walkID;
    private int attackID;
    private int damageID;

    private void Awake()
    {
        attackID = Animator.StringToHash("Attack");
        walkID = Animator.StringToHash("Walk");
        damageID = Animator.StringToHash("Damage");
    }

    public void SetAttackTrigger()
    {
        SetTrigger(attackID);
    }

    public void SetWalkTrigger()
    {
        SetTrigger(walkID);
    }

    public void SetDamageTrigger()
    {
        SetTrigger(damageID);
    }

    public void SetTrigger(int id) => animator.SetTrigger(id);
}
