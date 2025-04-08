using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Engine : Destructible
{
    public static Engine instance;
    public List<Enemy> enemies;

    [SerializeField] private float enemyDist = 1.5f;
    [SerializeField] private float hp = 100f;
    //[SerializeField] private float maxHealth = 100f;
    [SerializeField] private float dmg = 4f;


    [SerializeField] private TMP_Text hpText;
    [SerializeField] private RectTransform hpBar;
    [SerializeField] private Image hpBarImage;
    [SerializeField] private Gradient grad;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else {
            Destroy(this.gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        bool tookDamage = false;
        for (int i = 0; i< enemies.Count; i++)
        {
            if (Vector3.Distance(transform.position, enemies[i].transform.position) < enemyDist)
            {
                hp -= Time.deltaTime * dmg;
                tookDamage = true;
            }
        }

        if (hp < 0f)
        {
            GameStateManager.instance.LoseGame();
        }

        if (tookDamage)
        {
            hpBar.sizeDelta = new Vector2(100f* hp / maxHealth,15.0f);
            hpBarImage.color = grad.Evaluate(hp / maxHealth);
            hpText.text = Mathf.CeilToInt(hp).ToString();
        }
    }

    new public void TakeDamage(int dmg)
    {
        hp -= dmg;

        //wrench repair goes upwards instead, don't let it heal infinite
        if (hp > maxHealth)
        {
            hp = maxHealth;
        }

        hpBar.sizeDelta = new Vector2(100f * hp / maxHealth, 15.0f);
        hpBarImage.color = grad.Evaluate(hp / maxHealth);
        hpText.text = Mathf.CeilToInt(hp).ToString();
    }

    public void TakeDamage(float dmg)
    {
        hp -= dmg;

        //wrench repair goes upwards instead, don't let it heal infinite
        if (hp > maxHealth)
        {
            hp = maxHealth;
        }

        hpBar.sizeDelta = new Vector2(100f * hp / maxHealth, 15.0f);
        hpBarImage.color = grad.Evaluate(hp / maxHealth);
        hpText.text = Mathf.CeilToInt(hp).ToString();
    }

    private void Start()
    {
        hpBar.sizeDelta = new Vector2(100f * hp / maxHealth, 15.0f);
        hpBarImage.color = grad.Evaluate(hp / maxHealth);
        hpText.text = Mathf.CeilToInt(hp).ToString();
    }
}
