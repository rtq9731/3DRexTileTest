using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissilePool: MonoBehaviour
{
    GameObject missileObject = null;

    int missileHeight = 5;

    public void fireMissileFromStartToTarget(TileScript start, MissileData missile, TileScript target)
    {

        Vector3 midPoint = new Vector3(
            start.transform.position.x + target.transform.position.x,
            missileHeight,
            start.transform.position.z + target.transform.position.z);
        // 두 벡터 사이의 중점을 구함 ( 높이는 따로 지정 )

        StartCoroutine(FireCoroutine(start.transform.position, midPoint, target.transform.position, missileObject));
    }

    IEnumerator FireCoroutine(Vector3 startPoint, Vector3 midPoint, Vector3 targetPoint, GameObject missile)
    {

        while (missile.transform.position != targetPoint)
        {
            Vector3 p1 = Vector3.Lerp(startPoint, midPoint, Time.deltaTime);
            Vector3 p2 = Vector3.Lerp(midPoint, targetPoint, Time.deltaTime);
            missile.transform.position = Vector3.Lerp(p1, p2, Time.deltaTime);
            yield return null;
        }

    }
}
