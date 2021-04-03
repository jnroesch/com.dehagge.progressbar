using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Packages.com.dehagge.progressbar.Runtime
{
    [RequireComponent(typeof(Slider))]
    public class SliderProgressBar : ProgressBarBase
    {
        private Slider _slider;

        public override float MinFillAmount
        {
            get => _slider.minValue;
            set => _slider.minValue = value;
        }

        public override float MaxFillAmount
        {
            get => _slider.maxValue;
            set => _slider.maxValue = value;
        }

        public override float CurrentFillAmount
        {
            get => _slider.value;
            set => SetFillAmountImmediate(value);
        }

        private void Awake()
        {
            _slider = GetComponent<Slider>();
        }

        public override float GetCurrentFillPercentage()
        {
            return _slider.value / _slider.maxValue * 100;
        }

        public override void SetFillAmountImmediate(float fillAmount)
        {
            _slider.value = Mathf.Clamp(fillAmount, MinFillAmount, MaxFillAmount);
            
            if (_slider.value >= MaxFillAmount)
            {
                OnMaxFillReached(null);
            }
            else if (_slider.value <= MinFillAmount)
            {
                OnMinFillReached(null);
            }
        }

        public override void SetFillAmountTween(float fillAmount, float time, Ease easeType)
        {
            var newValue = Mathf.Clamp(fillAmount, MinFillAmount, MaxFillAmount);
            
            _slider.DOValue(newValue, time).SetEase(easeType).OnComplete(() =>
            {
                if (_slider.value >= MaxFillAmount)
                {
                    OnMaxFillReached(null);
                }
                else if (_slider.value <= MinFillAmount)
                {
                    OnMinFillReached(null);
                }
            });
        }

        public override void IncreaseFillAmountImmediate(float amount)
        {
            if (amount <= 0) return;

            _slider.value = Mathf.Clamp(_slider.value + amount, MinFillAmount, MaxFillAmount);

            if (_slider.value >= MaxFillAmount)
            {
                OnMaxFillReached(null);
            }
        }

        public override void IncreaseFillAmountTween(float amount, float time, Ease easeType)
        {
            if (amount <= 0) return;

            var newValue = Mathf.Clamp(_slider.value + amount, MinFillAmount, MaxFillAmount);
            
            _slider.DOValue(newValue, time).SetEase(easeType).OnComplete(() =>
            {
                if (_slider.value >= MaxFillAmount)
                {
                    OnMaxFillReached(null);
                }
            });
        }

        public override void DecreaseFillAmountImmediate(float amount)
        {
            if (amount <= 0) return;

            _slider.value = Mathf.Clamp(_slider.value - amount, MinFillAmount, MaxFillAmount);
            
            if (_slider.value <= MinFillAmount)
            {
                OnMinFillReached(null);
            }
        }

        public override void DecreaseFillAmountTween(float amount, float time, Ease easeType)
        {
            if (amount <= 0) return;
            
            var newValue = Mathf.Clamp(_slider.value - amount, MinFillAmount, MaxFillAmount);
            
            _slider.DOValue(newValue, time).SetEase(easeType).OnComplete(() =>
            {
                if (_slider.value <= MinFillAmount)
                {
                    OnMinFillReached(null);
                }
            });
        }
    }
}
