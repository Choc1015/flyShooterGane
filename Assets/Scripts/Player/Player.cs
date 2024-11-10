using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IUpdatable
{
    [SerializeField] float speed = 1f;
    int health;



    private void OnEnable()
    {
        UpdateManager.Instance?.Register(this);
    }

    private void OnDisable()
    {
        UpdateManager.Instance?.Unregister(this);
    }

    public void OnUpdate()
    {
        move();
    }

    void move()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");

        Vector2 playerInput = new Vector2(xInput, yInput);
        playerInput = playerInput.normalized;
        gameObject.transform.Translate(playerInput * speed * Time.deltaTime);
    }
    
}
