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
            return GetComponent<SpriteRenderer>().sprite == man; ;
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

    private void StartPlayerTransformation()
    {
        isTransforming = true;

        if (IsMan)
        {
            GetComponent<SpriteRenderer>().sprite = bear;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = man;
        }

        StartCoroutine(BlockTranforming(1.0f));
    }

    private IEnumerator BlockTranforming(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        isTransforming = false;
    }
}
