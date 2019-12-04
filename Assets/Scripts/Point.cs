using UnityEngine;

public class Point
{
    public float x;
    public float y;
    public Color color;

    private int xOffset;
    private int yOffset;

    private int interval;
    private int intervaler;

    public Point()
    {
        xOffset = Random.Range(0, 2) != 0 ? -1 : 1;
        yOffset = Random.Range(0, 2) != 0 ? -1 : 1;
        intervaler = Random.Range(3, 10);
        interval = 0;
    }

    public Point(float x, float y):this()
    {
        this.x = x;
        this.y = y;
    }

    public Point(float x, float y, Color color) : this(x, y)
    {
        this.color = color;
    }

    public void OnUpdate(float speed, Vector3 mousePos)
    {
        float dis = Dis(mousePos);
        speed = speed * (1 / dis);

        if (dis < 100)
        {
            if(mousePos.x > x)
            {
                x += speed;
            }
            else
            {
                x -= speed;
            }

            if (mousePos.y > y)
            {
                y += speed;
            }
            else
            {
                y -= speed;
            }
        }
        else
        {
            x += xOffset * speed;
            y += yOffset * speed;
        }
        
        interval++;

        if (interval >= intervaler)
        {
            int b = Random.Range(0, 2);
            if (b == 0)
            {
                xOffset *= -1;
            }
            b = Random.Range(0, 2);
            if (b == 0)
            {
                yOffset *= -1;
            }

            intervaler = Random.Range(3, 50);
            interval = 0;

            if (x > Screen.width || x < 0 || y > Screen.height || y < 0)
            {
                x = Random.Range(0, Screen.width);
                y = Random.Range(0, Screen.height);
            }
        }
    }

    private float Dis(Vector3 pos)
    {
        return Mathf.Sqrt(Mathf.Pow(pos.x - x, 2) + Mathf.Pow(pos.y - y, 2));
    }

    public static float Distance(Point a, Point b)
    {
        return Mathf.Sqrt(Mathf.Pow(b.x - a.x, 2) + Mathf.Pow(b.y - a.y, 2));  
    }

    public static implicit operator Vector3(Point v)
    {
        return new Vector3(v.x, v.y, 0);
    }
}