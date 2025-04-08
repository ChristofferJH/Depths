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

    public List<Behaviour> activeWhileWorking;


    public override void Start()
    {
        base.Start();
        particleSys.Pause();
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
    }

    void Update()
    {
        if (working > 0.0f)
        {
            working -= Time.deltaTime;
            if (working < 0.0f)
            {
                if (currentRecipe.Output != null)
                {
                    GameObject go = Instantiate(currentRecipe.Output.ObjectPrefab);
                    go.transform.position = outputTransform.position;
                    go.transform.rotation = outputTransform.rotation;

                }
                WeightManager.instance.weight -= 1;
                currentRecipe = null;
                audioSrc.pitch = Random.Range(0.97f, 1.03f);
                audioSrc.Play();
                particleSys.Stop();
                foreach (Behaviour b in activeWhileWorking)
                {
                    b.enabled = false;
                }
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
                    if (InteractableManager.instance.heldItem.scrItem.heldType == ScriptableItem.HeldType.overHead)
                    {
                        PlayerController.instance.anim.SetBool("Hold", false);
                    }
                    currentRecipe = recipes[i];
                    working = recipes[i].workTime;
                    particleSys.Play();
                    Destroy(InteractableManager.instance.heldItem.gameObject);
                    WeightManager.instance.weight += 1;
                    InteractableManager.instance.heldItem = null;
                    foreach (Behaviour b in activeWhileWorking)
                    {
                        b.enabled = true;
                    }
                    return;
                }
            }
            
        }

        InteractableManager.instance.heldItem.Interact();
    }
}
