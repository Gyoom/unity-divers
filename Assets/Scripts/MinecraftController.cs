using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinecraftController : MonoBehaviour
{
    [SerializeField] Vector2 cameraSpeed;
    [SerializeField] float moveSpeed;
    [SerializeField] GameObject blockPrefab;
    [SerializeField] GameObject blockOutline;

    private float pitch = 0;
    private float yaw = 0;
    private RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // camera rotation
        pitch += -Input.GetAxis("Mouse Y") * cameraSpeed.x * Time.deltaTime;
        yaw += Input.GetAxis("Mouse X") * cameraSpeed.y * Time.deltaTime;

        Camera.main.transform.eulerAngles = new Vector3(pitch, yaw, 0);

        // camera Move
        float InputX = Input.GetAxis("Horizontal");
        float InputY = Input.GetAxis("Fly");
        float InputZ = Input.GetAxis("Vertical");

        Vector3 foward = Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up).normalized;
        Vector3 side = Camera.main.transform.right;
        Vector3 up = Vector3.up;

        Vector3 move = (InputX * side) + (InputY * up) + (InputZ * foward);

        Camera.main.transform.position += move * moveSpeed * Time.deltaTime;

        // camera ray cast
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);


        // mouse action
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 pos = hit.point;

            pos += hit.normal * 0.1f;

            pos = new Vector3(
                Mathf.Floor(pos.x),
                Mathf.Floor(pos.y) + 0.5f,
                Mathf.Floor(pos.z)
            );

            blockOutline.transform.position = pos;

            // create
            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(blockPrefab, pos, Quaternion.identity);
            }
            // destroy
            else if (Input.GetMouseButtonDown(1)) 
            {
                if (hit.collider.name == "BlockGrass(Clone)")
                    Destroy(hit.collider.gameObject);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * 9999);

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(hit.point, 0.05f);

        Gizmos.DrawRay(hit.point, hit.normal);
    }
}
