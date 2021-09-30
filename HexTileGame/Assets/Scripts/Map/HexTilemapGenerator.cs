using UnityEngine;

public class HexTilemapGenerator : MonoBehaviour
{
    public enum GroundType
    {
        None,
        Jungle,
        Plain,
        Mountain
    }

    float TileXInterval = 1f;
    float TileZInterval = 0.875f;

    [SerializeField] GroundType type;

    [SerializeField] GameObject[] jungleGroundTileSet;
    [SerializeField] GameObject[] plainGroundTileSet;
    [SerializeField] GameObject[] MountainGroundTileSet;

    [SerializeField] GameObject beachTile;

    GameObject[] groundTiles;

    private void Start()
    {
        switch (type)
        {
            case GroundType.Jungle:
                groundTiles = jungleGroundTileSet;
                break;
            case GroundType.Plain:
                groundTiles = plainGroundTileSet;
                break;
            case GroundType.Mountain:
                groundTiles = MountainGroundTileSet;
                break;
            default:
                break;
        }

        TileXInterval = MainSceneManager.Instance.TileXInterval;
        TileZInterval = MainSceneManager.Instance.TileZInterval;

        GenerateTiles(15, 15);
    }

    private void GenerateTiles(int width, int height)
    {
        Vector3 tilePos = Vector3.zero;
        tilePos.z -= height / 2; // ¡ﬂæ”¿∏∑Œ Z¡¬«•∏¶ ∏¬√Á¡÷±‚ ¿ß«‘
        int cnt = 0;
        for (int i = 0; i < height; i++)
        {
            tilePos.x = (i % 2 == 0) ? -width / 2 : -width / 2 + 0.5f;
            for (int j = 0; j < width; j++)
            {
                GameObject temp = Instantiate(groundTiles[Random.Range(1, groundTiles.Length)], tilePos, Quaternion.Euler(Vector3.zero), this.gameObject.transform);
                TileScript tempScirpt = temp.GetComponent<TileScript>();
                TileMapData.Instance.SetTileData(tempScirpt);
                tempScirpt.SetPosition(tilePos);
                tempScirpt.Data.tileNum = cnt;
                tilePos.x += TileXInterval;
                cnt++;
            }
            tilePos.z += TileZInterval;
        }

        GetComponent<HexObjectTileManager>().GenerateObjects(width, height, type);
    }

}
