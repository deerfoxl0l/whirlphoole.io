using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropStaff : MonoBehaviour
{
    public PropLifetime PropLifetime;
    public PropHelper PropHelper;

    public void Start()
    {
        PropLifetime.Initialize();
        PropHelper.Initialize();
    }
}
