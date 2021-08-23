using UnityEngine;

public class HexTilemapGenerator : MonoBehaviour
{
    [SerializeField] GameObject[] groundTiles;

    private void Start()
    {
        GenerateTiles(15, 15);
    }

    private void GenerateTiles(int width, int height)
    {
        Vector3 tilePos = Vector3.zero;
        tilePos.z = -height / 2;
        for (int i = 0; i < height; i++)
        {
            tilePos.x = (i % 2 == 0) ? -width / 2 : -width / 2 + 0.5f;
            for (int j = 0; j < width; j++)
            {
                GameObject temp = Instantiate(groundTiles[Random.Range(1, groundTiles.Length)], this.gameObject.transform);
                temp.transform.position = tilePos;
                tilePos.x += 1;
            }
            tilePos.z += 0.875f;
        }
    }

}
