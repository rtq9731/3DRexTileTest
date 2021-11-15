using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileManager: MonoBehaviour
{
    [SerializeField] GameObject missilePrefab = null;

    Queue<GameObject> missileObjPool = new Queue<GameObject>();

    int missileHeight = 3;

    List<MissileFire> missileFires = new List<MissileFire>();

    private GameObject GetMissileObj()
    {
        GameObject result = null;
        if(missileObjPool.Count < 1)
        {
            result = Instantiate(missilePrefab, transform);
            missileObjPool.Enqueue(result);
        }
        else
        {
            if(missileObjPool.Peek().activeSelf)
            {
                result = Instantiate(missilePrefab, transform);
                missileObjPool.Enqueue(result);
            }
            else
            {
                result = missileObjPool.Dequeue();
                missileObjPool.Enqueue(result);
            }
        }
        return result;
    }

    public void fireMissileFromStartToTarget(TileScript start, MissileData missile, TileScript target, out GameObject missileObj)
    {
        Vector3 midPoint = new Vector3((start.transform.position.x + target.transform.position.x) / 2, missileHeight, (start.transform.position.z + target.transform.position.z) / 2);
        // 두 벡터 사이의 중점을 구함 ( 높이는 따로 지정 )

        missileObj = GetMissileObj();

        Vector3 startPoint = start.transform.position;
        startPoint.y += 0.5f; // 미사일이 땅에서 나오면 안되니까.

        Vector3 targetPoint = target.transform.position;
        targetPoint.y += 0.5f;

        MainSceneManager.Instance.effectPool.PlayFollowMissileEffect(missileObj.transform);
        missileObj.transform.position = startPoint;
        missileObj.transform.rotation = Quaternion.LookRotation(midPoint);

        missileObj.SetActive(true);

        missileFires.Add(new MissileFire { targetPos = targetPoint, startPos = startPoint, midPos = midPoint, missileData = missile, missileObj = missileObj, targetTile = target });
    }

    private void Update()
    {
        if(missileFires.Count > 0)
        {
            List<MissileFire> willRemove = new List<MissileFire>();
            foreach (var item in missileFires)
            {
                Vector3 beforePos = item.missileObj.transform.position;

                if (Vector3.Distance(item.missileObj.transform.position, item.targetPos) <= 0.01)
                {
                    item.targetTile.Damage(item.missileData.WarheadType);
                    MainSceneManager.Instance.effectPool.PlayEffectOnTile(item.targetTile);
                    item.missileObj.SetActive(false);
                    willRemove.Add(item);
                }

                item.timer += Time.deltaTime * 0.5f;
                Vector3 p1 = Vector3.Lerp(item.startPos, item.midPos, item.timer);
                Vector3 p2 = Vector3.Lerp(item.midPos, item.targetPos, item.timer);
                item.missileObj.transform.position = Vector3.Lerp(p1, p2, item.timer);

                Vector3 delta = item.missileObj.transform.position - beforePos;
                item.missileObj.transform.rotation = Quaternion.LookRotation(delta.normalized);
                beforePos = item.missileObj.transform.position;

            }

            if(willRemove.Count > 0)
            {
                foreach (var item in willRemove)
                {
                    missileFires.Remove(item);
                }
            }
        }
    }

    class MissileFire
    {
        public Vector3 startPos = Vector3.zero;
        public Vector3 midPos = Vector3.zero;
        public Vector3 targetPos = Vector3.zero;
        public float timer = 0f;

        public GameObject missileObj = null;
        public TileScript targetTile = null;
        public MissileData missileData = null;

        public MissileFire()
        {
            timer = 0f;
        }
    }
}
