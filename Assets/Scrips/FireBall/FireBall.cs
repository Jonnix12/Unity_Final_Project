using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{

    PlayerCombet _combet;
    bool _toShot = false;
    Animator _animator;
    readonly float speed = 20;
    bool _delay = false;
    float _waitTime = 0.2f;
    static public float Damage = 3;
    LayerMask _enemyLayers;
    void Start()
    {
        _combet = FindObjectOfType<PlayerCombet>();
        _animator = GetComponentInChildren<Animator>();
        _enemyLayers = LayerMask.GetMask("Enemy");
    }

    private void OnDisable()
    {
        _waitTime = 0.2f;
        _delay = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_toShot)
        {
            transform.Translate(Vector2.right * Time.deltaTime * speed);
        }

        if (_delay)
        {
            if (_waitTime >= 0)
            {
                _waitTime -= Time.deltaTime;
            }
            else
            {
                _combet.RenterFireBall(this);
            }
        }
    }

    public void Shot()
    {
        _toShot = true;
    }

    void PlayBoomAnim()
    {
        _animator.SetTrigger("Boom");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (!collision.gameObject.CompareTag("Player"))
        {
            _toShot = false;
            PlayBoomAnim();
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, 1.5f, _enemyLayers);

            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<AiCombat>().Hurt(Damage);
            }

            _delay = true;
        }
    }
}
