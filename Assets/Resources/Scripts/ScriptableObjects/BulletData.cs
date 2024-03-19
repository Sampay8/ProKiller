using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "newBullet", menuName = "Data/Bullet", order = 52)]
public class BulletData : ScriptableObject
{
    [SerializeField] private GameObject _template;
    [SerializeField] public readonly float Damage;
    public  GameObject Template => _template;

}
