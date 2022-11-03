using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleAttack : Attack
{
    private BossWeapon weapon;
    // Start is called before the first frame update
    void Start()
    {
        weapon = GameObject.FindGameObjectWithTag("BossWeapon").GetComponent<BossWeapon>();
        StartCoroutine(Attack(currentPhase));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Attack(int phase)
    {
        int nbAttacks;
        float delay;

        switch (phase)
        {
            case 0:
                nbAttacks = 3;
                delay = 0.8f;
                break;
            case 1:
                nbAttacks = 5;
                delay = 0.7f;
                break;
            case 2:
                nbAttacks = 8;
                delay = 0.6f;
                break;
            default:
                nbAttacks = 1;
                delay = 1.0f;
                break;
        }
        for (int i = 0; i < nbAttacks; i++)
        {
            weapon.ShootWeapon();
            yield return new WaitForSeconds(delay);
        }
        Destroy(gameObject, weapon.projectileDuration + 1);
    }
}
