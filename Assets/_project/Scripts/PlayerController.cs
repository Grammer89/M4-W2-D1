using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _speed;
    Rigidbody _rb;
    float _v;
    float _h;
    Vector3 _lastDirection;
    GroundCheck _checkGround;
    bool _okDoubleJump;
    float _angularVelocity = 120f;
    // Start is called before the first frame update
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _checkGround = GetComponentInChildren<GroundCheck>();
        //angularVelocity = new Vector3(0, 0.0001f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        _h = Input.GetAxis("Horizontal");
        _v = Input.GetAxis("Vertical");
        if (_h == 0 && _v == 0)
        {
            _h = _lastDirection.x;
            _v = _lastDirection.z;
        }
        else
        {
            _lastDirection.x = _h;
            _lastDirection.z = _v;
        }

        if ((Input.GetButtonDown("Jump") && _checkGround.IsGround()) ||
           (Input.GetButtonDown("Jump") && _okDoubleJump))
        {
            Debug.Log("SALTAAAA");
            _rb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
            if (_okDoubleJump)
            {
                _okDoubleJump = false;
            }
            else
            {
                _okDoubleJump = true;
            }
        }

        if (Input.GetButtonDown("Fire3"))
        {
            _speed *= 2;
        }

        if (Input.GetButtonUp("Fire3"))
        {
            _speed *= 0.5f;
        }

    }

    void FixedUpdate()
    {
        Vector3 position = new Vector3(_h, 0, _v);
        if (position.magnitude > 1)
        {
            position = position.normalized;
        }
        _rb.MovePosition(_rb.position + position * _speed * Time.deltaTime); //movimento del personaggio

        gameObject.transform.forward = position; //Indico la direzione in cui sta vedendo il personaggio

        Vector3 rotation = Vector3.up * (_h * Time.deltaTime * _angularVelocity);
        //// Calculate the rotation for this frame
        Quaternion deltaRotation = Quaternion.Euler(rotation);

        //// Apply the rotation to the Rigidbody
        _rb.MoveRotation(_rb.rotation * deltaRotation);
        //_rb.MoveRotation(Quaternion.LookRotation(_rb.velocity));

    }
    void OnDrawGizmos()
    {
        //Indico la direzione del personaggio dove si sta direzionando nella scena
        Gizmos.color = Color.red;
        Gizmos.DrawLine(gameObject.transform.position, gameObject.transform.position + gameObject.transform.forward * 10f);
    }
}
