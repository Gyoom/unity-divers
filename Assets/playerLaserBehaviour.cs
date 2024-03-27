using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class playerLaserBehaviour : MonoBehaviour
{
    [SerializeField] private float speed = 15f;

    private float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 5f)
        {
            Destroy(gameObject);
        }
        transform.position += transform.up * speed * Time.deltaTime;
    }
}
