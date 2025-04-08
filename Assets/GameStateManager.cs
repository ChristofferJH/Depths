using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance;


    private int _gold = 1;
    public int gold
    {
        get => _gold;
        set
        {
            _gold = value;
            goldText.text="Gold: "+gold.ToString();
        }
    }
    [SerializeField] TMP_Text goldText;

    bool started = false;

    [SerializeField] private TMP_Text depthCounter;
    BackgroundManager bm;
    BorisManager borisM;
    [SerializeField] private float bgSpeedTarget = 0.0f;
    [SerializeField] private float bgAcc = 0.05f;
    
    [SerializeField] private Light mainLight;

    [SerializeField] private ParticleSystem engineParticleSystem;

    public float depth = 0;
    public bool tutorialDone = false;

    public float tutorialDepth=50f;
    public int tutorialStage = 0;
    [SerializeField] private GameObject debrisDropper;
    [SerializeField] private GameObject debrisPrefab;
    ParticleSystem[] debrisDropperParticleSystems;

    [SerializeField] private List<Vector3> debrisPositions = new List<Vector3>();
    [SerializeField] private List<Vector3> enemyPositions = new List<Vector3>();

    [SerializeField] private List<Light> lanterns;

    [SerializeField] private GameObject[] slimePrefabs;

    public RectTransform contract;


    public float debrisDropRate=17f;
    public float slimeDropRate=20f;
    public float slimeDrop = 19f;
    public float debrisDrop = 3f;
    public int slimeType = 0;


    public enum GameEvent { debris, enemy, showDepth, upgradeSlimes, moreDebris, moreSlimes, lessSlimes }

    public struct GameEventPoint {
        public GameEventPoint(float d, GameEvent ge)
        {
            depth = d;
            gameEvent = ge;
        }
        public float depth;
        public GameEvent gameEvent;
    }

    public List<GameEventPoint> depthTimedGameEvents = new List<GameEventPoint>();

    public AudioSource goldSound;

    int droppedDebris = 0;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        depthTimedGameEvents.Add(new GameEventPoint(51, GameEvent.showDepth));
        depthTimedGameEvents.Add(new GameEventPoint(30, GameEvent.moreDebris));
        depthTimedGameEvents.Add(new GameEventPoint(75, GameEvent.moreDebris));
        depthTimedGameEvents.Add(new GameEventPoint(70, GameEvent.moreSlimes));
        depthTimedGameEvents.Add(new GameEventPoint(110, GameEvent.moreDebris));
        depthTimedGameEvents.Add(new GameEventPoint(210, GameEvent.moreDebris));
        depthTimedGameEvents.Add(new GameEventPoint(200, GameEvent.moreSlimes));
        depthTimedGameEvents.Add(new GameEventPoint(310, GameEvent.moreDebris));
        depthTimedGameEvents.Add(new GameEventPoint(300, GameEvent.moreSlimes));
        depthTimedGameEvents.Add(new GameEventPoint(390, GameEvent.lessSlimes));
        depthTimedGameEvents.Add(new GameEventPoint(400, GameEvent.upgradeSlimes));
        depthTimedGameEvents.Add(new GameEventPoint(410, GameEvent.moreDebris));
        depthTimedGameEvents.Add(new GameEventPoint(510, GameEvent.moreDebris));
        depthTimedGameEvents.Add(new GameEventPoint(590, GameEvent.lessSlimes));
        depthTimedGameEvents.Add(new GameEventPoint(600, GameEvent.upgradeSlimes));
        depthTimedGameEvents.Add(new GameEventPoint(650, GameEvent.moreSlimes));
        depthTimedGameEvents.Add(new GameEventPoint(650, GameEvent.moreSlimes));
        depthTimedGameEvents.Add(new GameEventPoint(700, GameEvent.moreSlimes));
        depthTimedGameEvents.Add(new GameEventPoint(720, GameEvent.moreDebris));
        depthTimedGameEvents.Add(new GameEventPoint(750, GameEvent.moreSlimes));
        depthTimedGameEvents.Add(new GameEventPoint(770, GameEvent.moreDebris));
        depthTimedGameEvents.Add(new GameEventPoint(790, GameEvent.moreSlimes));
        depthTimedGameEvents.Add(new GameEventPoint(820, GameEvent.moreDebris));
        depthTimedGameEvents.Add(new GameEventPoint(830, GameEvent.moreSlimes));

        borisM = GetComponent<BorisManager>();
      
        bm = GetComponent<BackgroundManager>();
        engineParticleSystem.Stop();
        debrisDropperParticleSystems = debrisDropper.GetComponentsInChildren<ParticleSystem>();
        goldSound = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
        depth += bm.speed * Time.deltaTime;

        if (!tutorialDone)
        {
            depth = Mathf.Min(depth, tutorialDepth);
        }
        else
        {
            depthCounter.text = Mathf.FloorToInt(depth).ToString();
        }

        if (!started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                RemoveContract();
                started = true;
                bgSpeedTarget = 2.0f;
                engineParticleSystem.Play();



            }
        }

        if (tutorialDone)
        {
            slimeDrop -= Time.deltaTime;
            debrisDrop -= Time.deltaTime;
            if (debrisDrop < 0)
            {
                DropDebris(Drop.debris);
                debrisDrop = debrisDropRate;
            }
            if (slimeDrop < 0)
            {
                if (slimeType == 0)
                {
                    DropDebris(Drop.slime0);
                }
                if (slimeType == 1)
                {
                    DropDebris(Drop.slime1);
                }
                if (slimeType == 2)
                {
                    DropDebris(Drop.slime2);
                }
                slimeDrop = slimeDropRate;
            }
        }

        if (depth < 200f)
        {
            mainLight.intensity = Mathf.Max(2f - depth * 0.2f, 0.1f);
        }
        bm.speed = Mathf.MoveTowards(bm.speed, bgSpeedTarget, bgAcc * Time.deltaTime);



        if (depth > 55f)
        {
            foreach (Light l in lanterns)
            {
                l.enabled = true;
            }
        }


        for (int i = 0; i < depthTimedGameEvents.Count; i++)
        {
            if (depthTimedGameEvents[i].depth < depth)
            {
                ProcessGameEvent(depthTimedGameEvents[i].gameEvent);
                depthTimedGameEvents.RemoveAt(i);
            }
        }
    }

    public enum Drop { debris, slime0, slime1, slime2 };
    public void DropDebris(Drop drop)
    {
        if (drop == Drop.debris)
        {
            Vector3 pos = PickRandomDebrisPosition();
            debrisDropper.transform.position = pos;
            foreach (ParticleSystem ps in debrisDropperParticleSystems)
            {
                ps.Play();
            }

            StartCoroutine(DelayedDebrisDrop(pos, drop));
        }
        else {
            Vector3 pos = PickRandomEnemyPosition();
            debrisDropper.transform.position = pos;
            foreach (ParticleSystem ps in debrisDropperParticleSystems)
            {
                ps.Play();
            }

            StartCoroutine(DelayedDebrisDrop(pos, drop));
        }
    }
    IEnumerator DelayedDebrisDrop(Vector3 pos, Drop drop)
    {
        yield return new WaitForSeconds(4f);
    switch (drop)
    {
            case Drop.debris:
                GameObject go = Instantiate(debrisPrefab);
                go.transform.position = pos;
                break;

            case Drop.slime0:
                GameObject go1 = Instantiate(slimePrefabs[0]);
                go1.transform.position = pos;
                break;

            case Drop.slime1:
                GameObject go2 = Instantiate(slimePrefabs[1]);
                go2.transform.position = pos;
                break;

            case Drop.slime2:
                GameObject go3 = Instantiate(slimePrefabs[2]);
                go3.transform.position = pos;
                break;
        }
        yield return null;
    }
    Vector3 PickRandomDebrisPosition()
    {
        int rnd = Random.Range(0, debrisPositions.Count - 1);
        return debrisPositions[rnd];
    }

    Vector3 PickRandomEnemyPosition()
    {
        int rnd = Random.Range(0, enemyPositions.Count - 1);
        return enemyPositions[rnd];
    }

    
    public void ProcessGameEvent(GameEvent gEvent)
    {
        switch (gEvent)
        {
            case GameEvent.debris:
                DropDebris(Drop.debris);
                break;

            case GameEvent.showDepth:
                depthCounter.gameObject.transform.parent.gameObject.SetActive(true);
                break;

            case GameEvent.moreDebris:
                debrisDropRate -= 1f;
                break;

            case GameEvent.moreSlimes:
                slimeDropRate -= 1f;
                break;

            case GameEvent.lessSlimes:
                slimeDropRate += 1f;
                break;

            case GameEvent.upgradeSlimes:
                slimeType+=1;
                break;

            default:
                break;
        }

    }

    public IEnumerator WaitAndDisplayBoris(string str, float time)
    {
        yield return new WaitForSeconds(time);
        borisM.SetBorisCanvas(str);
        yield return null;
    }

    public void ResetGameSpeed()
    {
        Time.timeScale = 1.0f;
    }


    public void LoseGame()
    {
        float hs=PlayerPrefs.GetFloat("highscore", 0f);
        if (depth > hs)
        {
            PlayerPrefs.SetFloat("highscore", depth);
        }
        PlayerPrefs.Save();
        StartCoroutine(WaitAndDisplayBoris("OH NO!!! This is coming out of your paycheck, for sure.",0.2f));
        StartCoroutine(LoseGameCoroutine());
    }

    IEnumerator LoseGameCoroutine()
    {
        for (int i = 0; i < 1000; i++)
        {
            bm.speed += 2f * Time.deltaTime;
            Camera.main.transform.position += new Vector3(Mathf.PerlinNoise(Time.time * 73.9713f, Time.time * 71.931f) -0.5f, Mathf.PerlinNoise(Time.time * 82.9713f, Time.time * 91.931f) -0.5f, Mathf.PerlinNoise(Time.time * 93.9713f, Time.time * 71.931f) -0.5f)*0.0000001f*bm.speed;
            yield return new WaitForEndOfFrame();
        }
        SceneManager.LoadScene(0);
        yield return null;
    }


    public void RemoveContract()
    {
        StartCoroutine(RemoveSlowlyContract());
        StartCoroutine(WaitAndDisplayBoris("You must be the new guy! Press [Space]", 5f));

    }

    IEnumerator RemoveSlowlyContract()
    {
        for (int i = 0; i < 1000; i++)
        {
            contract.transform.Translate(Vector3.down * Time.deltaTime * 200f);
            contract.transform.localScale = contract.transform.localScale * 0.98f;
            contract.transform.Rotate(Vector3.forward * Time.deltaTime * 900f);
            yield return new WaitForEndOfFrame();
        }

        Destroy(contract.gameObject);
        yield return null;
    }










    public void PlayGoldSound()
    {
        goldSound.Play();
    }
}
