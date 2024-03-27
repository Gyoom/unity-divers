using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 10;
    [SerializeField] private Transform ball;
    [SerializeField] private Transform arms;
    [SerializeField] private Transform posOverHead;
    [SerializeField] private Transform posDribble;
    [SerializeField] private Transform target;

    [SerializeField] private bool isBallInHands = true;
    [SerializeField] private bool isBallFliying = false;
    private float t = 0;

    // Update is called once per frame
    void Update()
    {
        // walking
        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        transform.position += direction * moveSpeed * Time.deltaTime;

        transform.LookAt(transform.position + direction);

        if (isBallInHands)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                ball.position = posOverHead.position;
                arms.localEulerAngles = Vector3.right * 180;

                transform.LookAt(target.parent.position);
            }
            else
            {
                ball.position = posDribble.position + Vector3.up * Mathf.Abs(Mathf.Sin(Time.time * 5));
                arms.localEulerAngles = Vector3.right * 0;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isBallInHands = false;
            isBallFliying = true;
            t = 0;
        }

        if (isBallFliying)
        {
            t += Time.deltaTime;
            
            float duration = 0.5f;
            float t01 = t / duration;

            Vector3 a = posOverHead.position;
            Vector3 b = target.position;
            Vector3 pos = Vector3.Lerp(a, b, t01);

            Vector3 arc = Vector3.up * 5 * Mathf.Sin(t01 * 3.14f);

            ball.position = pos + arc;

            if (t01 >= 1)
            {
                isBallFliying = false;
                ball.GetComponent<Rigidbody>().isKinematic = false;
            }

        }
    }  

    void OnTriggerEnter(Collider col)
    {
        if (!isBallInHands && !isBallFliying)
        {
            isBallInHands = true;
            ball.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
