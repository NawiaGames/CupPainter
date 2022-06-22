using UnityEngine;

public class LimitPositionBrush
{
    private float _middleX;
    private float _rightBorderX;
    private float _upBorderY;
    private float _downOrderY;

    public LimitPositionBrush(float middleX, float rightBorderX, float upBorderY, float downOrderY)
    {
        _middleX = middleX;
        _rightBorderX = rightBorderX;
        _upBorderY = upBorderY;
        _downOrderY = downOrderY;
    }

    public Vector3 Limit(Vector3 position)
    {
        if (position.x < _middleX)
            position.x = _middleX;

        if (position.x > _rightBorderX)
            position.x = _rightBorderX;

        if (position.y > _upBorderY)
            position.y = _upBorderY;

        if (position.y < _downOrderY)
            position.y = _downOrderY;
        
        return position;
    }
}
