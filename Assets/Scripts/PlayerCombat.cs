using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public static PlayerCombat Instance;
    public GameObject sword;
    public Animator animator;
    public bool isAtack = false;
    public bool swordEquip;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        swordEquip = sword.gameObject.activeInHierarchy;
        if (swordEquip)
        {
            if (Input.GetKeyDown(KeyCode.E) && !isAtack)
            {
                Attack();
            }
            else
            {
                animator.SetBool("IsAtack", isAtack);
            }
        }
    }

    private void Attack()
    {
        isAtack = true;
        animator.SetBool("IsAtack", isAtack);
        StartCoroutine(ResetAtack());
    }

    IEnumerator ResetAtack()
    {
        yield return new WaitForSeconds(0.5f);
        isAtack = false;
    }

    public void SwordEquip()
    {
        sword.SetActive(!swordEquip);
    }
}
