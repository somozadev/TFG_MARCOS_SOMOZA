using System.Collections;
using UnityEngine;
interface IAnimable
{   
    Vector3 finalScale{get;set;}
    float duration {get;set;}
    void Activate();
    void Deactivate();
    IEnumerator LerpAnim();
}