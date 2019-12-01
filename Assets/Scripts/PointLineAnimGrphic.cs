using System.Collections.Generic;
using UnityEngine;

public class PointLineAnimGrphic : MonoBehaviour
{
    [SerializeField]
    private int pointCount = 10;
    public float distance = 50;
    public Color PointColor = Color.white;
    public Color LineColor = Color.white;

    private List<Vector2> m_Points;
    private int screenWidth;
    private int screenHeight;

    private Material material;

    public int PointCount
    {
        get
        {
            return pointCount;
        }
        set
        {
            if(value > pointCount)
            {
                while (m_Points.Count < value)
                {
                    GeneratePoint();
                }
            }
            else if(value < pointCount)
            {
                while (m_Points.Count > value)
                {
                    m_Points.RemoveAt(m_Points.Count - 1);
                }
            }

            pointCount = value;
        }
    }

    private void Awake()
    {
        if(GetComponent<Camera>() == null)
        {
            var anim = Camera.main.gameObject.AddComponent<PointLineAnimGrphic>();
            anim.PointCount = pointCount;

            Destroy(this);
            return;
        }

        m_Points = new List<Vector2>();
        screenWidth = Screen.width;
        screenHeight = Screen.height;

        material = new Material(Shader.Find("Standard"));

        for (int i = 0; i < pointCount; i++)
        {
            GeneratePoint();
        }
    }

    private void GeneratePoint()
    {
        Vector2Int vector = new Vector2Int(Random.Range(0, 200), Random.Range(0, 200));
        m_Points.Add(vector);
    }

    private void Update()
    {
        for (int i = 0; i < m_Points.Count; i++)
        {
            m_Points[i] += new Vector2(Random.Range(-1, 2), Random.Range(-1, 2));
        } 
    }

    private void OnPostRender()
    {
        GL.LoadPixelMatrix();

        GL.Begin(GL.LINES);
        GL.Color(LineColor);
        foreach (var p1 in m_Points)
        {
            foreach (var p2 in m_Points)
            {
                if (Vector2.Distance(p1, p2) < distance)
                {
                    GL.Vertex(p1);
                    GL.Vertex(p2);
                }
            }
        }
        GL.End();
    }

    private void OnValidate()
    {
        PointCount = pointCount;
    }
}