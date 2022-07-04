using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ShowGoldenPath : MonoBehaviour
{
    private UnityEngine.AI.NavMeshPath path;
    private LineRenderer lineRenderer = null;
    public float maxPathLengthSquared = 10.0f;
    public RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        path = new UnityEngine.AI.NavMeshPath();
        lineRenderer = gameObject.GetComponent<LineRenderer>();

        Debug.Log(path.corners.Length);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                UnityEngine.AI.NavMesh.CalculatePath(transform.position, hit.point, UnityEngine.AI.NavMesh.AllAreas, path);
                Vector3[] positions = path.corners;

                positions=TrimPositions(positions);

                lineRenderer.positionCount = positions.Length;
                lineRenderer.SetPositions(positions);
            }
        }
    }

    private Vector3[] TrimPositions(Vector3[] positions)
    {
        float pathLength = 0.0f;
        float lastLength = 0;
        Vector3 lastVector = Vector3.zero;

        for (int positionsIndex = 1; positionsIndex < positions.Length; positionsIndex++)
        {
            lastVector = positions[positionsIndex] - positions[positionsIndex - 1];
            lastLength = lastVector.magnitude;
            pathLength = pathLength + lastLength;
            if(pathLength > maxPathLengthSquared)
            {
                pathLength = pathLength - lastLength;

                int lastIndex = positionsIndex;
                float norm = (maxPathLengthSquared - pathLength) / lastLength;

                Vector3[] shorterPositions = new Vector3[lastIndex + 1];
                Array.Copy(positions, shorterPositions, lastIndex + 1);

                shorterPositions[lastIndex] = shorterPositions[lastIndex-1] + lastVector * norm;

                return shorterPositions;
            }
        }
        return positions;        
    }

}
