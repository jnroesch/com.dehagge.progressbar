using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Packages.com.dehagge.progressbar.Runtime
{
    public class TestScript : MonoBehaviour
    {
        private Image _image;
        private void Start()
        {
            _image.DOFade(0, 3);
        }
    }
}
