using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceDumpManager : MonoBehaviour
{
    [SerializeField]
    private int _consumeAmount = 1;
    [SerializeField]
    private float _consumeInterval = 1f;
    [SerializeField]
    private ResourceDemandManager _resourceDemandManager = null;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered!");
        if (other.tag != "Player") return;

        //get resource manager
        ResourceManager manager = other.GetComponent<Player>().resourceManager;
        //get all resources demanded
        var demands = _resourceDemandManager.GetDemands();
        //start coroutine for every resource
        foreach (var demand in demands)
        {
            StartCoroutine(ConsumeResource(demand.Key, manager));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Trigger exited!");
        if (other.tag != "Player") return;

        StopAllCoroutines();
    }

    private IEnumerator ConsumeResource(Resources type, ResourceManager manager)
    {
        while(CheckAvailability(type, manager))
        {
            uint resourceChange = _resourceDemandManager.GetResourceCount(type);
            resourceChange = (uint)Mathf.Clamp(resourceChange, 0, _consumeAmount);
            if (!DeductResource(ref resourceChange, type, manager)) break;
            if (!SatisfyDemand(resourceChange, type)) break;
            yield return new WaitForSeconds(_consumeInterval);
        }
    }

    private bool DeductResource(ref uint count, Resources type, ResourceManager manager)
    {
        uint resourceCount = manager.GetResourceCount(type);
        if(resourceCount == 0) return false;
        count = (uint)Mathf.Clamp(count, 0f, resourceCount);
        manager.SetResourceCount(resourceCount - count, type);
        return true;
    }

    private bool SatisfyDemand(uint count, Resources type)
    {
        uint resourceRequired = _resourceDemandManager.GetResourceCount(type);
        if (resourceRequired == 0) return false;
        _resourceDemandManager.SetResourceCount(resourceRequired - count, type);
        return true;
    }

    private bool CheckAvailability(Resources type, ResourceManager manager)
    {
        return manager.GetResourceCount(type) > 0;
    }
}
