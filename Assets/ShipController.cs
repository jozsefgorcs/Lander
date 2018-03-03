//using UnityEditor;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    public GameObject LeftThrottle, RightThrottle, MainThrottle;
    public GameObject LeftEngine, RightEngine;
    public GameObject Explosion;
    private EllipsoidParticleEmitter _leftParticle, _rightParticle;
    private Vector3 _initialPosition;
    private Quaternion _initialRotation;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _leftParticle = LeftEngine.GetComponent<EllipsoidParticleEmitter>();
        _rightParticle = RightEngine.GetComponent<EllipsoidParticleEmitter>();
    }

    private void Update()
    {
    }

    void Start()
    {
        _initialPosition = gameObject.transform.position;
        _initialRotation = gameObject.transform.rotation;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis ("Horizontal");

        float moveVertical = Input.GetAxis ("Vertical")<0?0:Input.GetAxis ("Vertical");
       
        HandleParticles(moveHorizontal, moveVertical);
        HandleBoost(moveVertical, moveHorizontal);
    }

    private void HandleBoost(float moveVertical, float moveHorizontal)
    {
        var relativeForce = Vector2.up * moveVertical * 30f;
        _rigidbody2D.AddRelativeForce(relativeForce);
        _rigidbody2D.AddTorque(moveHorizontal * 1f);
    }

    private void HandleParticles(float moveHorizontal, float moveVertical)
    {
        if (moveHorizontal < 0)
        {
            _leftParticle.emit = true;
            _rightParticle.emit = false;
        }
        else if (moveHorizontal > 0)
        {
            _leftParticle.emit = false;
            _rightParticle.emit = true;
        }

        if (moveVertical > 0)
        {
            _leftParticle.emit = true;
            _rightParticle.emit = true;
        }
        if (moveHorizontal == 0 && moveVertical == 0)
        {
            _leftParticle.emit = false;
            _rightParticle.emit = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.relativeVelocity.magnitude > 5 )
        {
            var explosion = Instantiate(Explosion);
            explosion.transform.position = gameObject.transform.position;
            gameObject.SetActive(false);
            Invoke("Reinit",2f);
            Destroy(explosion,2f);
        }

       
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            //other.relativeVelocity.magnitude
           /* if (
                EditorUtility.DisplayDialog("You won",
                    "Congratulations, you successfully finished the game", "Thanks", "Don't care"))
            {
               
            }*/
            Reinit();
        }
    }

    void Reinit()
    {
        gameObject.transform.position = _initialPosition;
        gameObject.transform.rotation = _initialRotation;
        gameObject.SetActive(true);
        
    }
}