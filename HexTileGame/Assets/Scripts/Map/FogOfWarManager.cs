using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWarManager : MonoBehaviour
{
    [SerializeField] GameObject cloudPrefab;
    List<CloudObject> cloudList = new List<CloudObject>();

    public void GenerateCloudOnTile(TileScript tile)
    {
        cloudList.Add(Instantiate(cloudPrefab, tile.transform).GetComponent<CloudObject>());
    }

    public void RemoveCloudOnTile(TileScript tile)
    {
        cloudList.Find(x => x.transform.parent == tile.transform).RemoveCloud();
    }

}
