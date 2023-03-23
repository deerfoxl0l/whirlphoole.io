using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAbsorbable
{
    void Absorb(EventParameters param);
    IEnumerator Absorbing(EventParameters param);
    void AbsorbStop();
    IEnumerator StopAbsorbing();
}
