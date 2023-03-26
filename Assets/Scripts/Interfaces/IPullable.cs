using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPullable 
{
    void Pull(Transform target);
    IEnumerator Pulling();
    IEnumerator PullingAnchor(Transform target);

    void PullStop();
}
