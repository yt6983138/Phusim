using UnityEngine;
using UnityEngine.UI;


public class BackgroundImage : MonoBehaviour
{
    //public Sprite Background; 
    //public Image Background;
    public GameObject AssignedObject;
    void OnEnable()
    {
        AssignedObject.GetComponent<Image>().sprite = Skins.LoadTextureAsSprite(AssignedObject.name);
    }
    void OnGUI()
    {
    }
}
