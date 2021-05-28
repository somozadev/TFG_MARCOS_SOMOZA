using System.Collections;
using UnityEngine;

interface IAnimable
{
    [SerializeField] bool Open { get; set; }
    [SerializeField] Vector3 FinalScale { get; set; }
    [SerializeField] float Duration { get; set; }
    void Activate();
    void Deactivate();
    IEnumerator LerpAnim();
}

interface IDamageable
{
    void RecieveDamage(float cuantity);
}
interface IDamager
{
    void MakeDamage();}

