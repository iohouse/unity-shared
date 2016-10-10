using UnityEngine;

public class MoveMe : MonoBehaviour {

    public enum Move { Horizontal, Vertical }
    public Move Direction = Move.Horizontal;

    public Vector2 Speed;
    public float Amplitude;

    private Vector3 _tempPosition;


    void Start () {

        _tempPosition = this.transform.position;
    }

    void FixedUpdate () {

         switch (Direction)
         {
            case Move.Horizontal:
                this.transform.position = new Vector3(
                    _tempPosition.x + Mathf.Sin(Time.realtimeSinceStartup * Speed.x) * Amplitude, 
                    _tempPosition.y, 
                    _tempPosition.z);
                break;

             case Move.Vertical:
                this.transform.position = new Vector3(
                    _tempPosition.x, 
                    _tempPosition.y + Mathf.Sin(Time.realtimeSinceStartup * Speed.y) * Amplitude, 
                    _tempPosition.z);
                break;
         }
    }
}