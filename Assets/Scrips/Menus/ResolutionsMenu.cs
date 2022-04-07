using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionsMenu : MonoBehaviour
{
    public void ChangeTo480p()
    {
        // Switch to 640 x 480 full-screen at 60 hz
        Screen.SetResolution(640, 480, FullScreenMode.ExclusiveFullScreen, 60);
    }

    public void ChangeTo720p()
    {
        // Switch to 640 x 480 full-screen at 60 hz
        Screen.SetResolution(1280, 720, FullScreenMode.ExclusiveFullScreen, 60);
    }

    public void ChangeTo1080p()
    {
        // Switch to 640 x 480 full-screen at 60 hz
        Screen.SetResolution(1920, 1080, FullScreenMode.ExclusiveFullScreen, 60);
    }

    public void ChangeTo1440p()
    {
        // Switch to 640 x 480 full-screen at 60 hz
        Screen.SetResolution(2560, 1440, FullScreenMode.ExclusiveFullScreen, 60);
    }

    public void ChangeTo2160p()
    {
        // Switch to 640 x 480 full-screen at 60 hz
        Screen.SetResolution(3840, 2160, FullScreenMode.ExclusiveFullScreen, 60);
    }
}
