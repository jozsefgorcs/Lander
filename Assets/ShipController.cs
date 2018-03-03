using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    public GameObject LeftThrottle, RightThrottle, MainThrottle;
    public GameObject LeftEngine, RightEngine;
    private EllipsoidParticleEmitter _leftParticle, _rightParticle; 
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _leftParticle = LeftEngine.GetComponent<EllipsoidParticleEmitter>();
        _rightParticle = RightEngine.GetComponent<EllipsoidParticleEmitter>();
    }

    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //
        float moveHorizontal = Input.GetAxis ("Horizontal");

        //Store the current vertical input in the float moveVertical.
        float moveVertical = Input.GetAxis ("Vertical")<0?0:Input.GetAxis ("Vertical");
        Debug.Log(string.Format("Horizontal:{0}, vertical: {1}", moveHorizontal, moveVertical));
        //Use the two store floats to create a new Vector2 variable movement.
       
        if (moveHorizontal < 0)
        {
            _leftParticle.emit = true;
            _rightParticle.emit = false;
        }else if (moveHorizontal > 0)
        {
            _leftParticle.emit = false;
            _rightParticle.emit = true;
        }
        if(moveVertical>0)
        {
            _leftParticle.emit = true;
            _rightParticle.emit = true;
        }

        var relativeForce = Vector2.up * moveVertical*60f;
        //var sideForce = Vector2.left * moveHorizontal * 30f;
        Debug.Log("Relative force: "+relativeForce);
        _rigidbody2D.AddRelativeForce(relativeForce);
        _rigidbody2D.AddTorque(moveHorizontal * 3f);
       // _rigidbody2D.AddForceAtPosition(new Vector2(20,moveVertical*30f),throttlePosition );        
    }
}