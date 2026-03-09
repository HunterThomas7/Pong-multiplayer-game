using UnityEngine;
using Unity.Netcode;

public class Ball : NetworkBehaviour
{
    public Rigidbody2D RB;
    public float startSpeed;
    public NetworkGameManager gameManager;
    
    //StartG is called by NetworkGameManager when both users have connected
    public void StartG()
    {

        if (IsServer)
        {
          
            LaunchBall();
        }
    }

   


    void LaunchBall()
    {
        bool isRight = UnityEngine.Random.value >= 0.5f;

        float xVelo = -1f;
        if(isRight == true)
        {
            xVelo = 1f;
        }

        float yVelo = UnityEngine.Random.Range(0.3f, 1f);
        if(UnityEngine.Random.value >= 0.5f)
        {
            yVelo = -yVelo;
        }

        

        RB.linearVelocity = new Vector2(xVelo*startSpeed, yVelo*startSpeed);

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //Only the server runs collision logic
        if (!IsServer)
        {
            return;
        }

        if (col.gameObject.CompareTag("LeftWall"))
        {
            gameManager.Player2Scores();
            ResetBall();
        } else if (col.gameObject.CompareTag("RightWall")){
            gameManager.Player1Scores();
            ResetBall();
        }
    }

    void ResetBall()
    {
        transform.position = Vector2.zero;
        LaunchBall();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
