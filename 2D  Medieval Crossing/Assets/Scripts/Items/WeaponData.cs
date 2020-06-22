using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "ScriptableObjects/WeaponData")]
public class WeaponData : ItemData
{
    public GameObject projectilePrefab = null;
    public Sprite usedSprite = null;
    public Sprite droppedSprite = null;
    public ParticleSystem shootingParticle = null; //Supprimé si animations faites sur le sprite
    public float fireRate = 0;
}
