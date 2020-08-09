using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererConnect : MonoBehaviour
{
    [SerializeField]
    private Transform armConnect, handConnect;

    private LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        lineRenderer.SetPosition(0, new Vector3(armConnect.position.x, armConnect.position.y, 0f));
        lineRenderer.SetPosition(1, new Vector3(handConnect.position.x, handConnect.position.y, 0f));
    }
}
