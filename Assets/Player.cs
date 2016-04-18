using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class Player : MonoBehaviour
{
    public float MaxSpeed;
    public Sprite man;
    public Sprite bear;

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

    private bool shouldBeABear = false;

    Vector2 currentTarget;

    bool isTransforming;

    private bool IsFacingRight = true;
    private Animator anim;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();

        StartCoroutine(Transformer());
    }

    void FixedUpdate()
    {
        if (Application.isEditor &&
            Input.GetKeyDown(KeyCode.X))
        {
            shouldBeABear = !shouldBeABear;
        }

        float moveX = 0;
        float moveY = 0;

        if (isTransforming)
        {
        }
        else if (IsMan)
        {
            moveX = Input.GetAxis("Horizontal");
            moveY = Input.GetAxis("Vertical");
        }
        else
        {
            moveX = Random.Range(-MaxSpeed, MaxSpeed);
            moveY = Random.Range(-MaxSpeed, MaxSpeed);
        }

        GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * MaxSpeed, moveY * MaxSpeed);

        if (transform.position.x > 1.7)
        {
            moveX = 0;
            transform.position = new Vector3(1.7f, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -1.7)
        {
            moveX = 0;
            transform.position = new Vector3(-1.7f, transform.position.y, transform.position.z);
        }

        if (transform.position.y > 2.88)
        {
            moveY = 0;
            transform.position = new Vector3(transform.position.x, 2.88f, transform.position.z);
        }
        else if (transform.position.y < -1.9)
        {
            moveY = 0;
            transform.position = new Vector3(transform.position.x, -1.9f, transform.position.z);
        }

        anim.SetFloat("Speed", Mathf.Abs(moveX));

        if ((moveX > 0 && !IsFacingRight) ||
            (moveX < 0 && IsFacingRight))
        {
            Flip();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter2D");

        if (other.tag == "Light")
        {
            shouldBeABear = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("OnTriggerExit");

        if (other.tag == "Light")
        {
            shouldBeABear = false;
        }
    }

    private void Flip()
    {
        IsFacingRight = !IsFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private IEnumerator Transformer()
    {
        while (Global.Instance.IsPlaying)
        {
            yield return null;

            if (shouldBeABear)
            {
                yield return TransformToBear();
            }
            else
            {
                yield return TransformToMan();
            }
        }
    }

    private IEnumerator TransformToBear()
    {
        while (isTransforming)
        {
            yield return null;
        }

        if (IsMan)
        {
            isTransforming = true;

            anim.SetTrigger("TransformToBear");

            yield return new WaitForSeconds(1.0f);

            isTransforming = false;
        }
    }

    private IEnumerator TransformToMan()
    {
        while (isTransforming)
        {
            yield return null;
        }

        if (!IsMan)
        {
            isTransforming = true;

            anim.SetTrigger("TransformToMan");

            yield return new WaitForSeconds(1.0f);

            isTransforming = false;
        }
    }
}
