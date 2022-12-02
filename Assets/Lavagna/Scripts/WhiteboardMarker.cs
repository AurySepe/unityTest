using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using Oculus.Interaction.Input;
using Unity.VisualScripting;
using UnityEngine;

public class WhiteboardMarker : MonoBehaviour
{
    [SerializeField] private Transform _tip;
    [SerializeField] private int _penSize = 5;

    private Renderer _renderer;

    private Grabbable _ovrGrabbale;

    private Color[] _colors;
    private float _tipHeight;

    private RaycastHit _touch;

    private Whiteboard _whiteboard;

    private Vector2 _touchPos;
    private Vector2 _lastTouchPos;

    private Quaternion _lastTouchRot;

    private Color _whiteboardColor;

    private bool _touchedLastFrame;

    public bool isEreaser = false;

    public AudioClip writingSound;
    
    public AudioSource _AudioSource;
    
    [SerializeField]
    HandGrabInteractor grabInteractorRight;
    [SerializeField]
    HandGrabInteractor grabInteractorLeft;
    
    void Start()
    {
        _renderer = _tip.GetComponent<Renderer>();
        
        _AudioSource = GetComponent<AudioSource>();
        _AudioSource.loop = true;

        _ovrGrabbale = transform.GetComponent<Grabbable>();

        _colors =Enumerable.Repeat(_renderer.material.color, _penSize*_penSize).ToArray();
        _tipHeight = _tip.localScale.y;
    }

    OVRInput.Controller ControllerThatIsGrabbing()
    {
        if (grabInteractorLeft.IsGrabbing && grabInteractorLeft.Interactable.transform.parent.Equals(transform))
        {
            return OVRInput.Controller.LTouch;
        }
        else if (grabInteractorRight.IsGrabbing && grabInteractorRight.Interactable.transform.parent.Equals(transform))
        {
            return OVRInput.Controller.RTouch;
        }

        return OVRInput.Controller.None;
    }

    void FixedUpdate()
    {
        //se stiamo toccando la lavagno cambiamo il render della lavagna
        Draw();
    }

    // ReSharper disable Unity.PerformanceAnalysis
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

                    if (isEreaser)
                    {
                        _whiteboardColor = _touch.transform.GetComponent<Renderer>().material.color;

                        if (_whiteboardColor == null)
                        {
                            _whiteboardColor = Color.white;
                        }
                        
                        _colors =Enumerable.Repeat(_whiteboardColor, _penSize*_penSize).ToArray();
                    }
                    
                }
                

                _touchPos = new Vector2(_touch.textureCoord.x, _touch.textureCoord.y);

                var x = (int)(_touchPos.x * _whiteboard.textureSize.x - (_penSize / 2));
                var y = (int)(_touchPos.y * _whiteboard.textureSize.y - (_penSize / 2));

                //se siamo fuori dalla lavagna smettiamo di disegnare
                if (y < 0 || y > _whiteboard.textureSize.y-5 || x < 0 || x > _whiteboard.textureSize.x-5 )
                {
                    return;
                }

                if (_touchedLastFrame)
                {
                    var touchCord = new Vector2(x, y);

                    float distance = Vector2.Distance(_lastTouchPos,touchCord);

                    _whiteboard.texture.SetPixels(x, y, _penSize, _penSize,_colors);
                    VibrationManager.singleton.TriggerVibration(20,2,1,ControllerThatIsGrabbing());
                    _AudioSource.Play();

                    var interation = (_penSize / distance)/3;

                    var i = 0;
                    
                    for (float f = 0.01f; f < 1; f += interation)
                    {
                        var lerpX = (int)Mathf.Lerp(_lastTouchPos.x, x, f);
                        var lerpY = (int)Mathf.Lerp(_lastTouchPos.y, y, f);

                        i++;
                        
                        
                        _whiteboard.texture.SetPixels(lerpX, lerpY, _penSize, _penSize,_colors);
                    }
                    
                    print("ITERAZIONI FATTE "+i);

                    _whiteboard.texture.Apply();
                }

                _AudioSource.Stop();
                _lastTouchPos = new Vector2(x, y);
                _touchedLastFrame = true;
                return;
            }
        }

        _whiteboard = null;
        _touchedLastFrame = false;
    }
}

