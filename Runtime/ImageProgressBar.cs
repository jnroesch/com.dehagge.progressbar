using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Packages.com.dehagge.progressbar.Runtime
{
    [RequireComponent(typeof(Image))]
    public class ImageProgressBar : ProgressBarBase
    {
        private Image _image;

        public override float MinFillAmount => 0;

        public override float MaxFillAmount => 1;

        public override float CurrentFillAmount
        {
            get => _image.fillAmount;
            set => SetFillAmountImmediate(value);
        }

        private void Awake()
        {
            _image = GetComponent<Image>();

            if (_image.type != Image.Type.Filled)
            {
                Debug.LogWarning($"Image type is not set correctly on {name}. Should be filled but is {_image.type.ToString()}");
            }
        }
        
        public override float GetCurrentFillPercentage()
        {
            return _image.fillAmount * 100;
        }

        public override void SetFillAmountImmediate(float fillAmount)
        {
            _image.fillAmount = Mathf.Clamp(fillAmount, MinFillAmount, MaxFillAmount);
            
            if (_image.fillAmount >= MaxFillAmount)
            {
                OnMaxFillReached(null);
            }
            else if (_image.fillAmount <= MinFillAmount)
            {
                OnMinFillReached(null);
            }
        }

        public override void SetFillAmountTween(float fillAmount, float time, Ease easeType)
        {
            var newValue = Mathf.Clamp(fillAmount, MinFillAmount, MaxFillAmount);
            
            _image.DOFillAmount(newValue, time).SetEase(easeType).OnComplete(() =>
            {
                if (_image.fillAmount >= MaxFillAmount)
                {
                    OnMaxFillReached(null);
                }
                else if (_image.fillAmount <= MinFillAmount)
                {
                    OnMinFillReached(null);
                }
            });
        }

        public override void IncreaseFillAmountImmediate(float amount)
        {
            if (amount <= 0) return;

            _image.fillAmount = Mathf.Clamp(_image.fillAmount + amount, MinFillAmount, MaxFillAmount);

            if (_image.fillAmount >= MaxFillAmount)
            {
                OnMaxFillReached(null);
            }
        }

        public override void IncreaseFillAmountTween(float amount, float time, Ease easeType)
        {
            if (amount <= 0) return;

            var newValue = Mathf.Clamp(_image.fillAmount + amount, MinFillAmount, MaxFillAmount);
            
            _image.DOFillAmount(newValue, time).SetEase(easeType).OnComplete(() =>
            {
                if (_image.fillAmount >= MaxFillAmount)
                {
                    OnMaxFillReached(null);
                }
            });
        }

        public override void DecreaseFillAmountImmediate(float amount)
        {
            if (amount <= 0) return;

            _image.fillAmount = Mathf.Clamp(_image.fillAmount - amount, MinFillAmount, MaxFillAmount);
            
            if (_image.fillAmount <= MinFillAmount)
            {
                OnMinFillReached(null);
            }
        }

        public override void DecreaseFillAmountTween(float amount, float time, Ease easeType)
        {
            if (amount <= 0) return;
            
            var newValue = Mathf.Clamp(_image.fillAmount - amount, MinFillAmount, MaxFillAmount);
            
            _image.DOFillAmount(newValue, time).SetEase(easeType).OnComplete(() =>
            {
                if (_image.fillAmount <= MinFillAmount)
                {
                    OnMinFillReached(null);
                }
            });
        }
    }
}