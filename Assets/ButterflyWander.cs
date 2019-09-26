using System.Collections;
using UnityEngine;

public class ButterflyWander : MonoBehaviour
{
    [SerializeField]
    private GameObject[] points;

    private float moveSpeed = 1f;
    private float pointLength = 5f;

    private int random;

    private bool calculated = false;

    private void Start()
    {
        CalculateNewPoint();
    }

    private void Update()
    {
        if (!calculated)
        {
            CalculateNewPoint();
        }
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        if (calculated == true)
        {
            var randomPoint = points[random].transform.position;

            transform.position = Vector3.Lerp(transform.position, randomPoint, moveSpeed * Time.deltaTime);
            yield return new WaitUntil(() => CalculateDistance(transform.position, randomPoint));
            Debug.Log("ASDASDASDASDASDASDADS");
            CalculateNewPoint();
            calculated = false;
        }
    }

    private void CalculateNewPoint()
    {
        random = Random.Range(0, points.Length);
        calculated = true;
    }

    private bool CalculateDistance(Vector3 pos1, Vector3 pos2)
    {
        float distance = Vector3.Distance(pos1, pos2);
        if (distance == float.Epsilon)
        {
            return true;
        }
        return false;
    }
}
