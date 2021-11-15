using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileEffectPool : MonoBehaviour
{
    [SerializeField] GameObject missileImpactEffectPrefab = null;

    float particleLifeTime = 5f;

    Queue<ParticleSystem> missileImpactEffectPool = new Queue<ParticleSystem>();

    Dictionary<ParticleSystem, float> particleTimers = new Dictionary<ParticleSystem, float>();

    private void Update() // 만약 만들어진 시간 이후 particleLifeTime 경과한 파티클들은 전부 게임 오브젝트를 꺼준다
    {
        if (particleTimers.Count > 0)
        {
            foreach (var item in particleTimers)
            {
                if (!item.Key.gameObject.activeSelf)
                    continue;

                if (item.Value + particleLifeTime < Time.time)
                {
                    item.Key.gameObject.SetActive(false);
                }
            }
        }
    }

    public ParticleSystem PlayEffectOnTile(TileScript tile)
    {
        ParticleSystem result = null;
        if (missileImpactEffectPool.Count >= 1)
        {
            if(missileImpactEffectPool.Peek().gameObject.activeSelf)
            {
                result = Instantiate(missileImpactEffectPrefab, tile.transform).GetComponent<ParticleSystem>();
            }
            else
            {
                result = missileImpactEffectPool.Dequeue();
            }
        }
        else
        {
            result = Instantiate(missileImpactEffectPrefab, tile.transform).GetComponent<ParticleSystem>();
        }

        if(result != null)
        {
            particleTimers.Add(result, Time.time);
            missileImpactEffectPool.Enqueue(result);
        }

        return result;
    }
}
