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

        selectedTiles = new List<TileScript>();

        this.transform.position = tile.Data.Position;

        TileScript result;
        List<TileScript> selectedTileOnMyMethod = new List<TileScript>();

        result = GetUnderTile(new Vector3(transform.position.x - TileXInterval, transform.position.y, transform.position.z)); // 9시 방향 (북)
        if (result != null)
        {
            selectedTiles.Add(result);
            selectedTileOnMyMethod.Add(result);
        }

        result = GetUnderTile(new Vector3(transform.position.x + TileXInterval, transform.position.y, transform.position.z)); // 3시 방향 (남)
        if (result != null)
        {
            selectedTiles.Add(result);
            selectedTileOnMyMethod.Add(result);
        }

        result = GetUnderTile(new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z + TileZInterval)); // 1시 반 방향 (북동)
        if (result != null)
        {
            selectedTiles.Add(result);
            selectedTileOnMyMethod.Add(result);
        }

        result = GetUnderTile(new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z + TileZInterval)); // 10시 반 방향 (북서)
        if (result != null)
        {
            selectedTiles.Add(result);
            selectedTileOnMyMethod.Add(result);
        }

        result = GetUnderTile(new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z - TileZInterval)); // 1시 반 방향 (남동)
        if (result != null)
        {
            selectedTiles.Add(result);
            selectedTileOnMyMethod.Add(result);
        }

        result = GetUnderTile(new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z - TileZInterval)); // 7시 반 방향 (남서)
        if (result != null)
        {
            selectedTiles.Add(result);
            selectedTileOnMyMethod.Add(result);
        }

        range--;
        if (range <= 0)
        {
            return selectedTiles;
        }
        else
        {
            foreach (var item in selectedTileOnMyMethod)
            {
                FindTilesInMoreRange(item, range);
            };

            selectedTiles.Remove(tile); // 자기 자신 제거
            Debug.Log(selectedTiles.Count);

            return selectedTiles.Distinct().ToList(); // 중복된것 제거해서 반환
        }
    }

    private void FindTilesInMoreRange(TileScript tile, int range) // 내부 반복용 함수
    {
        if (range <= 0)
        {
            return;
        }

        this.transform.position = tile.Data.Position;
        TileScript result;

        List<TileScript> selectedTileOnMyMethod = new List<TileScript>();

        result = GetUnderTile(new Vector3(transform.position.x - TileXInterval, transform.position.y, transform.position.z)); // 9시 방향 (북)
        if (result != null)
        {
            selectedTiles.Add(result);
            selectedTileOnMyMethod.Add(result);
        }

        result = GetUnderTile(new Vector3(transform.position.x + TileXInterval, transform.position.y, transform.position.z)); // 3시 방향 (남)
        if (result != null)
        {
            selectedTiles.Add(result);
            selectedTileOnMyMethod.Add(result);
        }

        result = GetUnderTile(new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z + TileZInterval)); // 1시 반 방향 (북동)
        if (result != null)
        {
            selectedTiles.Add(result);
            selectedTileOnMyMethod.Add(result);
        }

        result = GetUnderTile(new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z + TileZInterval)); // 10시 반 방향 (북서)
        if (result != null)
        {
            selectedTiles.Add(result);
            selectedTileOnMyMethod.Add(result);
        }

        result = GetUnderTile(new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z - TileZInterval)); // 1시 반 방향 (남동)
        if (result != null)
        {
            selectedTiles.Add(result);
            selectedTileOnMyMethod.Add(result);
        }

        result = GetUnderTile(new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z - TileZInterval)); // 7시 반 방향 (남서)
        if (result != null)
        {
            selectedTiles.Add(result);
            selectedTileOnMyMethod.Add(result);
        }

        range--;
        if (range <= 0)
        {
            return;
        }
        else
        {
            foreach (var item in selectedTileOnMyMethod)
            {
                FindTilesInRange(item, range);
            };
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
