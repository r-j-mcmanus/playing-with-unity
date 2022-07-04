using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MovePathScript : MonoBehaviour
{
    private UnityEngine.AI.NavMeshPath path;
    private LineRenderer lineRenderer = null;
    private RaycastHit hit;

    public float maxPathLengthSquared = 100.0f;

    private ClickRayCastScript clickRayCast =  null;

    public GameObject selectedPC = null;

    // Start is called before the first frame update
    void Start()
    {
        path = new UnityEngine.AI.NavMeshPath();
        lineRenderer = GetComponent<LineRenderer>();
        clickRayCast = GetComponent<ClickRayCastScript>();
    }

    void DrawPath()
    {
        UnityEngine.AI.NavMesh.CalculatePath(selectedPC.transform.position, clickRayCast.hitPoint, UnityEngine.AI.NavMesh.AllAreas, path);
        Vector3[] positions = path.corners;
            
        Debug.Log(positions.Length);

        positions = TrimPositions(positions);

        lineRenderer.positionCount = positions.Length;
        lineRenderer.SetPositions(positions);
    }


    private Vector3[] TrimPositions(Vector3[] positions)
    {
        float pathLength = 0.0f;
        float lastLength = 0;
        int lastIndex = 0;
        float norm = 1;

        Vector3 lastVector = Vector3.zero;

        for (int positionsIndex = 1; positionsIndex < positions.Length; positionsIndex++)
        {
            lastVector = positions[positionsIndex] - positions[positionsIndex - 1];
            lastLength = lastVector.magnitude;
            pathLength = pathLength + lastLength;
            if (pathLength > maxPathLengthSquared)
            {
                pathLength = pathLength - lastLength;

                lastIndex = positionsIndex;
                norm = (maxPathLengthSquared - pathLength) / lastLength;

                Vector3[] shorterPositions = new Vector3[lastIndex + 1];
                Array.Copy(positions, shorterPositions, lastIndex + 1);

                shorterPositions[lastIndex] = shorterPositions[lastIndex - 1] + lastVector * norm;

                return shorterPositions;
            }
        }
        return positions;
    }

}
