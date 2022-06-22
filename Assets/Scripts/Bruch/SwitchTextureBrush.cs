using System.Collections;
using PaintIn3D;
using UnityEngine;

public class SwitchTextureBrush : MonoBehaviour
{
    [SerializeField] private P3dPaintDecal _paintDecal;
    [SerializeField] private Texture[] _textures;
    [SerializeField] private float timerSwitch = 0.1f;
    [SerializeField] private float _sizeRotation = 10f; 

    private int _counterTextur;
    private int _lengthTextur;

    private void Start()
    {
        _lengthTextur = _textures.Length;
        StartCoroutine("SwitchTexture"); 
    }

    private IEnumerator SwitchTexture()
    {
        while (true)
        {
            _counterTextur = (_counterTextur + 1) % _lengthTextur;
            _paintDecal.Texture = _textures[_counterTextur];

            _paintDecal.Angle += _sizeRotation; 
            yield return new WaitForSeconds(timerSwitch);
            if (_paintDecal.Angle >= 180)
                _paintDecal.Angle = -180;
        }
    }
}
