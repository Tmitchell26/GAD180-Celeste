using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int health;
    public int enemyDamage;
    public Animator animator;
    public float seconds; 

    // function to simulate the player taking damage
    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            animator.SetBool("isDead", true);
            SetAllCollidersStatus(false);
            Destroy(this.gameObject,seconds);
        }
    }

    public void SetAllCollidersStatus(bool active)
    {
        foreach (Collider c in GetComponents<Collider>())
        {
            c.enabled = active;
        }
    }
}
