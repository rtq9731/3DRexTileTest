using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileManager: MonoBehaviour
{
    [SerializeField] GameObject missilePrefab = null;
    GameObject missileObj;

    int missileHeight = 3;

    float timer = 0f;
    private void Awake()
    {
        missileObj = Instantiate(missilePrefab);
        missileObj.SetActive(false);
    }

    public void fireMissileFromStartToTarget(TileScript start, MissileData missile, TileScript target, out GameObject missileObj)
    {

        Vector3 midPoint = new Vector3((start.transform.position.x + target.transform.position.x) / 2, missileHeight, (start.transform.position.z + target.transform.position.z) / 2);
        // 두 벡터 사이의 중점을 구함 ( 높이는 따로 지정 )

        missileObj = this.missileObj;

        Vector3 startPoint = start.transform.position;
        startPoint.y += 0.5f; // 미사일이 땅에서 나오면 안되니까.

        Vector3 targetPoint = target.transform.position;
        targetPoint.y += 0.5f;

        missileObj.SetActive(true);

        timer = 0f;

        missileObj.transform.position = startPoint;
        StartCoroutine(FireCoroutine(startPoint, midPoint, targetPoint, missileObj));
    }

    IEnumerator FireCoroutine(Vector3 startPoint, Vector3 midPoint, Vector3 targetPoint, GameObject missile)
    {
        while (missile.transform.position != targetPoint)
        {
            timer += Time.deltaTime * 0.5f;
            Vector3 p1 = Vector3.Lerp(startPoint, midPoint, timer);
            Vector3 p2 = Vector3.Lerp(midPoint, targetPoint, timer);
            missile.transform.position = Vector3.Lerp(p1, p2, timer);
            yield return null;
        }

        missile.SetActive(false);
    }
}
