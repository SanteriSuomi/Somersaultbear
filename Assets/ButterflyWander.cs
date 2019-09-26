using System.Collections;
using UnityEngine;

public class ButterflyWander : MonoBehaviour
{
    [SerializeField]
    private GameObject[] points;
    [SerializeField]
    private float moveSpeed = 5f;

    private void OnEnable()
    {
        transform.position = points[0].transform.position;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, points[1].transform.position, moveSpeed * Time.deltaTime);
    }
}
