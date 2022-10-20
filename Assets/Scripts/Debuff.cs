using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debuff : MonoBehaviour
{
    public Weapon.dotEffect dot;
    public Enemy enemy;
    // Start is called before the first frame update
    void Start()
    {
         Coroutine dotRoutine = StartCoroutine(DamageOverTime(enemy));
    }

    private IEnumerator DamageOverTime(Enemy enemy)
    {
        int ticks = dot.ticks;

        enemy.nextColor = dot.color;
       
        while (ticks > 0 && enemy.alive == true)
        {
            enemy.moveSpeed = enemy.baseSpeed * (1 - dot.slowValue);
            yield return new WaitForSeconds(dot.delay);
            enemy.TakeDamage(dot.damage);
            --ticks;
        }
        enemy.GetComponent<SpriteRenderer>().color = enemy.nextColor;
        enemy.moveSpeed = enemy.baseSpeed;
        Destroy(gameObject);
    }
}
