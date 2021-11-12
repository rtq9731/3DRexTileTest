using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileManager: MonoBehaviour
{
    [SerializeField] GameObject missilePrefab = null;

    Queue<GameObject> missileObjPool = new Queue<GameObject>();

    int missileHeight = 3;

    float timer = 0f;

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
        // �� ���� ������ ������ ���� ( ���̴� ���� ���� )

        missileObj = GetMissileObj();

        Vector3 startPoint = start.transform.position;
        startPoint.y += 0.5f; // �̻����� ������ ������ �ȵǴϱ�.

        Vector3 targetPoint = target.transform.position;
        targetPoint.y += 0.5f;

        missileObj.SetActive(true);

        timer = 0f;

        missileObj.transform.position = startPoint;
        missileObj.transform.rotation = Quaternion.LookRotation(midPoint);
        missileFires.Add(new MissileFire { targetPos = targetPoint, startPos = startPoint, midPos = midPoint, missileData = missile, missileObj = missileObj, targetTile = target });
    }

    private void Update()
    {
        if(missileFires.Count > 0)
        {
            List<MissileFire> willRemove = new List<MissileFire>();
            timer += Time.deltaTime * 0.5f;
            foreach (var item in missileFires)
            {
                Vector3 beforePos = item.missileObj.transform.position;

                Vector3 p1 = Vector3.Lerp(item.startPos, item.midPos, timer);
                Vector3 p2 = Vector3.Lerp(item.midPos, item.targetPos, timer);
                item.missileObj.transform.position = Vector3.Lerp(p1, p2, timer);

                Vector3 delta = item.missileObj.transform.position - beforePos;
                item.missileObj.transform.rotation = Quaternion.LookRotation(delta.normalized);
                beforePos = item.missileObj.transform.position;

                if (Vector3.Distance(item.missileObj.transform.position, item.targetPos) <= 0.01)
                {
                    item.targetTile.Damage(item.missileData.WarheadType);
                    MainSceneManager.Instance.effectPool.PlayEffectOnTile(item.targetTile);
                    item.missileObj.SetActive(false);
                    willRemove.Add(item);
                }
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

        public GameObject missileObj = null;
        public TileScript targetTile = null;
        public MissileData missileData = null;


        public bool CheckIsOverMidPoint()
        {
            Vector2 startPos2D = new Vector2(startPos.x, startPos.z);
            Vector2 midPos2D = new Vector2(midPos.x, midPos.z);
            Vector2 missilePos2D = new Vector2(missileObj.transform.position.x, missileObj.transform.position.z);

            return Vector2.Distance(startPos2D, midPos2D) < Vector2.Distance(startPos2D, missilePos2D);
        }

        public MissileFire()
        {

        }
    }
}
