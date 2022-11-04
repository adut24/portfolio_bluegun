using System.Collections;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class BossAttack : MonoBehaviour
{
    private Coroutine phaseCoroutine = null;
    public delegate IEnumerator AttackMethod();
    public GameObject[,] attackPrefabs = new GameObject[3,3];
    public int currentPhase = 0;
    public float attackPatternDelay = 5.0f;
    [SerializeField]
    public GameObject phaseOneAttackOne;
    public GameObject phaseOneAttackTwp;
    public GameObject phaseOneAttackThree;
    public GameObject phaseTwoAttackOne;
    public GameObject phaseTwoAttackTwo;
    public GameObject phaseTwoAttackThree;
    public GameObject phaseThreeAttackOne;
    public GameObject phaseThreeAttackTwo;
    public GameObject phaseThreeAttackThree;

    // Start is called before the first frame update
    void Start()
    {
        //Set attack prefabs into array for easy attack pattern randomization
        attackPrefabs[0, 0] = phaseOneAttackOne;
        attackPrefabs[0, 1] = phaseOneAttackTwp;
        attackPrefabs[0, 2] = phaseOneAttackThree;
        attackPrefabs[1, 0] = phaseTwoAttackOne;
        attackPrefabs[1, 1] = phaseTwoAttackTwo;
        attackPrefabs[1, 2] = phaseTwoAttackThree;
        attackPrefabs[2, 0] = phaseThreeAttackOne;
        attackPrefabs[2, 1] = phaseThreeAttackTwo;
        attackPrefabs[2, 2] = phaseThreeAttackThree;
        phaseCoroutine = StartCoroutine(AttackPattern());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public IEnumerator AttackPattern()
    {
        int i = 0;
        int id = 0;
        int lastAttack = 0;
        while (true)
        {
            lastAttack = (lastAttack + id) % 3;
            id = UnityEngine.Random.Range(1, 2);
            Attack attack = Instantiate(attackPrefabs[0, (lastAttack + id) % 3]).GetComponent<Attack>();
            attack.currentPhase = currentPhase;
            yield return new WaitForSeconds(attackPatternDelay);
            i++;
            switch (currentPhase)
            {
                case 0:
                    attackPatternDelay = 4.0f;
                    break;
                case 1:
                    attackPatternDelay = 3.5f;
                    break;
                case 2:
                    attackPatternDelay = 3.0f;
                    break;
            }
            GameObject.Find("PhaseIndicator").GetComponent<TextMeshProUGUI>().text = "Phase " + (currentPhase + 1);
        }
    }
}
