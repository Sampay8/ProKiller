using UnityEngine;
using UnityEngine.UI;

public class Window : MonoBehaviour
{
    [SerializeField] private Button _outSideButton;

    private void Start()
    {
        _outSideButton?.onClick.AddListener(Hide);
    }
    public void Hide()
    { 
        _outSideButton?.onClick.RemoveAllListeners();
        Destroy(gameObject);
    }
}
