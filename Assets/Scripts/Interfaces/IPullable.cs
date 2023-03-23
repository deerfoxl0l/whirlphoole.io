using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPullable 
{
    void Pull(Transform target);
    IEnumerator PullingProp();
    IEnumerator PullingPropAnchor(Transform target);

    void PullStop();
}
