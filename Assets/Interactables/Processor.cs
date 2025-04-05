using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Processor : BaseInteractable
{

    public List<ScriptableRecipe> recipes;

    public float working = 0.0f;
    public ScriptableRecipe currentRecipe;
    public Transform outputTransform;
    public float scaleFreq = 1.0f;
    public AudioSource audioSrc;
    public ParticleSystem particleSys;

    public override void Start()
    {
        base.Start();
        particleSys.Pause();
    }

    void Update()
    {
        if (working > 0.0f)
        {
            working -= Time.deltaTime;
            transform.localScale = new Vector3(Mathf.Sin(working*scaleFreq)*0.1f+0.95f, Mathf.Cos(working * scaleFreq) *0.1f + 0.95f, Mathf.Sin(working * scaleFreq) * 0.1f + 0.95f);
            if (working < 0.0f)
            {
                GameObject go = Instantiate(currentRecipe.Output.ObjectPrefab);
                go.transform.position = outputTransform.position;
                go.transform.rotation = outputTransform.rotation;
                currentRecipe = null;
                audioSrc.pitch = Random.Range(0.97f, 1.03f);
                audioSrc.Play();
                particleSys.Stop();
            }
        }
        else
        {
            transform.localScale = Vector3.one;
        }
    }

    public override void Interact()
    {
        if (InteractableManager.instance.heldItem == null)
        {
            return;
        }

        for (int i = 0; i < recipes.Count; i++)
        {
            for (int k = 0; k < recipes[i].Inputs.Count; k++)
            {
                if (InteractableManager.instance.heldItem.scrItem.itemType == recipes[i].Inputs[k])
                {
                    currentRecipe = recipes[i];
                    working = recipes[i].workTime;
                    particleSys.Play();
                    Destroy(InteractableManager.instance.heldItem.gameObject);
                    InteractableManager.instance.heldItem = null;
                    return;
                }
            }
            
        }

        InteractableManager.instance.heldItem.Interact();
    }
}
