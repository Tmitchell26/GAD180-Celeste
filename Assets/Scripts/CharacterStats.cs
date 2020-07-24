using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int health;
    public int playerDamage;

    // function to simulate the player taking damage
    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            PlayerManager.instance.KillPlayer();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyStats enemy = collision.collider.GetComponent<EnemyStats>();
        if (enemy != null)
        {
            foreach(ContactPoint2D point in collision.contacts)
            {
                if (point.normal.y >= 0.9f)
                {
                    enemy.TakeDamage(playerDamage);
                }
                else
                {
                    TakeDamage(enemy.gameObject.GetComponent<EnemyStats>().enemyDamage);
                }
            }
        }
    }
}
