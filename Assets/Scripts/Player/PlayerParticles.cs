using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticles : MonoBehaviour
{
    [SerializeField] ParticleSystem getHit;
    [SerializeField] ParticleSystem getHeal;
    [SerializeField] ParticleSystem getCoin;
    [SerializeField] ParticleSystem getLvlUp;


    public void GetHitParticle() => getHit.Play();
    public void GetHealParticle() => getHeal.Play();
    public void GetCoinParticle() => getCoin.Play();
    public void GetLvlUpParticle() => getLvlUp.Play();
}
