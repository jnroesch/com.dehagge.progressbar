using System;
using DG.Tweening;
using UnityEngine;

namespace Packages.com.dehagge.progressbar.Runtime
{
    public abstract class ProgressBarBase : MonoBehaviour
    {
        public virtual float MaxFillAmount { get; set; }
        public virtual float MinFillAmount { get; set; }
        public virtual float CurrentFillAmount { get; set; }
        
        public event EventHandler MaxFillReached;
        public event EventHandler MinFillReached;

        protected virtual void OnMaxFillReached(EventArgs args)
        {
            MaxFillReached?.Invoke(this, args);
        }
        
        protected virtual void OnMinFillReached(EventArgs args)
        {
            MinFillReached?.Invoke(this, args);
        }
        
        public abstract float GetCurrentFillPercentage();

        public abstract void SetFillAmountImmediate(float fillAmount);

        public abstract void SetFillAmountTween(float fillAmount, float time, Ease easeType);

        public abstract void IncreaseFillAmountImmediate(float amount);
        
        public abstract void IncreaseFillAmountTween(float amount, float time, Ease easeType);
        
        public abstract void DecreaseFillAmountImmediate(float amount);
        
        public abstract void DecreaseFillAmountTween(float amount, float time, Ease easeType);
    }
}