using UnityEngine;

public class MonsterBulletMovement : MonoBehaviour
{

    private Vector3 m_TargetDirection;
    private bool m_Moving;



    private Animator m_Animator;


    [SerializeField] private LayerMask m_InteractiveLayerMask;
    [SerializeField] private float m_BulletSpeed = 5f;
    [SerializeField] private float m_BulletExplodeRange = 1f;
    [SerializeField] private LayerMask m_DealDamageLayerMask;
    [SerializeField] private float m_Damage = 1f;


    private void Awake()
    {
        m_Moving = false;
        m_Animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(Utils.IsInLayerMask(collision.gameObject, m_InteractiveLayerMask))
        {
            Explode();
        }
    }

    private void FixedUpdate()
    {
        if(m_Moving)
        {
            Vector3 newPosition = transform.position;
            newPosition.x += m_TargetDirection.x * m_BulletSpeed * Time.deltaTime;
            newPosition.y += m_TargetDirection.y * m_BulletSpeed * Time.deltaTime;

            transform.position = newPosition;

        }

    }

    private void Explode()
    {
        OnExplosionUI();
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, m_BulletExplodeRange, m_DealDamageLayerMask);
        foreach (Collider2D hit in hitColliders)
        {
            // hit player
            PlayerHealth playerHealth = hit.GetComponent<PlayerHealth>();
            if (playerHealth)
            {
                Debug.Log("Player take damage: " + m_Damage);
                playerHealth.TakeDamage((int)m_Damage);
            }

            // hit props
            Prop prop = hit.GetComponent<Prop>();
            if (prop)
            {
                Debug.Log("Prop take damage: " + m_Damage);
                prop.TakeDamage((int)m_Damage);
            }
        }
        m_Moving = false;
    }


    private void OnExplosionUI()
    {
        m_Animator.SetBool("explode", true);
    }


    private void DestroyBullet()
    {
        gameObject.SetActive(false);
        m_Animator.SetBool("explode", false);

    }


    public void SetTargetDirection(Vector3 targetDirection)
    {
        if (!m_Moving)
        {
            m_TargetDirection = targetDirection;
            m_TargetDirection.Normalize();
            m_Moving = true;
        }
    }
}
