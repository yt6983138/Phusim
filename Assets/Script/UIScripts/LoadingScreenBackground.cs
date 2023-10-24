using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;


// UNUSED

public class LoadingScreenBackgroundImage : MonoBehaviour
{
    //public Sprite Background;
    //public Image Background;
    public GameObject AssignedObject;
    private readonly int delay = 1;
    private Texture2D LoadingBackground;
    void Start()
    {
        LoadingBackground = new Texture2D(1, 1);
    }
    void OnGUI()
    {
        for (float i = 0; i < 1; i += 1 / 256)
        {
            //Color[] =  UIUtils.HSL2RGB(i, 0.5f, 0.5f);
            Thread.Sleep(delay);
            LoadingBackground.SetPixel(0, 0, UIUtils.HSL2RGB(i, 0.5f, 0.5f));
            AssignedObject.GetComponent<Image>().sprite = Sprite.Create(LoadingBackground, new Rect(0, 0, 1, 1), new Vector2(0, 0), 100);
        }
        for (float i = 1; i > 0; i -= 1 / 256)
        {
            //Color[] =  UIUtils.HSL2RGB(i, 0.5f, 0.5f);
            Thread.Sleep(delay);
            LoadingBackground.SetPixel(0, 0, UIUtils.HSL2RGB(i, 0.5f, 0.5f));
            AssignedObject.GetComponent<Image>().sprite = Sprite.Create(LoadingBackground, new Rect(0, 0, 1, 1), new Vector2(0, 0), 100);
        }
        //AssignedObject.GetComponent<Image>()
    }
}
