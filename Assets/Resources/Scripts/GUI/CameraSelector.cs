public class CameraSelector
{
    private MenuCamera _menu;
    private PlayerCamerasHolder _level;
    private Pause _pausa;

    public CameraSelector(MenuCamera menu, PlayerCamerasHolder level)
    {
        _menu = menu;
        _level = level;
    }

    public void SetMode(bool pausaValue)
    {
        _menu.gameObject.SetActive(pausaValue);
        _level.gameObject.SetActive(!pausaValue);
    }
}