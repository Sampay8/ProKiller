using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_view 
{
    private GameObject _player;
    private WeaponUI _ui;

    public Player_view(GameObject player, WeaponUI ui)
    {
        _player = player;
        _ui = ui;

    }
}
