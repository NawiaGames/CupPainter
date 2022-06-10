using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Button3DGameUI : MonoBehaviour
{
    [SerializeField] private Button _buttonSample;
    [SerializeField] private float _speedAnimationMove = 600f;
    [SerializeField] private GameObject _bigSample;
    [SerializeField] private ComparisonTexture _comparisonTexture;

    private RectTransform _bigSampleRectTransform;
    private Vector3 _startPositionBigSample;
    private Vector3 _directionBigSample;
    private RectTransform _buttonSampleRectTransform;
    private Vector3 _startPositionButtonSample;
    private Vector3 _directionButtonSample;

    private float _pathLengthButtonSample = 200f;
    private float _pathLengthBigSample = 530f;
    
    

    private bool _onClickButtonSample;

    private void Start()
    {
        _buttonSampleRectTransform = _buttonSample.gameObject.GetComponent<RectTransform>();
        _startPositionButtonSample = _buttonSampleRectTransform.localPosition; 
        _directionButtonSample = _buttonSampleRectTransform.localPosition + (Vector3.left * _pathLengthButtonSample);

        _bigSampleRectTransform = _bigSample.GetComponent<RectTransform>();
        _startPositionBigSample = _bigSampleRectTransform.localPosition;
        _directionBigSample = _bigSampleRectTransform.localPosition + (Vector3.right * _pathLengthBigSample);
    }

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0) || !_onClickButtonSample) return;
        StarMoveCoroutines(_startPositionButtonSample, _startPositionBigSample);
    }

    public void OnButtonSample()
    {
        StarMoveCoroutines(_directionButtonSample, _directionBigSample);
    }

    private void StarMoveCoroutines (Vector3 directionButtonSample, Vector3 directionBigSample)
    {
        _onClickButtonSample = !_onClickButtonSample; 
        StopCoroutine("MoveButtonSample");
        StartCoroutine(MoveButtonSample(directionButtonSample));

        StopCoroutine("MoveBigSample");
        StartCoroutine(MoveBigSample(directionBigSample));
    }

    private IEnumerator MoveButtonSample(Vector3 direction)
    {
        while (_buttonSampleRectTransform.localPosition != direction)
        {
            _buttonSampleRectTransform.localPosition = Vector3.MoveTowards(_buttonSampleRectTransform.localPosition,
                direction, _speedAnimationMove * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator MoveBigSample(Vector3 direction)
    {
        while (_bigSampleRectTransform.localPosition != direction)
        {
            _bigSampleRectTransform.localPosition = Vector3.MoveTowards(_bigSampleRectTransform.localPosition,
                direction, _speedAnimationMove * 3f * Time.deltaTime);
            yield return null;
        }

        if (direction != _directionBigSample)
            _comparisonTexture.OnEnableText();
        else
            _comparisonTexture.ComparisonPixelDrawing();
    }
}