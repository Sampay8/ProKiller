#region MenuSignals

using UnityEngine;

public class MapSignal { }
public class ReitingsSignal { }
public class SettingsSignal { }
public class ShopSignals { }
public class GoToMenuSignal { }
#endregion


public class WeaponIsReadySignal 
{
    public readonly bool State;
    public WeaponIsReadySignal(bool value = false)
    { 
        State = value;
    }
}
public class WeaponOnShoot { }
public class WeaponCompletteShoot { }
public class EscortBulletComlette { }

public class LevelIsPausedSignal{}
public class LevelIsPlayedSignal { }

public class PreViewLevelSignal 
{
    public readonly Sprite Sprite;

    private Level _levelToLoad;
    public PreViewLevelSignal(Level level)
    {
        _levelToLoad = level;
        Sprite = level.PreView;
    }

    public Level GetLevel()
    {
        return _levelToLoad;
    }
}
public class ShowCurtainSignal { }
public class HideCurtainSignal { }
