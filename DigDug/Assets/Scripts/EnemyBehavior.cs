using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;


public class EnemyBehavior : MonoBehaviour
{
    public Transform searchPoint;
    public float searchRange = .7f;
    public float patrolSearch = .3f;
    public float moveSpeed = 2f;
    public Transform playerController;
    public float borderBounce = 1.5f;
    public Transform origPoint;
    public Transform destPoint;
    bool moveDirect;
    public int attackTimer = 0;
    public float goTime;
    



    public void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").transform;
        float rando = Random.Range(3f, 9f);
        goTime = rando;

    }
    
    public void Update()
    {
        if (attackTimer >= goTime)
        {
            chasePlayer();
        }
        else
        { 
            patrolMove();
        }
        
  
        
    }

    public void chasePlayer()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector3 playerPosition = playerController.position;
        List<Vector3> tunnels = new List<Vector3>();
        Collider2D[] surroundings = Physics2D.OverlapCircleAll(searchPoint.position, searchRange);
        Collider2D[] pointSearch = Physics2D.OverlapCircleAll(searchPoint.position, patrolSearch);
        List<float> distances = new List<float>();


        foreach (Collider2D i in surroundings)
        {
            SpriteRenderer dirt = i.GetComponent<SpriteRenderer>();
            string player = i.gameObject.tag;
            Vector2 spaceVector = i.transform.position;
            if (dirt==null)
            {
                tunnels.Add(spaceVector);

            }
        }

        foreach (Vector2 tunnel in tunnels)
        {
            float route = Vector3.Distance(tunnel, playerPosition);
            distances.Add(route);
            
        }

        int nextMoveIndex = distances.IndexOf(distances.Min<float>());
        Vector2 nextMove = Vector2.MoveTowards(searchPoint.position, tunnels[nextMoveIndex], moveSpeed * Time.deltaTime);
        rb.MovePosition(nextMove);

        foreach (Collider2D point in pointSearch)
        {
            string found = point.gameObject.tag;
            if (found == "Player")
            {
                SceneSwitcher.gameOver();
            }

        }
    }

    

    void OnDrawGizmosSelected()
    {
        if (searchPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(searchPoint.position, searchRange);

    }
    

    public void patrolMove()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Collider2D[] pointSearch = Physics2D.OverlapCircleAll(searchPoint.position, patrolSearch);
        Vector2 patrolMoveO = Vector2.MoveTowards(searchPoint.position, origPoint.position, moveSpeed * Time.deltaTime);
        Vector2 patrolMoveI = Vector2.MoveTowards(searchPoint.position, destPoint.position, moveSpeed * Time.deltaTime);
        if (moveDirect)
        {
            rb.MovePosition(patrolMoveI);
            foreach(Collider2D point in pointSearch)
            {
                string found = point.gameObject.name;
                if (found == "patrolPoint_1")
                {
                    moveDirect = false;
                    attackTimer += 1;
                }
                else if (found == "Player") 
                {
                    SceneSwitcher.gameOver();
                }

            }
        }
        if(!moveDirect)
        {
            rb.MovePosition(patrolMoveO);
            foreach (Collider2D point in pointSearch)
            {
                string found = point.gameObject.name;
                if (found == "patrolPoint_0")
                {
                    moveDirect = true;
                }
                else if (found == "Player")
                {
                    SceneSwitcher.gameOver();
                }

            }
        }



        //foreach (Collider2D move in pointSearch)
        //{
        //    if (move.gameObject.name == "patrolPoint_1")
        //    {
        //        moveDirect = false;

        //    }
        //}

        //if (moveDirect)
        //{


        //}
        //if (!moveDirect)
        //{
        //    //rb.MovePosition(patrolMoveO);
        //}








        //if (!moveDirect)
        //{


        //    foreach (Collider2D search in pointSearch)
        //    {
        //        if (search.gameObject.name == "patrolPoint_1")
        //        {


        //            Debug.Log(search.gameObject.name);
        //            attackTimer += 1;

        //        }
        //        if (search.gameObject.name == "patrolPoint_0")
        //        {
        //            rb.MovePosition(patrolMoveI);

        //        }

        //    }
        //}

    }

    void OnCollisionEnter2D(Collision2D collision)
    {


        var border = collision.gameObject.name;
        

        if (border == "rightBorder")
        {
            transform.position += new Vector3(-borderBounce, 0, 0) * Time.deltaTime * moveSpeed;
        }
        if (border == "leftBorder")
        {
            transform.position += new Vector3(borderBounce, 0, 0) * Time.deltaTime * moveSpeed;
        }
        if (border == "lowerBorder")
        {
            transform.position += new Vector3(0, borderBounce, 0) * Time.deltaTime * moveSpeed;
        }
        if (border == "upperBorder")
        {
            transform.position += new Vector3(0, -borderBounce, 0) * Time.deltaTime * moveSpeed;
        }


    }
}

