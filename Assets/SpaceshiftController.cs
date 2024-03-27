using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshiftController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float minY, maxY;
    [SerializeField] private GameObject playerLaser;
    [SerializeField] private Transform playerShooter1;
    [SerializeField] private Transform playerShooter2;

    private bool shooterSelected = false;
    private float shoots = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    void MovePlayer() 
    { 
        if (Input.GetAxis("Vertical") > 0f &&
            transform.position.y < maxY)
        {
            Vector3 temp = transform.position;
            temp.y += speed * Time.deltaTime;

             transform.position = temp;

        }   else if (Input.GetAxis("Vertical") < 0f &&
                     transform.position.y > minY)
        {
            Vector3 temp = transform.position;
            temp.y -= speed * Time.deltaTime;

            transform.position = temp;
        }

        if (Input.GetMouseButtonDown(0) && shoots < 6)
        {
            Vector3 temp;
            SpriteRenderer sp;
            if (shooterSelected)
            {
                temp = playerShooter1.position;
                sp = playerShooter1.GetComponent<SpriteRenderer>();
            }     
            else 
            {
                temp = playerShooter2.position;
                sp = playerShooter2.GetComponent<SpriteRenderer>();
            }
            sp.enabled = true;
            temp.x += 1;
            Instantiate(playerLaser, temp, playerLaser.transform.rotation);
            shooterSelected = !shooterSelected;
            shoots++;

            StartCoroutine(StopFlash(sp));
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("mobLaser"))
        {
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die() 
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
        yield return new WaitForSeconds(1);
        Application.Quit();
    }

    private IEnumerator StopFlash(SpriteRenderer sp) 
    {
        yield return new WaitForSeconds(0.1f);
        sp.enabled = false;  
        yield return new WaitForSeconds(2f);
        shoots--;
    }
}
