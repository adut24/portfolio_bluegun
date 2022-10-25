using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Beer : Artifact
{
    [SerializeField]
    private ArtifactData beerData;
    private bool isInInv = false;
    private bool canUse = true;
    private bool timerIsRunning = false;
    private float timer;

    private PlayerHealth ph;
    private PlayerMovement pm;
    private Weapon weapon;
    private GameObject cooldown;

    public void Awake()
    {
        ph = GameObject.Find("Player").GetComponent<PlayerHealth>();
        pm = GameObject.Find("Player").GetComponent<PlayerMovement>();
        weapon = GameObject.FindWithTag("Weapon").GetComponent<Weapon>();
        cooldown = GameObject.FindGameObjectWithTag("ArtifactCooldown");
    }

    public override void Add()
    {
        isInInv = true;
    }

    public void Update()
    {
        if (isInInv && Input.GetKeyDown(KeyCode.R))
        {
            if (canUse)
            {
                canUse = false;
                ph.RegenerateLife(beerData.health);
                StartCoroutine(DamageCooldown());
                StartCoroutine(BeerCooldown());
                SliderCooldown();
            }
        }

        if (timerIsRunning)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                cooldown.GetComponent<ArtifactCooldown>().SetValue(timer / beerData.artifactActivation);
            }
            else
            {
                timer = 0;
                timerIsRunning = false;
            }
        }
    }

    public IEnumerator BeerCooldown()
    {
        yield return new WaitForSeconds(beerData.artifactActivation);
        canUse = true;
    }

    public IEnumerator DamageCooldown()
    {
        weapon.damage += beerData.damage;
        yield return new WaitForSeconds(10);
        weapon.damage -= beerData.damage;
        StartCoroutine(SlowCooldown());
    }

    public IEnumerator SlowCooldown()
    {
        pm.moveSpeed -= beerData.slow;
        yield return new WaitForSeconds(10);
        pm.moveSpeed += beerData.slow;
    }

    public void SliderCooldown()
    {
        cooldown.GetComponent<Slider>().value = 1;
        timer = 30;
        timerIsRunning = true;
    }

    public override void Remove()
    {
        isInInv = false;
    }
}
