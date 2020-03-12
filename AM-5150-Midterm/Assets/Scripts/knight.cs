using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class knight : MonoBehaviour
{
    [SerializeField]
   private GameObject coinss;
   
    Animator anim;
    private bool KillDragon = false;
    private int coins = 0;
    private float move;
    private float move2;
    private float maxSpeedx = 2f;
    private float maxSpeedy = 2f;
    private Rigidbody2D rb;
    private float jump = 70f;
    //using UnityEngine.SceneManagement;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coinsFunction();
        coinsFunction();
        //timer for the game,if player doesen't reach t he goal in 50 seconds he will lose
        StartCoroutine("counter"); }
 
    
    void Update()
    {
        
        //press right aroow for walking , R for moving the sword and kill the dragon
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            anim.SetInteger("State", 1);
        }

        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            anim.SetInteger("State", 0);
        }

        //press R for runing, release w for idle
        if (Input.GetKeyDown(KeyCode.R))
        {
            anim.SetInteger("State", 2);
            KillDragon = true;
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            anim.SetInteger("State", 0);
          
        }

        move = Input.GetAxis("Horizontal");
        move2 = Input.GetAxis("Vertical");
    }
    
    private void FixedUpdate()
    
    { //press space for jumping
        rb.velocity = new Vector2(move * maxSpeedx, move2 * maxSpeedy);    
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(move * maxSpeedx, rb.velocity.y);
            rb.velocity = new Vector2(rb.velocity.x +jump, rb.velocity.y + jump);
        } 
    }

   

    private void OnTriggerEnter2D(Collider2D collision)                                                                 
    {                   
        //if the knigh touches the fire you will lose
        if (collision.gameObject.tag == "fire")                                                                         
        { SceneManager.LoadScene("Lose"); 
        }
    }
    
    
    //time for the game
    IEnumerator counter()
    {
        yield return new WaitForSecondsRealtime(50);
        SceneManager.LoadScene("Lose"); 

    }

    //Random coins falling from the sky appears at level 2
    private void coinsFunction()
    {
        bool coinsSpawned = false;
        while (!coinsSpawned)
        {
            Vector3 coinsposition = new Vector3(Random.Range(-7f,2f),Random.Range(5f,4f),0f);
            if((coinsposition - transform.position).magnitude <3)
            {
                continue;
            }
            else
            {
                Instantiate(coinss, coinsposition, Quaternion.identity);
                coinsSpawned = true;
            } } }
    
    private void OnCollisionEnter2D(Collision2D collision)

        //if the knight touches the powerup, power app will disappear and coins will be increased by 1 with each coin
    { if (collision.gameObject.tag == "powerup")
        {
            Destroy (GameObject.FindWithTag("powerup"));
            coins = coins + 1; }
        
        //if the knight touches the princess, you win.
        if (collision.gameObject.tag == "princess")
        { SceneManager.LoadScene("Win"); }
        
        //to kill the dragon, you must collect at least 2 powerups and press R to fight with sword, if you press (R) "KillDragon" will be true.
        if (collision.gameObject.tag == "dragon" && coins>=2 && KillDragon==true)  
        { Destroy (GameObject.FindWithTag("dragon")); }
        
        //this the end of level 1, once the knight kills the dragon and enter the door he will move to level2
        if (collision.gameObject.tag == "door") {
            SceneManager.LoadScene("Level2"); }
      

    }
    
}