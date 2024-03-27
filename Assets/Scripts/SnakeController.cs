using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float steerSpeed = 5;
    [SerializeField] private GameObject bodyPrefab;
    [SerializeField] private int gap;

    private List<GameObject> bodyParts = new List<GameObject>();
    private List<Vector3> posHistory = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        GrowSnake();
        GrowSnake();
        GrowSnake();
        GrowSnake();
        GrowSnake();
        GrowSnake();
    }

    // Update is called once per frame
    void Update()
    {
        // move head foward
        transform.position += transform.forward * moveSpeed * Time.deltaTime;

        // controls
        float steerDirection = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * steerDirection * steerSpeed * Time.deltaTime);

        // store
        posHistory.Insert(0, transform.position);

        // move body
        int index = 0;
        foreach(var body in bodyParts)
        {
            Vector3 point = posHistory[Mathf.Min(index * gap, posHistory.Count - 1)];
            Vector3 moveDirection = point - body.transform.position;
            body.transform.position += moveDirection * moveSpeed * Time.deltaTime;
            
            index++;
        }

    }

    private void GrowSnake()
    {
        GameObject body = Instantiate(bodyPrefab);

        bodyParts.Add(body);
    }
}
