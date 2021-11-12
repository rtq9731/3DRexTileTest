using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileEffectPool : MonoBehaviour
{
    [SerializeField] GameObject missileImpactEffectPrefab = null;
    Queue<ParticleSystem> missileImpactEffectPool = new Queue<ParticleSystem>();

    public ParticleSystem PlayEffectOnTile(TileScript tile)
    {
        ParticleSystem result = null;
        
        if (missileImpactEffectPool.Count >= 1)
        {
            if(missileImpactEffectPool.Peek().gameObject.activeSelf)
            {
                result = Instantiate(missileImpactEffectPrefab, tile.transform).GetComponent<ParticleSystem>();
                missileImpactEffectPool.Enqueue(result);
            }
            else
            {
                result = missileImpactEffectPool.Dequeue();
                missileImpactEffectPool.Enqueue(result);
            }
        }
        else
        {
            result = Instantiate(missileImpactEffectPrefab, tile.transform).GetComponent<ParticleSystem>();
            missileImpactEffectPool.Enqueue(result);
        }

        return result;
    }
}
