using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class NoteTextureManager
{
    private static readonly Sprite Unset = Sprite.Create(Texture2D.whiteTexture, new Rect(0, 0, Texture2D.whiteTexture.width, Texture2D.whiteTexture.height), new Vector2(0, 0));
    public static GameObject NoteTemplate { get; private set; }

    public static Sprite Tap { get; private set; } = Unset;
    public static Sprite TapMultiPress { get; private set; } = Unset;
    public static Sprite Drag { get; private set; } = Unset;
    public static Sprite DragMultiPress { get; private set; } = Unset;
    public static Sprite Flick { get; private set; } = Unset;
    public static Sprite FlickMultiPress { get; private set; } = Unset;
    public static Sprite TapFlick { get; private set; } = Unset;
    public static Sprite TapFlickMultiPress { get; private set; } = Unset;
    public static Sprite Hold { get; private set; } = Unset;
    public static Sprite HoldMultiPress { get; private set; } = Unset;

    public static Dictionary<int, Sprite> TextureOverride { get; set; } = new();

    public static void Initialize(Sprite tap, Sprite tapMl, Sprite drag, Sprite dragMl, Sprite flick, Sprite flickMl, Sprite TF, Sprite TFMl, Sprite hold, Sprite holdMl)
    {
        TextureOverride.Clear();
        Tap = tap;
        TapMultiPress = tapMl;
        Drag = drag;
        DragMultiPress = dragMl;
        Flick = flick;
        FlickMultiPress = flickMl;
        TapFlick = TF;
        TapFlickMultiPress = TFMl;
        Hold = hold;
        Hold = holdMl;
    }
    public static Sprite GetSpriteForNote(in NoteInternalFormat note)
    {
        if (note.UseOverrideTexture)
        {
            return TextureOverride.ContainsKey(note.Id) ? TextureOverride[note.Id] : throw new Exception(string.Format("NoteInternalFormat id {0} do not have texture override registered!", note.Id.ToString()));
        }
        switch ((note.RequireTap, note.RequireFlick, note.MultiPress, note.HoldTime > 0))
        {
            case (false, false, false, false):
                return Drag;
            case (false, false, true, false):
                return DragMultiPress;
            case (false, true, false, false):
                return Flick;
            case (false, true, true, false):
                return FlickMultiPress;
            case (true, false, false, false):
                return Tap;
            case (true, false, true, false):
                return TapMultiPress;
            case (true, true, false, false):
                return TapFlick;
            case (true, true, true, false):
                return TapFlickMultiPress;
            case (true, false, false, true):
                return Hold;
            case (true, false, true, true):
                return HoldMultiPress;
            default:
                LogHandler.Log(LogHandler.Warning, ":thonking: hold note with flick required or only touch required found");
                return note.MultiPress ? HoldMultiPress : Hold;
        }
    }
    public static void Reset()
    {
        Tap = Unset;
        TapMultiPress = Unset;
        Drag = Unset;
        DragMultiPress = Unset;
        Flick = Unset;
        FlickMultiPress = Unset;
        TapFlick = Unset;
        TapFlickMultiPress = Unset;
        Hold = Unset;
        HoldMultiPress = Unset;

        TextureOverride.Clear();
    }
}
