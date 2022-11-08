using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
public class CameraSetting : Singleton<CameraSetting>
{
    [SerializeField] PostProcessVolume volume;
    [SerializeField] Bloom bloom;

    [ColorUsage(true,true)]
    [SerializeField] Color start;
    [ColorUsage(true, true)]
    [SerializeField] Color upgrade;
    [ColorUsage(true, true)]
    [SerializeField] Color die;

    private void Awake()
    {
        volume.profile.TryGetSettings(out bloom);

        Camera camera = GetComponent<Camera>();
        Rect rect = camera.rect;
        float scaleheight = ((float)Screen.width / Screen.height) / ((float)9 / 16);
        float scalewidth = 1f / scaleheight;
        if (scaleheight < 1)
        {
            rect.height = scaleheight;
            rect.y = (1f - scaleheight) / 2f;
        }
        else
        {
            rect.width = scalewidth;
            rect.x = (1f - scalewidth) / 2f;
        }
        camera.rect = rect;
    }

    public void MainPost()
    {
        bloom.color.value = start;
    }
    public void UpgradePost()
    {
        bloom.color.value = upgrade;
    }
    public void DiePost()
    {
        bloom.color.value = die;
    }
}
