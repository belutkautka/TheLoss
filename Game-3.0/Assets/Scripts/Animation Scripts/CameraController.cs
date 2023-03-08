using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float dumping = 1.5f;
    public float offsetX = 2f;
    public float offsetY = 1f;
    public Vector2 offset;
    public bool isLeft;
    public Transform activePlayer;
    public int lastX;

    [SerializeField] private float leftLimit;
    [SerializeField] private float rightLimit;
    [SerializeField] private float bottomLimit;
    [SerializeField] private float upperLimit;

    public void Start()
    {
        offset = new Vector2(offsetX, offsetY);
        offset = new Vector2(Mathf.Abs(offset.x), offset.y);
        activePlayer = null;
        FindPlayer(isLeft);
    }

    public void Update()
    {
        activePlayer = Control.TakeActivePlayer().transform;
        if (activePlayer != null && activePlayer)
        {
            var currentX = Mathf.RoundToInt(activePlayer.position.x);
            if (currentX > lastX) isLeft = false;
            else if (currentX < lastX) isLeft = true;
            lastX = Mathf.RoundToInt(activePlayer.position.x);

            Vector3 target;
            if (isLeft)
                target = new Vector3(activePlayer.position.x - offset.x, activePlayer.position.y + offset.y,
                    transform.position.z);
            else
                target = new Vector3(activePlayer.position.x + offset.x, activePlayer.position.y + offset.y,
                    transform.position.z);

            var currentPosition = Vector3.Lerp(transform.position, target, dumping * Time.deltaTime);
            transform.position = currentPosition;
        }

        var position = transform.position;
        position = new Vector3(Mathf.Clamp(position.x, leftLimit, rightLimit),
            Mathf.Clamp(position.y, bottomLimit, upperLimit), position.z
        );
        transform.position = position;
    }

    private void FindPlayer(bool playerIsLeft)
    {
        activePlayer = Control.TakeActivePlayer()?.transform;
        if (activePlayer == null) return;
        lastX = Mathf.RoundToInt(activePlayer.position.x);
        if (playerIsLeft)
            transform.position = new Vector3(activePlayer.position.x - offset.x, activePlayer.position.y - offset.y,
                transform.position.z);
        else
            transform.position = new Vector3(activePlayer.position.x + offset.x, activePlayer.position.y + offset.y,
                transform.position.z);
    }
}