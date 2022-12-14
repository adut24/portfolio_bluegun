using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossHealth : Enemy
{
    public Gradient gradur;
    public Image fill;
    public Slider slider;
    public int maxHealth;
    public BossAttack boss;

    private void Awake()
    {
        GameObject bossIndic = GameObject.Find("Canvas").transform.Find("BossIndicator").gameObject;
        GameObject phaseIndic = GameObject.Find("Canvas").transform.Find("PhaseIndicator").gameObject;
        bossIndic.SetActive(true);
        phaseIndic.SetActive(true);
        slider = bossIndic.transform.Find("Slider").GetComponent<Slider>();
        fill = slider.transform.Find("Fill Area").transform.Find("Fill").GetComponent<Image>();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        Destroy(GameObject.Find("Music Loop"));
        sprite = GetComponent<SpriteRenderer>();
        health = maxHealth;
        fill.color = gradur.Evaluate(1f);
    }

    protected override void Update()
    {

    }

    public override void TakeDamage(int damage)
    {
        if (_alive == true)
        {
            health -= damage;
            if ((float) (health) / (float) maxHealth < 0.34f)
                boss.currentPhase = 2;
            else if ((float) (health) / (float) maxHealth < 0.67f)
                boss.currentPhase = 1;
            if (health <= 0)
            {
                _alive = false;
                Destroy(gameObject, 0.5f);
                SceneManager.LoadScene("YouWon");
            }
            slider.normalizedValue = (float) health / (float) maxHealth;
            fill.color = gradur.Evaluate(slider.normalizedValue);
        }
    }

    protected override void Pathfinding()
    {}

    protected override void MoveRandom()
    {}

    protected override void DropItem(Vector2 position)
    {}

}
