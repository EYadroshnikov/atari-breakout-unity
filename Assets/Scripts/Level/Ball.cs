using Unity.VisualScripting;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public new Rigidbody2D rigidbody { get; private set; }
    public float speed  { get; set; } = 500f;

    private void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        ResetPosition();
        // Invoke(nameof(SetRandomTrajectory), 1f);
    }

    public void ResetPosition()
    {
        this.transform.position = Vector2.zero;
        this.rigidbody.velocity = Vector2.zero;

        Invoke(nameof(SetRandomTrajectory), 1f); 
    }

    private void SetRandomTrajectory()
    {
        Vector2 force = Vector2.zero;
        force.x = Random.Range(-1f, 1f);
        force.y = -1f;

        this.rigidbody.AddForce(force.normalized * this.speed);
        Debug.Log(force);
        // Debug.Log(this.rigidbody.totalForce);
    }

    //TODO: fix it
    // public void Update()
    // {
    //     this.speed = 500;
    // }
}
