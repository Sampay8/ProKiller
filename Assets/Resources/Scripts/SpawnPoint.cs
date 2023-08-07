using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private void Start()
    {
        Init();
    }

    private void Init()
    {
        var _player = FindObjectOfType<Player>();
        _player.transform.position = this.transform.position;
    }
}
