using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineMaker : MonoBehaviour
{
    [SerializeField] [Range(0f, 1f)] private float lineStartWidth = 0.4f;
    [SerializeField] [Range(0f, 1f)] private float lineEndWidth = 0.4f;

    public LineRenderer line;

    public Vector3 startPoint;
    public Vector3 endPoint;

    public Vector3 firstPlayerPosition;
    public Vector3 secondPlayerPosition;
    private readonly List<ParticleSystem> particles = new List<ParticleSystem>();

    private GameObject firstPlayer;

    private bool flag;
    private float min;
    private GameObject secondPlayer;

    public void Start()
    {
        line.startWidth = lineStartWidth;
        line.endWidth = lineEndWidth;
        line.positionCount = 0;
        for (var i = 0; i < gameObject.transform.childCount; i++)
            particles.Add(gameObject.transform.GetChild(i).GetComponent<ParticleSystem>());
    }

    public void Update()
    {
        firstPlayerPosition = firstPlayer.transform.position;
        firstPlayerPosition.y += 1.2f;

        secondPlayerPosition = secondPlayer.transform.position;
        secondPlayerPosition.y += 1.2f;

        line.positionCount = 2;

        startPoint = new Vector3(firstPlayerPosition.x, firstPlayerPosition.y, -2);
        endPoint = new Vector3(secondPlayerPosition.x, secondPlayerPosition.y, -2);

        flag = true;
        min = float.MaxValue;

        var walls = GameObject.FindGameObjectsWithTag("Wall");
        foreach (var wall in walls)
        {
            var wallPos = wall.transform.position;
            var wallScale = wall.transform.localScale;

            ChangeEndPoint(wallPos, wallScale);
        }


        Control.UpdateDictionary(secondPlayer, flag);

        line.SetPosition(0, startPoint);
        line.SetPosition(1, endPoint);
        foreach (var t in particles) t.transform.position = line.GetPosition(1);
    }

    public void ChangePlayers(GameObject first, GameObject second)
    {
        firstPlayer = first;
        secondPlayer = second;
    }

    private void ChangeEndPoint(Vector3 pos, Vector3 scale)
    {
        var points = new List<Vector2>
        {
            new Vector2(pos.x + scale.x / 2, pos.y + scale.y / 2),
            new Vector2(pos.x - scale.x / 2, pos.y + scale.y / 2),
            new Vector2(pos.x - scale.x / 2, pos.y - scale.y / 2),
            new Vector2(pos.x + scale.x / 2, pos.y - scale.y / 2),
            new Vector2(pos.x + scale.x / 2, pos.y + scale.y / 2)
        };
        for (var i = 0; i < points.Count - 1; i++)
        {
            var point1 = points[i];
            var point2 = points[i + 1];

            var intersectionPoint =
                FindIntersectionPoint(secondPlayerPosition.x, secondPlayerPosition.y, firstPlayerPosition.x,
                    firstPlayerPosition.y, point1.x, point1.y, point2.x, point2.y);
            if (intersectionPoint == null) continue;
            var dx = ((Vector2) intersectionPoint).x - firstPlayerPosition.x;
            var dy = ((Vector2) intersectionPoint).y - firstPlayerPosition.y;
            var sqrt = (float) Math.Sqrt(dx * dx + dy * dy);
            if (sqrt < min)
            {
                min = sqrt;
                endPoint = new Vector3(((Vector2) intersectionPoint).x, ((Vector2) intersectionPoint).y, -2);
            }

            flag = false;
        }
    }

    private Vector2? FindIntersectionPoint(float sndPlrPosX, float sndPlrPosY, float fstPlrPosX, float fstPlrPosY,
        float point1X, float point1Y, float point2X,
        float point2Y)
    {
        if (sndPlrPosX > fstPlrPosX)
        {
            (sndPlrPosX, fstPlrPosX) = (fstPlrPosX, sndPlrPosX);
            (sndPlrPosY, fstPlrPosY) = (fstPlrPosY, sndPlrPosY);
        }

        if (point1X > point2X)
        {
            (point1X, point2X) = (point2X, point1X);
            (point1Y, point2Y) = (point2Y, point1Y);
        }

        var x = 0f;
        var y = 0f;
        if (Math.Abs(point2X - point1X) < 10e-7 && Math.Abs(sndPlrPosX - fstPlrPosX) < 10e-7)
            return null;
        if (Math.Abs(point2X - point1X) < 10e-7)
        {
            var k1 = (fstPlrPosY - sndPlrPosY) / (fstPlrPosX - sndPlrPosX);
            var b1 = sndPlrPosY - k1 * sndPlrPosX;
            x = point1X;
            y = k1 * x + b1;
            if (Math.Min(sndPlrPosY, fstPlrPosY) <= y && Math.Max(sndPlrPosY, fstPlrPosY) >= y &&
                Math.Min(point1Y, point2Y) <= y && Math.Max(point1Y, point2Y) >= y)
                if (fstPlrPosX <= x && x <= sndPlrPosX || sndPlrPosX <= x && x <= fstPlrPosX)
                    return new Vector2(x, y);
        }
        else if (Math.Abs(sndPlrPosX - fstPlrPosX) < 10e-7)
        {
            var k2 = (point2Y - point1Y) / (point2X - point1X);
            var b2 = point1Y - k2 * point1X;
            x = sndPlrPosX;
            y = k2 * x + b2;
            if (Math.Min(sndPlrPosY, fstPlrPosY) <= y && Math.Max(sndPlrPosY, fstPlrPosY) >= y &&
                Math.Min(point1Y, point2Y) <= y && Math.Max(point1Y, point2Y) >= y)
                return new Vector2(x, y);
        }
        else
        {
            var k1 = (fstPlrPosY - sndPlrPosY) / (fstPlrPosX - sndPlrPosX);
            var k2 = (point2Y - point1Y) / (point2X - point1X);
            if (Math.Abs(k1 - k2) <= 10e-7f) return null;
            var b1 = sndPlrPosY - k1 * sndPlrPosX;
            var b2 = point1Y - k2 * point1X;
            x = (b2 - b1) / (k1 - k2);
            y = k1 * x + b1;
            if (sndPlrPosX <= x && fstPlrPosX >= x && point1X <= x && point2X >= x)
                return new Vector2(x, y);
        }

        return null;
    }
}