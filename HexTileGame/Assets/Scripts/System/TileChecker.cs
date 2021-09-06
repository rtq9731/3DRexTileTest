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
        TileXInterval = GameManager.Instance.TileXInterval;
        TileZInterval = GameManager.Instance.TileZInterval;
    }

    public TileScript[] FindTilesInRange(TileScript tile, int range)
    {
        if(range <= 0)
        {
            return null;
        }

        Vector3 curPos = Vector3.zero;
        this.transform.position = tile.Data.position;
        curPos = this.transform.position;
        curPos.x += TileZInterval;
        this.transform.position = curPos;

        return null;
    }

    private TileScript GetUnderTile(Vector3 curPos)
    {
        Ray ray = new Ray(curPos, Vector3.down);

        if(Physics.Raycast(ray, out RaycastHit hit, 1, whatIsTile))
        {
            return hit.transform.GetComponent<TileScript>();
        }
        else
        {
            return null;
        }
    }

}
