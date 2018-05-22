using UnityEngine;
using UnityEngine.EventSystems;

public class CampaignMover : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private float speed;
    [Space]
    [SerializeField]
    private float leftX;
    [SerializeField]
    private float rightX;

    private Vector2 prevMousePos;
    private bool isMoving;

    void Update()
    {
        if (isMoving)
        {
            Move();
        }
    }

    private void Move()
    {
        transform.position += new Vector3((Input.mousePosition.x - prevMousePos.x) * Time.deltaTime * speed, 0);
        float posX = transform.localPosition.x;
        if (posX < leftX) posX = leftX;
        else if (posX > rightX) posX = rightX;
        transform.localPosition = new Vector3(posX, transform.position.y, transform.position.z);
        prevMousePos = Input.mousePosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isMoving = true;
        prevMousePos = Input.mousePosition;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isMoving = false;
    }
}
