using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;

public class Player2 : NetworkBehaviour
{
    public float moveSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Makes it so the other player cant use this paddle
        if (!IsOwner)
        {
            return;
        }
      
        if (Keyboard.current.upArrowKey.isPressed)
        {
            transform.Translate(Vector2.up * Time.deltaTime * moveSpeed);
        }
        if (Keyboard.current.downArrowKey.isPressed)
        {
            transform.Translate(Vector2.down * Time.deltaTime * moveSpeed);
        }

    }
}
