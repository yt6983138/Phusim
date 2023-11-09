using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class NoteManager
{
    private int CurrentID = 0;
    public Dictionary<int, Note> RegisteredNotes { get; } = new();
    public Dictionary<int, Texture2D> Textures { get; } = new();
    private long maxTime = 0;
    private int[] Counter;
    private bool hasComputed = false;

    private int GetID()
    {
        CurrentID += 1;
        return CurrentID;
    }
    public void RegisterNote(ref Note note)
    {
        RegisteredNotes.Add(GetID(), note);
        note.Id = CurrentID;
        if (note.Time > maxTime) { maxTime = note.Time; }
    }
    public void Reset()
    {
        CurrentID = 0;
        RegisteredNotes.Clear();
        Textures.Clear();
        maxTime = 0;
        Counter = Array.Empty<int>();
        hasComputed = false;
    }
    private void ComputeMultiPress()
    {
        if (hasComputed) { return; }
        Counter = new int[maxTime];
        foreach (var note in RegisteredNotes)
        {
            Counter[note.Value.Time]++;
        }
        hasComputed = true;
    }
    public bool CheckMultiPress(int time)
    {
        ComputeMultiPress();
        return Counter[time] > 1;
    }
    /*public void AddTextureOverride(int id, Texture2D texture)
    {
        OverrideTextures.Add(id, texture);
    }*/
    /*public static Texture2D GetTextureForNote(int id, bool flick, bool tap, bool hold, bool multipress)
    {
        if (OverrideTextures.ContainsKey(id)) { return OverrideTextures[id]; }
        switch ((flick, tap, hold)) 
        {
            case (false, true, false):
                return multipress ? TextureTapMultiPress : TextureTap;
            case (false, false, false):
                return multipress ? TextureDragMultiPress : TextureDrag;
            case (false, true, true):
                return multipress ? TextureHoldMultiPress : TextureHold;
            case (true, false, false):
                return multipress ? TextureFlickMultiPress : TextureFlick;
            default:
                throw new Exception("Unknown note type: " + (flick, tap, hold));
        }

    }*/
}
