using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debuff : MonoBehaviour
{
    public Weapon.dotEffect dot;
    public Enemy enemy;
	[HideInInspector]public bool isDestroyed = true;
    // Start is called before the first frame update
    void Start()
    {
		isDestroyed = false;
        if (dot.name != null)
            gameObject.name = dot.name;
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
        Component[] debuffList = enemy.gameObject.GetComponentsInChildren(typeof(Debuff));
        bool debuffedStill = false;
        foreach (Debuff d in debuffList)
        {
            if (d != this)
            {
                debuffedStill = true;
                break;
            }
        }
        if (debuffedStill == false)
        {
            enemy.GetComponent<SpriteRenderer>().color = Color.white;
            enemy.nextColor = Color.white;
        }

        enemy.moveSpeed = enemy.baseSpeed;
		isDestroyed = true;
        Destroy(gameObject);
    }
}
