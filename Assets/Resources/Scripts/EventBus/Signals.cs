#region MenuSignals
using UnityEngine;

public interface ISignal { }


public class ShowMapSignal { }
public class ShowReitingsSignal { }
public class ShowSettingsSignal { }
public class ShowShopSignals { }
public class GoToMenuSignal { }
#endregion



public class OnShootSignal : ISignal { }
public class FollowCompletteSignal : ISignal { }
public class LevelIsLoadedSignal : ISignal { }
public class LevelIsPausedSignal : ISignal { }
public class LevelIsPlayedSignal: ISignal{ }

public class TimeStartSignal : ISignal { }

public class HideMainMenuSignal : ISignal { }
public class BrifingIsFinishSignal { }

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
