using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerScript : MonoBehaviour
{
    //carPrebを入れる
    public GameObject carPrefab;

    //coinPrefabを入れる
    public GameObject coinPrefab;

    //cornPrefabを入れる
    public GameObject conePrefab;

    //CreateWallPrefabを入れる
    public GameObject wallPrefab;

    //スタート地点
    private int startPos = 40;

    //ゴール地点
    private int goalPos = 360;

    //アイテムを出すx方向の範囲
    private float posRange = 3.4f;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateObjects(GameObject other)
    {
        //Debug.Log(other.gameObject.tag);
        //障害物に衝突した場合
        if (other.gameObject.tag == "CreateWallTag")
        {

            if (other.transform.position.z < (goalPos - 50))
            {

                //アイテムの種類を決める
                int item = Random.Range(1, 11);

                Debug.Log(item);

                //アイテムを置くZ座標のオフセットをランダムに設定
                int offsetZ = Random.Range(-3, 5);

                //60%コイン配置：30％車配置：10％何もなし
                if (item == 2)
                {
                    //コーンをx軸方向に一直線に生成
                    for (float j = -1; j <= 1; j += 0.4f)
                    {
                        GameObject cone = Instantiate(conePrefab);
                        cone.transform.position = new Vector3(4 * j, cone.transform.position.y, other.transform.position.z + 40);
                    }
                }
                else if (3 <= item && item <= 6)
                {
                        //コインを生成
                        GameObject coin = Instantiate(coinPrefab);
                        coin.transform.position = new Vector3(posRange * Random.Range(-1, 1), coin.transform.position.y, other.transform.position.z + 40 + offsetZ);
                }
                else if (7 <= item && item <= 9)
                {
                        //車を生成
                        GameObject car = Instantiate(carPrefab);
                        car.transform.position = new Vector3(posRange * Random.Range(-1, 1), car.transform.position.y, other.transform.position.z + 40 + offsetZ);
                }
            }
        }

    }


}
