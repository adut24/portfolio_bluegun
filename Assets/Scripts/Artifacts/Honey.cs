using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Honey : Artifact
{
    [SerializeField]
    private ArtifactData honeyData;
    private bool isInInv = false;
    private bool canHeal = true;
    private bool timerIsRunning = false;
    private float timer;

    private PlayerHealth ph;
    private GameObject cooldown;

    public void Awake()
    {
        ph = GameObject.Find("Player").GetComponent<PlayerHealth>();
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
            if (canHeal)
            {
                canHeal = false;
                ph.RegenerateLife(honeyData.health);
                StartCoroutine(HoneyCooldown());
                SliderCooldown();
            }
        }

        if (timerIsRunning)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                cooldown.GetComponent<ArtifactCooldown>().SetValue(timer / honeyData.artifactActivation);
            }
            else
            {
                timer = 0;
                timerIsRunning = false;
            }
        }
    }

    public IEnumerator HoneyCooldown()
    {
        yield return new WaitForSeconds(honeyData.artifactActivation);
        canHeal = true;
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
