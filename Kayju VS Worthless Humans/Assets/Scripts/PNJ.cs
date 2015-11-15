using UnityEngine;
using System.Collections;

public class PNJ : Human {
    protected override void Init() { }
    protected override void SetVelocity() {
        if (master != null) {
            float deltaX = master.transform.position.x - this.transform.position.x;
            float deltaY = master.transform.position.y - this.transform.position.y;

            Vector2 pos = new Vector2(deltaX, deltaY) * speed;
            angle = Vector2.Angle(Vector2.up, pos);
            if (Mathf.Abs(deltaX) < 0.5 && Mathf.Abs(deltaY) < 0.5) {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            }
            else { GetComponent<Rigidbody2D>().velocity = pos; }
        }
    }
}
