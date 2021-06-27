using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioSource source;

    public AudioClip enemyShootSFX, metalImpactSFX, enemyExplodeSFX, mineExplodeSFX;


    public void PlayEnemyShootSFX(){
        source.PlayOneShot(enemyShootSFX);
    }

    public void PlayMetalImpactSFX(){
        source.PlayOneShot(metalImpactSFX);
    }

    public void PlayEnemyExplodeSFX(){
        source.PlayOneShot(enemyExplodeSFX);
    }

    public void PlayMineExplodeSFX(){
        source.PlayOneShot(mineExplodeSFX);
    }
}
