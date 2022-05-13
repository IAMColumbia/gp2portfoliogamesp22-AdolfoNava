using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PlayerWeapon { Melee,Ranged }
public class Player : MonoBehaviour
{
    public PlayerWeapon playerWeapon;
    public Inventory inventory;
    public Inventory equipment;

    public Transform Weapon;
    
    public Transform Ranged;
    public Transform ProjectileStartingPoint;
    public GameObject RangedProjectile;
    
    private MainControl inputs;
    private Rigidbody2D rb;
    
    public float speed = 5f;
    private Vector2 MovementValue;
    public int Health = 10;
    public int MeleeDamage;
    public int RangedDamage;
    public int WeaponAttackCooldown = 30;
    public bool AttackAction;
    public static bool Alive = true;
    private void Awake()
    {
        inputs = new MainControl();
    }
    private void Start()
    {        
        rb = GetComponent<Rigidbody2D>();
        inventory.Load();
    }
    void FixedUpdate()
    {
        if (Health <= 0)
        {
            Alive = false;
            this.gameObject.SetActive(false);
        }
        if(Ranged.gameObject.activeInHierarchy && playerWeapon == PlayerWeapon.Ranged)
        RotateRangedWeapon();
        else if(Weapon.gameObject.activeInHierarchy&& playerWeapon == PlayerWeapon.Melee)
        RotateWeapon();
        rb.velocity = new Vector2(MovementValue.x * speed,MovementValue.y * speed);
        if (WeaponAttackCooldown < 0 && AttackAction &&playerWeapon == PlayerWeapon.Melee) {
            Aiming();

            WeaponAttackCooldown = 30;

        }
        else if (WeaponAttackCooldown < 0 && AttackAction && playerWeapon==PlayerWeapon.Ranged)
        {
            ShootGun();
            WeaponAttackCooldown = 30;

        }
        else
        {

            WeaponAttackCooldown--;
        }
        if (WeaponAttackCooldown <= 0)
        {
            Weapon.gameObject.SetActive(false);
        }
    }
    public void SwitchActiveWeapon()
    {
        if (playerWeapon==PlayerWeapon.Melee)
        {
            playerWeapon = PlayerWeapon.Ranged;
            Ranged.gameObject.SetActive(true);
            //Weapon.gameObject.SetActive(false);
        }
        else if(playerWeapon == PlayerWeapon.Ranged)
        {
            playerWeapon = PlayerWeapon.Melee;
            Ranged.gameObject.SetActive(false);
        }
    }
    private void RotateWeapon()
    {
        float angle = AngleTowardsMouse(Weapon.position);
        Weapon.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    public void Movement(Vector2 value)
    {
        MovementValue = value;
    }
    public void Attack()
    {
        if(WeaponAttackCooldown<0)
        AttackAction = true;
        
    }
    void ShootGun()
    {
        Vector3 targetPos = Ranged.position;
        Vector3 ShootingDirection = ProjectileStartingPoint.position -targetPos;
        GameObject bullet = Instantiate(RangedProjectile, ProjectileStartingPoint.transform.position,Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().AddForce(ShootingDirection * speed * 125);
        //Transfer stats of ranged weapon here
        AttackAction = false;
    }
    private void Aiming()
    {
        Vector2 AimingPosition = inputs.Gameplay.Aim.ReadValue<Vector2>();
        Vector3 AimWorldPosition = Camera.main.ScreenToWorldPoint(AimingPosition);
        Vector3 targetDirection = AimWorldPosition - transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        Weapon.gameObject.SetActive(true);
        Weapon.rotation = Quaternion.Euler(0, 0, angle);
        //Weapon.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        Weapon.transform.localPosition = AimWorldPosition.normalized*2;
        //Invoke("WeaponSwing",0f);
        AttackAction = false;

    }
    void RotateRangedWeapon()
    {
        float angle = AngleTowardsMouse(Ranged.position);
        Ranged.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        Ranged.localPosition = new Vector2(Input.mousePosition.normalized.x, Input.mousePosition.normalized.y);
    }
    float AngleTowardsMouse(Vector3 pos)
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 5.23f;
        Vector3 objectPos = Camera.main.WorldToScreenPoint(pos);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        return angle;
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        var item = other.GetComponent<ItemPickup>();
        if(item != null)
        {
            ItemContent itemNew = new ItemContent(item.item);
            inventory.AddItem(new ItemContent(item.item), 1);
            other.gameObject.SetActive(false);
        }
        
    }
    private void OnApplicationQuit()
    {
        //inventory.Save();
        //inventory.InventoryCollection.Clear();
        //inventory.Container.InventoryItems.;
        inventory.Clear();
        equipment.Clear();
    }
    private void OnEnable()
    {
        inputs.Enable();
    }
    private void OnDisable()
    {
        inputs.Disable();
    }
}
