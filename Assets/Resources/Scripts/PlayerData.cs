using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player", order = 51)]
public class PlayerData : ScriptableObject
{
    [SerializeField] private uint _id;
    [SerializeField] private float _weaponZoom;
    [SerializeField] private float _aimShake; 
    public float WeaponZoom => _weaponZoom;
    public float AimShake => _aimShake;
    private float _holdingTime = .0f;
    private float _zoom = .0f;
    private float _amplitudeGian = 10.0f;
}
