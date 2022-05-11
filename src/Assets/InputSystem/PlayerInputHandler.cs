using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{
    Player player;
    //PlayerInput input;
    private void Start()
    {
        player = GetComponent<Player>();
        //input = GetComponent<PlayerInput>();
    }
    public void Movement(CallbackContext context)
    {
        player.Movement(context.ReadValue<Vector2>());
    }
    public void Attack(CallbackContext context)
    {
        player.Attack();
    }
    public void SwitchWeapon(CallbackContext context)
    {
        player.SwitchActiveWeapon();
    }
}
