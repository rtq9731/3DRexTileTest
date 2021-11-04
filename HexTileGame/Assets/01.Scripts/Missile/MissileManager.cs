using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileManager: MonoBehaviour
{
    [SerializeField] GameObject missilePrefab = null;

    Queue<GameObject> missileObjPool = new Queue<GameObject>();

    int missileHeight = 3;

    float timer = 0f;

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

        missileObj.SetActive(true);

        timer = 0f;

        missileObj.transform.position = startPoint;
        StartCoroutine(FireCoroutine(startPoint, midPoint, targetPoint, missileObj, target, missile));
    }

    IEnumerator FireCoroutine(Vector3 startPoint, Vector3 midPoint, Vector3 targetPoint, GameObject missileObj, TileScript targetTile, MissileData missileData)
    {
        while (missileObj.transform.position != targetPoint)
        {
            timer += Time.deltaTime * 0.5f;
            Vector3 p1 = Vector3.Lerp(startPoint, midPoint, timer);
            Vector3 p2 = Vector3.Lerp(midPoint, targetPoint, timer);
            missileObj.transform.position = Vector3.Lerp(p1, p2, timer);
            yield return null;
        }

        
        targetTile.Damage(missileData.WarheadType);
        MainSceneManager.Instance.effectPool.PlayEffectOnTile(targetTile);
        missileObj.SetActive(false);
    }
}
