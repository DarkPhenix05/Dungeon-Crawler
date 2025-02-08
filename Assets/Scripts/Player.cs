using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody rb;
    public Camera _mainCamera;
    public Vector3 _cameraFoward;
    public Vector3 _cameraRight;

    public float _speed;

    public float hp = 10f;

    // Start is called before the first frame update
    void Start()
    {
        while (!rb)
        {
            rb = this.gameObject.GetComponent<Rigidbody>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (_mainCamera == null)
        {
            return;
        }

        float horInput = Input.GetAxisRaw("Horizontal") * _speed;
        float verInput = Input.GetAxisRaw("Vertical") * _speed;

        
        _cameraFoward = _mainCamera.transform.forward;
        _cameraRight = _mainCamera.transform.right;

        _cameraFoward.y = 0;
        _cameraRight.y = 0;

        Vector3 rightRelative = horInput * _cameraRight;
        Vector3 forwardRelative = verInput * _cameraFoward;

        Vector3 moveDir = forwardRelative + rightRelative;

        rb.velocity = new Vector3(moveDir.x, rb.velocity.y , moveDir.z);

        //this.transform.position += new Vector3(moveDir.x,0,moveDir.z); POS METH
    }

    public void TakeDamage(float _dmg)
    {
        hp -= _dmg;
    }
}
