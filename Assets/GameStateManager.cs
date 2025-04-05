using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    bool started = false;

    BackgroundManager bm;
    [SerializeField] private float bgSpeedTarget = 0.0f;
    [SerializeField] private float bgAcc = 0.05f;
    
    [SerializeField] private Light mainLight;

    [SerializeField] private ParticleSystem engineParticleSystem;

    float depth = 0f;


    [SerializeField] private GameObject debrisDropper;
    [SerializeField] private GameObject debrisPrefab;
    ParticleSystem[] debrisDropperParticleSystems;

    [SerializeField] private List<Vector3> debrisPositions = new List<Vector3>();

    int droppedDebris = 0;

    void Start()
    {
        bm = GetComponent<BackgroundManager>();
        engineParticleSystem.Stop();
        debrisDropperParticleSystems = debrisDropper.GetComponentsInChildren<ParticleSystem>();
    }
    void Update()
    {
        depth += bm.speed * Time.deltaTime;
        if (!started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                started = true;
                bgSpeedTarget = 2.0f;
                engineParticleSystem.Play();
                
            }
        }
        if (depth < 200f)
        {
            mainLight.intensity = Mathf.Max(2f - depth * 0.05f, 0.0f);
        }
        bm.speed = Mathf.MoveTowards(bm.speed, bgSpeedTarget, bgAcc * Time.deltaTime);

        if (depth > (droppedDebris+1) * 15f)
        {
            DropDebris();
            droppedDebris++;
        }
    }


    void DropDebris()
    {
        Vector3 pos = PickRandomDebrisPosition();
        debrisDropper.transform.position = pos;
        foreach (ParticleSystem ps in debrisDropperParticleSystems)
        {
            ps.Play();
        }

        StartCoroutine(DelayedDebrisDrop(pos));

    }
    IEnumerator DelayedDebrisDrop(Vector3 pos)
    { 
    yield return new WaitForSeconds(5f);
    GameObject go = Instantiate(debrisPrefab);
    go.transform.position = pos;
        yield return null;
    }
    Vector3 PickRandomDebrisPosition()
    {
        int rnd = Random.Range(0, debrisPositions.Count - 1);
        return debrisPositions[rnd];
    }


    public void ResetGameSpeed()
    {
        Time.timeScale = 1.0f;
    }
}
