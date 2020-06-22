using UnityEngine;

public class Movable : MonoBehaviour
{
    public bool beingPushed = false;
    protected SpriteRenderer sr;
    protected Rigidbody2D rb;
    protected BoxCollider2D col;
    protected SpriteMask msk;
    public enum Rotation { front, right, back, left }
    public Rotation rotation = Rotation.front;
    protected Rotation lastRotation;
}
