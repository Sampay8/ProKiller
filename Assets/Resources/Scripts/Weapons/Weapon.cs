using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;
using Zenject;
using Zenject.SpaceFighter;

public class Weapon : MonoBehaviour, IWeapon
{
    [SerializeField] private AimVizor _aimVizor;
    [SerializeField] private BulletData _bulletData;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private List<Character> _characters;
    [SerializeField] private List<Transform> _transforms;
    [SerializeField] private List<GameObject> _objects;
    [SerializeField] private GameObject _aimUIPanel;
    [SerializeField] private WeaponUI _weaponUI;

    [SerializeField] private int _bulletValue = 100;
 
    private readonly WaitForSecondsRealtime _delay = new WaitForSecondsRealtime(2.0f);
    private Bullet _bullet;

    public Vector3 ShootPoint => _shootPoint.position;
    
    private void InitBullet(Weapon weapon)
    {
        if (_bullet != null)
            Destroy(_bullet);
        _bullet = Instantiate(_bulletData.Template, Vector3.zero, Quaternion.identity).GetComponent<Bullet>();
    }

    private void OnEnable()
    {  
        _weaponUI.ShotBtnPressed += OnShot;
    }

    private void OnDisable()
    { 
        if(_bullet != null)
            Destroy(_bullet);

        _weaponUI.ShotBtnPressed -= OnShot;
    }

    private bool CanShoot()
    {        
        return _bulletValue > 0; //добавить еще
    }


    private void OnShot()
    {
        if (CanShoot() == false)
        { 
            Debug.LogError("Dont Can shotting");
            return;
        }

        Shoot(_aimVizor.GetTargetHit());
    }



    public void Shoot(RaycastHit[] hits)
    {
        InitBullet(this);
        _bulletValue -= 1;

        List<Character> characters = new List<Character>();
        List<RaycastHit> raycastHits = new List<RaycastHit>();

        for (int i = 0; i < hits.Length; i++)
        {
            Character character = hits[i].transform.GetComponentInParent<Character>();

            if (character != null && character.IsAlive)
            {
                characters.Add(character);
                raycastHits.Add(hits[i]);
            }
        }
        bool hasHeadShoot = CheckHeadshoot(raycastHits.ToArray());
        if (hasHeadShoot)
            LaunchBullet(targetPosirion: hits[0].point);
        else
            for (int i = 0; i < characters.Count; i++)
                characters[i].GetDamage(raycastHits[i].collider, _bullet);
    }

    private bool CheckHeadshoot(RaycastHit[] hits)
    { 
        bool isHas = false;
        for (int i = 0; i < hits.Length; i++)
            if (isHas == false && hits[i].transform.name == Character.HeadName)
                isHas = true;

        return isHas;
    }

    private void LaunchBullet(Vector3 targetPosirion)
    {
        _bullet.transform.position = _shootPoint.position;
        _bullet.SetTarget(targetPosirion);
        _bullet.gameObject.SetActive(true);
    }

    public void Reload()
    {
        throw new NotImplementedException();
    }
}
