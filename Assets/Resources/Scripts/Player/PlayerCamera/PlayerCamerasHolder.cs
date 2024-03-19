using Cinemachine;
using System;
using System.Collections;
using UnityEngine;
using Zenject;

public partial class PlayerCamerasHolder : MonoBehaviour
{
    public event Action StalkingBegined;
    public event Action StalkingCompletted;

 

    [SerializeField] private Camera _camera;
    [SerializeField] private CinemachineVirtualCamera _idle;
    [SerializeField] private CinemachineVirtualCamera _back;
    [SerializeField] private CinemachineVirtualCamera _side;
    [SerializeField] private CinemachineVirtualCamera _stalker;
    [SerializeField] private StalkerCM _stalkerGO;
    [SerializeField] private AimVizor _aimVizor;
    private CinemachineVirtualCamera _cur;

    private Vector3 _sideStartPosition;

    private void Awake()
    {
        _camera ??= GetComponentInChildren<Camera>();
    }
    private void Start()
    {
        gameObject.SetActive(false);
        _sideStartPosition = _side.gameObject.transform.localPosition;
        ChangeVirtualCamera(_idle);

        _cameraFSM = new CameraFSM(this, _idle,_back, _side, _stalker);
    }

    private CameraFSM _cameraFSM;
    public event Action PressedI;
    public event Action PressedS;
    public event Action PressedB;
    public event Action PressedF;
    public event Action PressedV;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) 
            PressedI?.Invoke();
        
        if (Input.GetKeyDown(KeyCode.S))
            PressedS?.Invoke();
           
        if (Input.GetKeyDown(KeyCode.B))
            PressedB?.Invoke();

        if (Input.GetKeyDown(KeyCode.F))
            PressedF?.Invoke();
        
        if (Input.GetKeyDown(KeyCode.V))
            PressedV?.Invoke();
           
    }




    //private void OnEnable()
    //{
    //    _aimVizor.HeadShooted += EscortBullet;
    //}

    //private void EscortBullet() => StartCoroutine(Escort(_bullet.transform));

    //private void OnDisable()
    //{
    //    _aimVizor.HeadShooted -= EscortBullet;
    //}

    private void ChangeVirtualCamera(CinemachineVirtualCamera next)
    {
        if (_cur != null)
            _cur.gameObject.SetActive(false);

        _cur = next;
        _cur.gameObject.SetActive(true);
    }

    //private IEnumerator Escort(Transform target)
    //{
    //    StalkingBegined?.Invoke();
    //    _stalkerGO.Init(_bullet);

    //    while (target.gameObject.activeInHierarchy == false)
    //        yield return null;

    //    yield return new WaitForSeconds(1.0f);

    //    while (_stalkerGO.IsFollow)
    //        yield return null;

    //    yield return new WaitForSeconds(2.5f);
    //    ChangeVirtualCamera(_back);
    //    yield return new WaitForSeconds(1.0f);
    //    ChangeVirtualCamera(_idle);
    //    EventBus.Current.Invoke(new FollowCompletteSignal());
    //    StalkingCompletted?.Invoke();
    //}

}