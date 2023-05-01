using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtributeManager : MonoBehaviour
{
    public int health;
    public int atack;
    public Animator animator;

    public void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            animator.SetTrigger("IsDead");
        }
    }
}
