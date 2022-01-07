using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ResolutionSetting
{
    public int Width;
    public int Height;
    public FullScreenMode Mode;
}

public class ResolutionManager : MonoBehaviour
{
    public ResolutionSetting[] CycleSettings = new ResolutionSetting[3];
    protected int _currentSetting = 0;

    public void GoToNextResolution()
    {
        _currentSetting++;
        if (_currentSetting >= CycleSettings.Length)
            _currentSetting = 0;

        ChangeResolution(CycleSettings[_currentSetting]);
    }

    public void GoToPreviousResolution()
    {
        _currentSetting--;
        if (_currentSetting < 0)
            _currentSetting = CycleSettings.Length - 1;

        ChangeResolution(CycleSettings[_currentSetting]);
    }

    public void ChangeResolution(ResolutionSetting setting)
    {
        Screen.SetResolution(setting.Width, setting.Height, setting.Mode);
    }
}
