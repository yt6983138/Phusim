using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class EffectProcesser
{
    public Texture2D TextureToProcess { get; private set; }
    public List<IEffectProcesser> EffectsToApply { get; private set; }
    public EffectProcesser(Texture2D tex, List<IEffectProcesser> effects)
    {
        this.TextureToProcess = tex;
        this.EffectsToApply = effects;
    }
    public Texture2D ApplyEffects()
    {
        foreach (IEffectProcesser effect in EffectsToApply)
        {
            effect.ProcessTexture(this.TextureToProcess);
        }
        return this.TextureToProcess;
    }

}
