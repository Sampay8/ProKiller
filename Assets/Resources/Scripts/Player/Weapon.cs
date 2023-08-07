using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Aim))]

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject _bulletTemplate;
    [SerializeField] private Aim _aim;

    public Bullet Bullet { get; private set;}
    
    private WaitForSecondsRealtime _delay = new WaitForSecondsRealtime(2.0f);

    public void DoShoot(Vector3 target) => StartCoroutine(ShootRoutine(target));


    public void Init()
    {
        if (_aim == null)
            _aim = gameObject.GetComponent<Aim>();
    }

    private void CreateBullet()
    {
        Bullet = Instantiate(_bulletTemplate).GetComponent<Bullet>();
        Bullet.gameObject.name = "Bullet";
    }

    private IEnumerator ShootRoutine(Vector3 target)
    {
        if (Bullet == null)
            CreateBullet();

        EventBus.Current.Invoke<WeaponOnShoot>(new WeaponOnShoot());

        Bullet.transform.position = this.transform.position;
        yield return _delay;
        Bullet.DoShot(target);
    }
}