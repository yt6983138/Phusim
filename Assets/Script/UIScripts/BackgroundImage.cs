using System.Diagnostics;
using System;
using UnityEngine;
using UnityEngine.UI;


public class BackgroundImage : MonoBehaviour
{
    public GameObject AssignedObject;
    private SkinItems skin;
    void OnEnable()
    {
        skin = Skins.ReadSkin();

        Stopwatch watch = new();
        watch.Start();
        Texture2D blurred = LinearBlur.Blur(
            Skins.LoadTexture(AssignedObject.name),
            (int)skin.Properties[AssignedObject.name]["BlurRadius"],
            (int)skin.Properties[AssignedObject.name]["BlurIteration"],
            (float)skin.Properties[AssignedObject.name]["DownSample"],
            (FilterMode)skin.Properties[AssignedObject.name]["DownSampleMode"]
        );
        watch.Stop();
        LogHandler.Log(
            LogHandler.Verbose, 
            new Exception(
                string.Format(
                    "Blurring background at {0} took {1}ms!", 
                    AssignedObject.name, 
                    watch.ElapsedMilliseconds.ToString()
                )
            )
        );
        AssignedObject.transform.localScale = UIUtils.CalcTextureFitScreen(blurred);
        AssignedObject.GetComponent<RectTransform>().sizeDelta = new Vector2(blurred.width, blurred.height);
        AssignedObject.GetComponent<RawImage>().texture = blurred;
    }
    void OnGUI()
    {
    }
}
