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

        this.transform.position = tile.Data.position;
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
            Debug.Log(selectedTiles.Count);
            return selectedTiles;
        }
        else
        {
            selectedTiles.ForEach(x => FindTilesInRange(x, range).ForEach(x => selectedTiles.Add(x)));
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
