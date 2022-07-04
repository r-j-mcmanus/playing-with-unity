using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class WalkablePathScript : MonoBehaviour
{
    private UnityEngine.AI.NavMeshPath path;
    private LineRenderer lineRenderer = null;

    private Vector3 _endPosition;
    public Vector3 endPosition { get { return _endPosition; } }

    public bool isPath = false;


    // Start is called before the first frame update
    void Start()
    {
        path = new UnityEngine.AI.NavMeshPath();
        lineRenderer = gameObject.GetComponent<LineRenderer>();

        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.white;
        Material whiteDiffuseMat = new Material(Shader.Find("Unlit/Texture"));
        lineRenderer.material = whiteDiffuseMat;
    }

    private void OnDestroy()
    {
        Destroy(this.GetComponent<Renderer>().material);
    }

    public void makePath(Vector3 start, Vector3 end, float maxDist)
    {
        isPath = UnityEngine.AI.NavMesh.CalculatePath(start, end, UnityEngine.AI.NavMesh.AllAreas, path);
        if (isPath)
        {
            Vector3[] positions = path.corners;
            positions = TrimPositions(positions, maxDist);
            _endPosition = positions[positions.Length - 1];

            lineRenderer.enabled = true;
            lineRenderer.positionCount = positions.Length;
            lineRenderer.SetPositions(positions);
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }

    private Vector3[] TrimPositions(Vector3[] positions, float maxDist)
    {
        float pathLength = 0.0f;
        float lastLength = 0.0f;
        Vector3 lastVector = Vector3.zero;

        for (int positionsIndex = 1; positionsIndex < positions.Length; positionsIndex++)
        {
            lastVector = positions[positionsIndex] - positions[positionsIndex - 1];
            lastLength = lastVector.magnitude;
            pathLength = pathLength + lastLength;
            if (pathLength > maxDist)
            {
                pathLength = pathLength - lastLength;

                int lastIndex = positionsIndex;
                float norm = (maxDist - pathLength) / lastLength;

                Vector3[] shorterPositions = new Vector3[lastIndex + 1];
                Array.Copy(positions, shorterPositions, lastIndex + 1);

                shorterPositions[lastIndex] = shorterPositions[lastIndex - 1] + lastVector * norm;

                return shorterPositions;
            }
        }
        return positions;
    }

    public void SetActive(bool val)
    {
        this.gameObject.SetActive(val);
    }
}
