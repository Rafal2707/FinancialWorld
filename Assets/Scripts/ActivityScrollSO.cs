using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ActivityScrollSO : ScriptableObject
{
    public Transform prefab;
    public int number;
    public string descriptionPL;
    public string descriptionENG;
    public string descriptionDK;
    public string descriptionFIN;
    public bool isCentralBankActivity;
}
