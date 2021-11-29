using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D RMrb;
    public int damage = 20;
    public Transform bulletPoint;
    public float bulletSize = .01f;
    
    

    // Start is called before the first frame update
    void Start()
    {
        RMrb.velocity = transform.right * speed;
        
    }



    private void Update()
    {
        
        Collider2D[] pointSearch = Physics2D.OverlapCircleAll(bulletPoint.position, bulletSize);
        foreach (Collider2D point in pointSearch)
        {
            string found = point.gameObject.tag;
            SpriteRenderer dirt = point.gameObject.GetComponent<SpriteRenderer>();
            if (found == "Enemy")
            {
                Destroy(point.gameObject);
                PlayerController.enemyCount();
            }
            if (found == "Dirt" && dirt != null)
            {
                Destroy(gameObject);
            }


        }

    }

    void OnDrawGizmosSelected()
    {
        if (bulletPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(bulletPoint.position, bulletSize);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
        else
        {
            Debug.Log(collision.gameObject.name);
        }

    }
}
