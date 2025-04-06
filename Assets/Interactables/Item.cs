using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ScriptableItem scrItem;
    private void Start()
    {
        transform.parent = InteractableManager.instance.HoldingPositions[scrItem.heldType].parent;
        StartCoroutine(MoveToCorrectPosition());
    }

    public void Interact()
    {
        GameObject go = Instantiate(scrItem.ObjectPrefab);
        go.transform.SetPositionAndRotation(InteractableManager.instance.dropPosition.position, InteractableManager.instance.dropPosition.rotation);
        go.GetComponent<Rigidbody>().AddForce(PlayerController.instance.transform.forward * 500f);
        go.GetComponentInChildren<BaseInteractable>().DelayBeingUseable();
        Destroy(this.gameObject);
    }

    public void Use()
    {
        PlayerController.instance.playerItemUser.UseItem(scrItem.itemType);
    }

    IEnumerator MoveToCorrectPosition()
    {
        Vector3 initialPosition = transform.localPosition;
        Quaternion initialRotation = transform.localRotation;
        float t = 0.0f;
        while (t < 1.0f)
        {
            transform.localPosition = Vector3.Lerp(initialPosition, InteractableManager.instance.HoldingPositions[scrItem.heldType].localPosition, t);
            transform.localRotation = Quaternion.Slerp(initialRotation, InteractableManager.instance.HoldingPositions[scrItem.heldType].localRotation, t);
            t += Time.deltaTime* InteractableManager.instance.grabSpeed;
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }
}
