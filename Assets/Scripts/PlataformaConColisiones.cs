    using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] private float timeComeBack = 1.5f;
    [SerializeField]private Vector3 direccionOfMovement;
    [SerializeField]private float speed = 2f;
    private Rigidbody rb;
    private float timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        direccionOfMovement = Vector3.back;
        rb.linearVelocity = direccionOfMovement * speed;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            timer = timeComeBack;
            rb.linearVelocity *= (-1);
        }
    }
}
