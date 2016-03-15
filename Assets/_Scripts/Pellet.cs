using UnityEngine;
using System.Collections;

public class Pellet : MonoBehaviour {

    private float speed;

    void Start()
    {
        speed = 1;
        GetComponent<Rigidbody2D>().velocity = new Vector2(6, 0) * speed;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ennemi")
        {
            Destroy(col.gameObject);
            Destroy(gameObject);
        }
    }
}
