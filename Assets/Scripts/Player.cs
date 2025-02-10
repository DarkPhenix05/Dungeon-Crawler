using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{
    public Rigidbody rb;
    public Camera _mainCamera;
    public Vector3 _cameraFoward;
    public Vector3 _cameraRight;

    public float _speed;

    [Header("HP")]
    public bool isDead = false;
    public int hp;
    public int maxHp = 10;
    public HealthBar healthBar;

    private float attackDuration = .75f;
    public Collider attackCollider;

    [SerializeField] private Animator _anim;

    void Start()
    {
        while (!rb)
        {
            rb = this.gameObject.GetComponent<Rigidbody>();
        }

        while (!_anim)
        {
            _anim = this.gameObject.GetComponent<Animator>();
        }

        hp = maxHp;
        healthBar.SetMaxHealth(maxHp);
    }

    void Update()
    {
        Movement();
        Attack();
    }

    void Movement()
    {
        if (_mainCamera == null)
        {
            return;
        }

        float horInput = Input.GetAxisRaw("Horizontal");
        float verInput = Input.GetAxisRaw("Vertical");

        
        _cameraFoward = _mainCamera.transform.forward;
        _cameraRight = _mainCamera.transform.right;

        _cameraFoward.y = 0;
        _cameraRight.y = 0;

        Vector3 rightRelative = horInput * _cameraRight;
        Vector3 forwardRelative = verInput * _cameraFoward;

        Vector3 moveDir = (forwardRelative + rightRelative).normalized * _speed;

        rb.velocity = new Vector3(moveDir.x, rb.velocity.y , moveDir.z);

        _anim.SetFloat("Ver", verInput);
        _anim.SetFloat("Hor", horInput);
    }

    private void Attack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(TurnAttackCollider(attackDuration));
        }
    }

    private IEnumerator TurnAttackCollider(float _attackDuration)
    {
        attackCollider.gameObject.SetActive(true);
        yield return new WaitForSeconds(_attackDuration);
        attackCollider.gameObject.SetActive(false);
    }

    public void TakeDamage(int _dmg)
    {
        hp -= _dmg;
        healthBar.SetHealth(hp);
    }
}
