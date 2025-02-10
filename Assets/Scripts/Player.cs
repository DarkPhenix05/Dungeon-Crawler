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

    public PlayerAnimator playerAnimator;

    public float _speed;

    [Header("HP")]
    public bool isDead = false;
    public int hp;
    public int maxHp = 10;
    public HealthBar healthBar;

    private float attackDuration = .75f;
    public Collider attackCollider;

    void Start()
    {
        while (!rb)
        {
            rb = this.gameObject.GetComponent<Rigidbody>();
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

        Vector3 InputVector = new Vector3(horInput, 0, verInput).normalized;

        _cameraFoward = _mainCamera.transform.forward;
        _cameraRight = _mainCamera.transform.right;

        _cameraFoward.y = 0;
        _cameraRight.y = 0;

        Vector3 rightRelative = InputVector.x * _cameraRight;
        Vector3 forwardRelative = InputVector.z * _cameraFoward;

        Vector3 moveDir = (forwardRelative + rightRelative) * _speed;

        rb.velocity = new Vector3(moveDir.x, rb.velocity.y , moveDir.z);

        //if(playeranimator != null)
        playerAnimator?.SetAnimMovement(rb.velocity.sqrMagnitude);

        //this.transform.position += new Vector3(moveDir.x,0,moveDir.z); POS METH
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
        playerAnimator?.SetAnimAttack();
        yield return new WaitForSeconds(_attackDuration);
        attackCollider.gameObject.SetActive(false);
    }

    public void TakeDamage(int _dmg)
    {
        hp -= _dmg;
        healthBar.SetHealth(hp);
        playerAnimator?.SetHurtAttack();
    }
}
