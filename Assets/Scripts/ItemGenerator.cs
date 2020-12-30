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

        //一定の距離ごとにアイテムを生成
        //ｚ方向に15Mずつ間隔をあける
        for (int i = startPos; i < goalPos; i += 15)
        {
            //どのアイテムを出すかランダムに設定
            int num = Random.Range(1, 11);

            //アイテムを置くZ座標のオフセットをランダムに設定
            

            if (num <= 2)
            {
                int offsetZ = Random.Range(-5, 6);
                //wallを生成
                GameObject wall = Instantiate(wallPrefab);
                wall.transform.position = new Vector3(0, wall.transform.position.y, i + offsetZ);
            }
            else
            {
                //レーンごとにアイテムを生成
                for (int j = -1; j <= 1; j++)
                {
                    //アイテムの種類を決める
                    int item = Random.Range(1, 11);

                    int offsetZ = Random.Range(-5, 6);

                    //60%コイン配置：30％車配置：10％何もなし
                    if (1 <= item && item <= 6)
                    {
                        //wallを生成
                        GameObject wall = Instantiate(wallPrefab);
                        wall.transform.position = new Vector3(0, wall.transform.position.y, i + offsetZ);
                    }
                    else if (7 <= item && item <= 9)
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
