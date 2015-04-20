using UnityEngine;
using System.Collections;

public class GridManager : MonoBehaviour {

    public float distanceBetweenElements = 10f;
    public int xFactor = 0;
    public int yFactor = 0;

    [ContextMenu ("Arrange")]
    public void ArrangeElements()
    {
        for (int i = 0; i < transform.childCount; i++)
        {

            transform.GetChild(i).transform.localPosition = new Vector3(i * xFactor * distanceBetweenElements, i * yFactor * distanceBetweenElements, 0);
        }
    }
}
