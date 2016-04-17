using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class Player : MonoBehaviour
{
    public float MaxSpeed;
    public Sprite man;
    public Sprite bear;

    Vector2 currentTarget;
    bool isAtTarget;

    bool isTransforming;
    float transformTimer;

    private bool IsFacingRight = true;
    private Animator anim;

    public bool IsMan
    {
        get
        {
            AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);

            if (state.IsName("ManIdle") || state.IsName("ManWalk"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (isTransforming == false &&
            Input.GetKeyDown(KeyCode.X))
        {
            StartPlayerTransformation();
        }

        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        anim.SetFloat("Speed", Mathf.Abs(moveX));

        GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * MaxSpeed, moveY * MaxSpeed);

        if ((moveX > 0 && !IsFacingRight) ||
            (moveX < 0 && IsFacingRight))
        {
            Flip();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter");

        if (!isTransforming && other.tag == "Light")
        {
            TransformToBear();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("OnTriggerExit");

        if (!isTransforming && other.tag == "Light")
        {
            TransformToMan();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        currentTarget = eventData.position;
        isAtTarget = false;
    }

    private void Flip()
    {
        IsFacingRight = !IsFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void TransformToBear()
    {
        if (IsMan)
        {
            StartPlayerTransformation();
        }
    }

    private void TransformToMan()
    {
        if (!IsMan)
        {
            StartPlayerTransformation();
        }
    }

    private void StartPlayerTransformation()
    {
        isTransforming = true;

        anim.SetTrigger("Transform");

        StartCoroutine(BlockTranforming(1.0f));
    }

    private IEnumerator BlockTranforming(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        isTransforming = false;
    }
}
