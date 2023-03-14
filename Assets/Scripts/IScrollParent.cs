using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public interface IScrollParent
{
    public Transform GetScrollFollowTransform();

    public void SetActivityScroll(ActivityScroll activityScroll);

    public ActivityScroll GetActivityScroll();


    public void ClearActivityScroll();

    public bool HasActivityScroll();

    public NetworkObject GetNetworkObject();

    public bool IsInDroppingArea();

    public void SetIsInDroppingArea(bool isInDroppingArea);
}
