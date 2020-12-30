﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWall : MonoBehaviour
{

    //UnityChanのオブジェクト
    private GameObject unitychan;

    //UnityChanとカメラの距離
    private float difference;

    // Start is called before the first frame update
    void Start()
    {
        //UnityChanのオブジェクトを取得
        this.unitychan = GameObject.Find("unitychan");
        //UnityChanとカメラの位置（z座標）の差を求める
        this.difference = unitychan.transform.position.z - this.transform.position.z;

    }

    // Update is called once per frame
    void Update()
    {
        //UnityChanの位置に合わせてカメラを移動
        this.transform.position = new Vector3(0, this.transform.position.y, this.unitychan.transform.position.z - difference);
    }

    private void OnTriggerEnter(Collider other)
    {
        //障害物に衝突した場合
        if (other.gameObject.tag != "CreateWallTag")
        {
            //接触したオブジェクトを破棄
            Destroy(other.gameObject);
        }

    }
}
