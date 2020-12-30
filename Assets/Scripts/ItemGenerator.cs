using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    //スクリプトでアイテムを生成する
    //何のインスタンスから生成し、どこに配置するか


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

        //一定の距離ごとに障害物発生タイミングの壁を生成
        //ｚ方向に15Mずつ間隔をあける
        for (int i = startPos; i < goalPos; i += 15)
        {
            //どのアイテムを出すかランダムに設定
            int num = Random.Range(1, 11);

            if (num <= 2)
            {
                //もともとコーン4つ生成する部分をwallを生成にしてます
                GameObject wall = Instantiate(wallPrefab);
                wall.transform.position = new Vector3(0, wall.transform.position.y, i);
            }
            else
            {
                //壁の数
                for (int j = -1; j <= 1; j++)
                {
                    //ランダム
                    int rndm    = Random.Range(1, 11);

                    //前後位置ランダム調整
                    int offsetZ = Random.Range(-5, 6);

                    //60%コイン配置：30％車配置：10％何もなし
                    if (1 <= rndm && rndm <= 9)
                    {
                        //wallを生成
                        GameObject wall = Instantiate(wallPrefab);
                        wall.transform.position = new Vector3(0, wall.transform.position.y, i + offsetZ);
                    }

                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

 
}
