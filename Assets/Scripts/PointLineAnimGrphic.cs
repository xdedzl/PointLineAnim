using System.Collections.Generic;
using UnityEngine;

public class PointLineAnimGrphic : MonoBehaviour
{
    [SerializeField]
    private int pointCount = 10;
    public float distance = 50;
    public Color pointColor = Color.white;
    public Color lineColor = Color.white;
    public float speed = 1;

    private List<Point> m_Points;
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

        m_Points = new List<Point>();
        screenWidth = Screen.width;
        screenHeight = Screen.height;

        material = new Material(Shader.Find("RunTimeHandles/VertexColor"));
        material.color = Color.white;

        for (int i = 0; i < pointCount; i++)
        {
            GeneratePoint();
        }
    }

    private void GeneratePoint()
    {
        Point vector = new Point(Random.Range(0, Screen.width), Random.Range(0, Screen.height));
        m_Points.Add(vector);
    }

    private void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        for (int i = 0; i < m_Points.Count; i++)
        {
            m_Points[i].OnUpdate(speed, mousePos);
        } 
    }

    private void OnPostRender()
    {
        material.SetPass(0);
        GL.LoadPixelMatrix();

        GL.Begin(GL.LINES);
        GL.Color(lineColor);
        foreach (var p1 in m_Points)
        {
            foreach (var p2 in m_Points)
            {
                if (Point.Distance(p1, p2) < distance)
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