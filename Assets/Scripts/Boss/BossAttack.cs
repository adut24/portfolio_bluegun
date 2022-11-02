using System.Collections;
using System;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    private Coroutine phaseCoroutine = null;
    public delegate IEnumerator AttackMethod();
    public GameObject[,] attackPrefabs = new GameObject[3,3];
    private int currentPhase;
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
        phaseCoroutine = StartCoroutine(FirstPhase());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public IEnumerator FirstPhase()
    {
        while (true)
        {
            Debug.Log("Hello");
            Instantiate(attackPrefabs[0, 0]);
            yield return new WaitForSeconds(4);

        }
    }
}
