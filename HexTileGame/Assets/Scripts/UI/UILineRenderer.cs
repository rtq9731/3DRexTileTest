using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILineRenderer : Graphic
{
    [SerializeField] Vector2Int gridSize;
    [SerializeField] List<Vector2> points = new List<Vector2>();

    float width;
    float height;
    float unitWidth;
    float unitHeight;

    [SerializeField] float thickness = 10f;

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();

        width = rectTransform.rect.width;
        height = rectTransform.rect.height;

        unitWidth = width / (float)gridSize.x;
        unitHeight = height / (float)gridSize.y;

        if(points.Count < 2)
        {
            return;
        }    

        for (int i = 0; i < points.Count; i++)
        {
            Vector2 point = points[i];

            DrawVerticesForPoint(point, vh);    
        }

        for (int i = 0; i < points.Count - 1; i++)
        {
            int idx = i * 2;
            vh.AddTriangle(idx + 0, idx + 1, idx + 3);
            vh.AddTriangle(idx + 3, idx + 2, idx + 0);
        }
    }

    void DrawVerticesForPoint(Vector2 point, VertexHelper vh)
    {
        UIVertex vertex = UIVertex.simpleVert;
        vertex.color = color;

        vertex.position = new Vector3(-thickness / 2, 0);
        vertex.position += new Vector3(unitWidth * point.x, unitHeight * point.y);
        vh.AddVert(vertex);

        vertex.position = new Vector3(thickness / 2, 0);
        vertex.position += new Vector3(unitWidth * point.x, unitHeight * point.y);
        vh.AddVert(vertex);
    }
}
