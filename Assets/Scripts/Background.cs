using UnityEngine;

/*
public class Background : MonoBehaviour
{
    // スクロールするスピード
    public float speed = 0.1f;

    void Update ()
    {
        // 時間によってYの値が0から1に変化していく。1になったら0に戻り、繰り返す。
        float y = Mathf.Repeat (Time.time * speed, 1);

        // Yの値がずれていくオフセットを作成
        Vector2 offset = new Vector2 (0, y);

        // マテリアルにオフセットを設定する
        GetComponent<Renderer>().sharedMaterial.SetTextureOffset ("_MainTex", offset);
    }
}
*/

public class Background : MonoBehaviour
{
    private Vector3 beforePosition;
    private Vector2 beforeOffset;
    // スクロールするスピード
    public float speed = 0.1f;

    public GameObject TrackObject;

    void Update ()
    {
        if (TrackObject == null)
            return;

        var pos = TrackObject.transform.position - beforePosition;
        var pos2 = new Vector2(pos.x, pos.y);
        Vector2 offset = beforeOffset + pos2 * speed;

        Debug.Log(offset);

        // マテリアルにオフセットを設定する
        GetComponent<Renderer>().sharedMaterial.SetTextureOffset ("_MainTex", offset);

        beforePosition = TrackObject.transform.position;
        beforeOffset = offset;

        this.transform.position = new Vector3 (Camera.main.transform.position.x, Camera.main.transform.position.y, this.transform.position.z) ;
    }
}