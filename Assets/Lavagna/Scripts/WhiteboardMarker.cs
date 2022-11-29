using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WhiteboardMarker : MonoBehaviour
{
    [SerializeField] private Transform _tip;
    [SerializeField] private int _penSize = 5;

    private Renderer _renderer;

    private Color[] _colors;
    private float _tipHeight;

    private RaycastHit _touch;

    private Whiteboard _whiteboard;

    private Vector2 _touchPos;
    private Vector2 _lastTouchPos;

    private Quaternion _lastTouchRot;

    private bool _touchedLastFrame;
    void Start()
    {
        _renderer = _tip.GetComponent<Renderer>();
        _colors = makeCircle(_penSize/2,_penSize/2,_penSize/2);
        //_colors = Enumerable.Repeat(_renderer.material.color, _penSize*_penSize).ToArray();
        _tipHeight = _tip.localScale.y;
    }

    void Update()
    {
        //se stiamo toccando la lavagno cambiamo il render della lavagna
        Draw();
    }

    private void Draw()
    {
        //se tocchiamo qualcosa dalla poszione della punta e si va versp l'alto allora facciamo questo
        if (Physics.Raycast(_tip.position, transform.up, out  _touch, _tipHeight))
        {
            if (_touch.transform.CompareTag("Whiteboard"))
            {
                //se non è in chache la creiamo altimenti passiamo avanti
                if (_whiteboard == null)
                {
                    _whiteboard = _touch.transform.GetComponent<Whiteboard>();
                }

                _touchPos = new Vector2(_touch.textureCoord.x, _touch.textureCoord.y);

                var x = (int)(_touchPos.x * _whiteboard.textureSize.x - (_penSize / 2));
                var y = (int)(_touchPos.y * _whiteboard.textureSize.y - (_penSize / 2));

                //se siamo fuori dalla lavagna smettiamo di disegnare
                if (y < 0 || y > _whiteboard.textureSize.y || x < 0 || x > _whiteboard.textureSize.x )
                {
                    return;
                }

                if (_touchedLastFrame)
                {
                    _whiteboard.texture.SetPixels(x, y, _penSize, _penSize,_colors);

                    for (float f = 0.01f; f < 1.00f; f += 0.01f)
                    {
                        var lerpX = (int)Mathf.Lerp(_lastTouchPos.x, x, f);
                        var lerpY = (int)Mathf.Lerp(_lastTouchPos.y, y, f);
                        
                        _whiteboard.texture.SetPixels(lerpX, lerpY, _penSize, _penSize,_colors);
                    }
                    
                    //bloccare la rotazione quando si interagisce con la lavagna e non farla flippare troppo
                   // transform.rotation = _lastTouchRot;
                    
                    _whiteboard.texture.Apply();
                }

                _lastTouchPos = new Vector2(x, y);
               // _lastTouchRot = transform.rotation;
                _touchedLastFrame = true;
                return;
            }
        }

        _whiteboard = null;
        _touchedLastFrame = false;
    }
    
    private Color[] makeCircle(int centerX,int centerY,int radius)
    {
        int x, y, d, yDiff, threshold, radiusSq;
        Color[,] _colorsTemp = new Color[_penSize,_penSize];
        Color[] _colorTemp = new Color[_penSize*_penSize];
        radius = (radius * 2) + 1;
        radiusSq = (radius * radius) / 4;
        for(y = 0; y < _penSize; y++)
        {
            yDiff = y - centerY;
            threshold = radiusSq - (yDiff * yDiff);
            for(x = 0; x < _penSize; x++)
            {
                d = x - centerX;
                _colorsTemp[y,x] = ((d * d) > threshold) ? Color.clear : _renderer.material.color;
            }
        }

        int i, j, k;
        k = 0;
        for (i = 0; i < _penSize; i++) {
            for (j = 0; j < _penSize; j++) {
                _colorTemp[k++] = _colorsTemp[i, j];
            }
        }

        return _colorTemp;
    }
}
