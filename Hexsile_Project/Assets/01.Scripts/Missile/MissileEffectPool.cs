using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MissileEffectPool : MonoBehaviour
{
    [SerializeField] GameObject missileImpactEffectPrefab = null;
    [SerializeField] GameObject followMissileEffectPrefab = null;
    [SerializeField] Vector3 followEffectOriginPos = Vector3.zero;

    float tileHitEffectLifeTime = 5f;
    float missileFollowEffectLifeTime = 10f;

    Queue<ParticleSystem> missileImpactEffectPool = new Queue<ParticleSystem>();
    Queue<ParticleSystem> followMissileEffectPool = new Queue<ParticleSystem>();

    List<ParticleTimer> particleTimers = new List<ParticleTimer>();
    List<ParticleTimer> removeTimers = new List<ParticleTimer>();


    private void Update() // 만약 만들어진 시간 이후 particleLifeTime 경과한 파티클들은 전부 게임 오브젝트를 꺼준다
    {
        if (particleTimers.Count > 0)
        {
            foreach (var item in particleTimers)
            {
                if (!item.particle.gameObject.activeSelf)
                    continue;

                if (item.madeTime + item.lifeTime < Time.time)
                {
                    item.particle.gameObject.SetActive(false);
                    removeTimers.Add(item);
                }
            }

            if(removeTimers.Count > 0)
            {
                removeTimers.ForEach(x => particleTimers.Remove(x));
                removeTimers.Clear();
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
                result = Instantiate(missileImpactEffectPrefab, new Vector3(tile.transform.position.x, tile.transform.position.y + 0.005f, tile.transform.position.z), Quaternion.identity, transform).GetComponent<ParticleSystem>();
            }
            else
            {
                result = missileImpactEffectPool.Dequeue();
                result.transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y + 0.005f, tile.transform.position.z);
            }
        }
        else
        {
            result = Instantiate(missileImpactEffectPrefab, new Vector3(tile.transform.position.x, tile.transform.position.y + 0.005f, tile.transform.position.z), Quaternion.identity, transform).GetComponent<ParticleSystem>();
        }

        if(result != null)
        {
            particleTimers.Add(new ParticleTimer(result, tileHitEffectLifeTime));
            missileImpactEffectPool.Enqueue(result);
            result.gameObject.SetActive(true);
            result.Play();
        }

        return result;
    }

    public ParticleSystem PlayFollowMissileEffect(Transform effectParent)
    {
        ParticleSystem result = null;
        if (followMissileEffectPool.Count >= 1)
        {
            if (followMissileEffectPool.Peek().gameObject.activeSelf)
            {
                result = Instantiate(followMissileEffectPrefab, effectParent).GetComponent<ParticleSystem>();
            }
            else
            {
                result = followMissileEffectPool.Dequeue();
                result.transform.SetParent(effectParent);
            }
        }
        else
        {
            result = Instantiate(followMissileEffectPrefab, effectParent).GetComponent<ParticleSystem>();
        }

        if (result != null)
        {
            result.transform.localPosition = followEffectOriginPos;
            particleTimers.Add(new ParticleTimer(result, missileFollowEffectLifeTime));
            followMissileEffectPool.Enqueue(result);
            result.gameObject.SetActive(true);
            result.Play();
        }

        return result;
    }

    class ParticleTimer
    {
        public bool isOff = false;
        public ParticleSystem particle = null;
        public float madeTime;
        public float lifeTime = 0f;

        public ParticleTimer(ParticleSystem particle, float lifeTime)
        {
            madeTime = Time.time;
            this.lifeTime = lifeTime;
            this.particle = particle;
        }
    }
}
