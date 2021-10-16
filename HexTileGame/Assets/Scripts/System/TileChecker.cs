using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileChecker : MonoBehaviour
{
    [SerializeField] LayerMask whatIsTile;

    private float TileXInterval;
    private float TileZInterval;

    private void Start()
    {
        TileXInterval = MainSceneManager.Instance.TileXInterval;
        TileZInterval = MainSceneManager.Instance.TileZInterval;
    }

    List<TileScript> selectedTiles = new List<TileScript>();
    public List<TileScript> FindTilesInRange(TileScript tile, int range)
    {
        if (range <= 0)
        {
            return null;
        }

        Vector3 curPos = Vector3.zero;

        selectedTiles.RemoveRange(0, selectedTiles.Count);

        this.transform.position = tile.Data.Position;

        TileScript result = null;

        result = GetUnderTile(new Vector3(transform.position.x - TileXInterval, transform.position.y, transform.position.z)); // 9�� ���� (��)
        if (result != null)
        {
            selectedTiles.Add(result);
        }

        result = GetUnderTile(new Vector3(transform.position.x + TileXInterval, transform.position.y, transform.position.z)); // 3�� ���� (��)
        if(result != null)
        {
            selectedTiles.Add(result);
        }

        result = GetUnderTile(new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z + TileZInterval)); // 1�� �� ���� (�ϵ�)
        if (result != null)
        {
            selectedTiles.Add(result);
        }

        result = GetUnderTile(new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z + TileZInterval)); // 10�� �� ���� (�ϼ�)
        if (result != null)
        {
            selectedTiles.Add(result);
        }

        result = GetUnderTile(new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z - TileZInterval)); // 1�� �� ���� (����)
        if (result != null)
        {
            selectedTiles.Add(result);
        }

        result = GetUnderTile(new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z - TileZInterval)); // 7�� �� ���� (����)
        if (result != null)
        {
            selectedTiles.Add(result);
        }

        range--;
        if (range <= 0)
        {
            return selectedTiles;
        }
        else
        {
            List<TileScript> resultList = new List<TileScript>();
            foreach (var item in selectedTiles)
            {
                FindTilesInRange(item, range, resultList);
            };
            resultList.ForEach(x => selectedTiles.Add(x)); // �ߺ��Ȱ� ����

            selectedTiles.Remove(tile); // �ڱ� �ڽ� ����

            return selectedTiles.Distinct().ToList(); // �ٽ� �ѹ� �ߺ��Ȱ� �����ؼ� ��ȯ
        }
    }

    private List<TileScript> FindTilesInRange(TileScript tile, int range, List<TileScript> tileList) // ���� �ݺ��� �Լ�
    {
        if (range <= 0)
        {
            return null;
        }

        Vector3 curPos = Vector3.zero;

        this.transform.position = tile.Data.Position;
        TileScript result = null;

        result = GetUnderTile(new Vector3(transform.position.x - TileXInterval, transform.position.y, transform.position.z)); // 9�� ���� (��)
        if (result != null)
        {
            tileList.Add(result);
        }

        result = GetUnderTile(new Vector3(transform.position.x + TileXInterval, transform.position.y, transform.position.z)); // 3�� ���� (��)
        if (result != null)
        {
            tileList.Add(result);
        }

        result = GetUnderTile(new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z + TileZInterval)); // 1�� �� ���� (�ϵ�)
        if (result != null)
        {
            tileList.Add(result);
        }

        result = GetUnderTile(new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z + TileZInterval)); // 10�� �� ���� (�ϼ�)
        if (result != null)
        {
            tileList.Add(result);
        }

        result = GetUnderTile(new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z - TileZInterval)); // 1�� �� ���� (����)
        if (result != null)
        {
            tileList.Add(result);
        }

        result = GetUnderTile(new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z - TileZInterval)); // 7�� �� ���� (����)
        if (result != null)
        {
            tileList.Add(result);
        }

        range--;
        if (range <= 0)
        {
            return tileList;
        }
        else
        {
            Debug.Log(tileList.Count);
            foreach (var item in tileList)
            {
                FindTilesInRange(item, range, tileList);
            };

            return selectedTiles.Distinct().ToList();
        }
    }

    private TileScript GetUnderTile(Vector3 curPos)
    {
        curPos.y += 1;
        Ray ray = new Ray(curPos, Vector3.down);

        if(Physics.Raycast(ray, out RaycastHit hit, 10, whatIsTile))
        {
            return hit.transform.GetComponent<TileScript>();
        }
        else
        {
            return null;
        }
    }

}
