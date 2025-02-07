using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : MonoBehaviour
{
    [Header("Weapon Settings")]
    [SerializeField]
    [Tooltip("Time between firing projectiles")]
    private float cooldown;

    [SerializeField]
    [Tooltip("Projectile spawn point")]
    private Transform spawnPos;

    [SerializeField]
    private ObjectPooler ammoPool;

    private float timer;

    private void Update() {
        timer += Time.deltaTime;
        Mathf.Clamp(timer, 0f, cooldown + 1f); // To prevent the timer from going into infinity
    }

    public void Fire() {
        if (timer < cooldown) { return; }
        timer = 0f;
        GameObject curProjectile = ammoPool.GetPooledObject();
        curProjectile.transform.forward = Camera.main.transform.forward;
        curProjectile.transform.position = spawnPos.position;
        curProjectile.SetActive(true);
    }    
}
