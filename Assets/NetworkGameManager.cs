using UnityEngine;
using Unity.Netcode;
using TMPro;

public class NetworkGameManager : NetworkBehaviour
{
    //NetworkVariable syncs scores to all clients when the server changes
    private NetworkVariable<int> player1Score = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    private NetworkVariable<int> player2Score = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);

    public TextMeshProUGUI player1Scoreboard;
    public TextMeshProUGUI player2Scoreboard;
  


    public override void OnNetworkSpawn()
    {
        //updates score on all clients
        player1Score.OnValueChanged += OnPlayer1ScoreChanged;
        player2Score.OnValueChanged += OnPlayer2ScoreChanged;

        NetworkManager.OnClientConnectedCallback += OnClientConnect;


    }

    void OnPlayer1ScoreChanged(int oldVal, int newVal)
    {
        player1Scoreboard.text = newVal.ToString();
    }
    void OnPlayer2ScoreChanged(int oldVaL, int newVal)
    {
        player2Scoreboard.text = newVal.ToString();
    }

    public void Player1Scores()
    {
        //only server can change the score
        if (!IsServer)
        {
            return;
        }
        player1Score.Value++;
    }

    public void Player2Scores()
    {
        if (!IsServer)
        {
            return;
        }
        player2Score.Value++;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player1Scoreboard.text = "0";
        player2Scoreboard.text = "0";
        
   
        
        
    }

    void OnClientConnect(ulong client)
    {
        if (!IsServer)
        {
            return;
        }
        //This is to ensure the new client is 1
        if(client == 1)
        {
            GameObject paddle2 = GameObject.Find("Paddle2");
            if(paddle2 != null)
            {
                //Transfer ownership of paddle 2 to the client
                paddle2.GetComponent<NetworkObject>().ChangeOwnership(client);
            }
            GameObject BallObj = GameObject.Find("Ball");
            if(BallObj != null)
            {
                BallObj.GetComponent<Ball>().StartG();
            }
        }
    }
}
