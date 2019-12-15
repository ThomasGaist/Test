using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{

    [SerializeField]
    protected int health;

    [SerializeField]
    private EdgeCollider2D SwordCollider;

    [SerializeField]
    private List<string> damageSources;

    public abstract bool IsDead { get; }

    public bool attack;

    public bool TakingDamage { get; set; }
    private protected bool facingRight;
    //protected float movementSpeed;
    //protected SpriteRenderer spriteRenderer;

    public Animator Animator { get; private set; }
    public Rigidbody2D Rigidbody2D { get; private set; }

    
    // Start is called before the first frame update
    public virtual void Start()
    {
		facingRight = true;
		//spriteRenderer = GetComponent<SpriteRenderer>();
		Animator = GetComponent<Animator>();

	}

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract IEnumerator TakeDamage();

    public abstract void Death();

    public void ChangeDirection()
	{
		facingRight = !facingRight;
		transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
	}

    public void MeleeAttack()
    {
        SwordCollider.enabled = !SwordCollider.enabled;
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if(damageSources.Contains(other.tag))
        {
            StartCoroutine(TakeDamage());
        }
    }

    
}
