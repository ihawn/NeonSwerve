using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class PathGenerator : MonoBehaviour
{
    [SerializeField]
    float pathGenerationDelay, pathGenerationTimer;

    [SerializeField]
    Vector2 xBounds, yBounds, zBounds;

    [SerializeField]
    int drawDistance;

    [SerializeField]
    bool generatePath = true;

    [SerializeField]
    Vector3 currentPathCoords, lastPathCoords;

    public PathCreator pathPrefab;
    PathCreator thisPath;
    Queue<Vector3> bezierPoints;
    

    void Start()
    {
        thisPath = GetComponent<PathCreator>();
        CurvePointInit();
        currentPathCoords += Vector3.one * Random.Range(1f, 3f);
    }

    void Update()
    {
        GeneratePath();
    }

    void GeneratePath()
    {
        if (generatePath && pathGenerationTimer >= pathGenerationDelay)
        {
            pathGenerationTimer = 0;
            BezierPath bpath = new BezierPath(bezierPoints.ToArray());
            thisPath.bezierPath = bpath;

            lastPathCoords = currentPathCoords;
            currentPathCoords += PathPointOffset();
            bezierPoints.Enqueue(currentPathCoords);
            bezierPoints.Dequeue();
        }
        pathGenerationTimer += Time.deltaTime;
    }

    void CurvePointInit()
    {
        bezierPoints = new Queue<Vector3>();
        for (int i = 0; i < drawDistance; i++)
        {
            bezierPoints.Enqueue(currentPathCoords);
            lastPathCoords = currentPathCoords;
            currentPathCoords += PathPointOffset();
        }
    }

    Vector3 PathPointOffset()
    {
        return new Vector3(Random.Range(xBounds.x, xBounds.y), Random.Range(yBounds.x, yBounds.y), Random.Range(zBounds.x, zBounds.y));
    }
}
