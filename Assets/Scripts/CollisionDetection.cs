using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public PlayerCombat pc;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && pc.isAtack)
        {
            other.GetComponent<Animator>().SetTrigger("GetHit");
            var target = other.GetComponent<AtributeManager>();
            if (target != null)
            {
                target.TakeDamage(target.atack);
            }
        }
    }
}
