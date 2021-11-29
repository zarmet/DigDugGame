using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    
    public float runSpeed = .5f;
    public float borderBounce = 1.5f;
    private bool m_FacingRight = true;


    public Transform attackPoint;
    public float attackRange = .05f;
    public LayerMask dirtLayers;
    public static int enemies = 4;


    // Update is called once per frame
    public void Update()
    {
        float moveHoriz = Input.GetAxis("Horizontal");
        float moveVerti = Input.GetAxis("Vertical");
        var playerBody = GetComponent<Rigidbody2D>();
        if (enemies == 0)
        {
            SceneSwitcher.youWin();
        }

        if (moveHoriz > 0 && !m_FacingRight)
        {
            // ... flip the player.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (moveHoriz < 0 && m_FacingRight)
        {
            // ... flip the player.
            Flip();
        }


        transform.position += new Vector3(moveHoriz, moveVerti) * Time.deltaTime * runSpeed;

        Collider2D[] dugDirt = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, dirtLayers);
        foreach (Collider2D dirt in dugDirt)
        {
            dirt.GetComponent<dirtBehavior>().Digging(1);
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        

        var border = collision.gameObject.name;

        if (border=="rightBorder")
        {
            transform.position += new Vector3(-borderBounce, 0, 0) * Time.deltaTime * runSpeed;
        }
        if (border == "leftBorder")
        {
            transform.position += new Vector3(borderBounce, 0, 0) * Time.deltaTime * runSpeed;
        }
        if (border == "lowerBorder")
        {
            transform.position += new Vector3(0, borderBounce, 0) * Time.deltaTime * runSpeed;
        }
        if (border == "upperBorder")
        {
            transform.position += new Vector3(0, -borderBounce, 0) * Time.deltaTime * runSpeed;
        }


    }




    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);

    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        transform.Rotate(0f, 180f, 0f);
    }

    public static void enemyCount()
    {
        enemies -= 1;
    }

    public static void gameReset()
    {
        enemies = 4;
    }
}
