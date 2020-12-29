using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UnityChanController : MonoBehaviour
{
    //このスクリプトはUnityChanにアタッチするので、ここで
    //GetComponentとするとUnityChanに設定されたConponentが取得できる


    //アニメーションするためのコンポーネントを入れる
    private Animator myAnimator;

    //Unityちゃんを移動させるコンポーネントを入れる
    private Rigidbody myRigidbody;

    //前方向の速度(Rigidbodyクラスのvelocityに渡す:速度)
    private float velocityZ = 16f;

    //横方向の速度
    private float velocityX = 10f;

    //上方向の速度
    private float velocityY = 10f;

    //左右の移動できる範囲
    private float movableRange = 3.4f;

    //動きを減速させる係数
    private float coefficient = 0.99f;

    //ゲーム終了の判定
    private bool isEnd = false;

    //ゲーム終了時に表示するテキスト
    private GameObject stateText;

    //スコアを表示するテキスト
    private GameObject scoreText;

    //得点
    private int score = 0;

    //左ボタン押下の判定
    private bool isLButtonDown = false;

    //右ボタン押下の判定
    private bool isRButtonDown = false;

    //ジャンプボタン押下の判定
    private bool isJButtonDown = false;




    // Start is called before the first frame update
    void Start()
    {
        //Animatorコンポーネントを取得
        //スクリプトでアニメーションを操作するためObjectにアタッチしているAnimatorコンポーネントを所得する
        this.myAnimator = GetComponent<Animator>();

        //走るアニメーションを開始
        //SetFloat関数　第一引数：パラメータ「スピード」を指定　　第二：再生条件。スピードは０.８以上を指定
        this.myAnimator.SetFloat("Speed", 1);

        //Rigidbodyコンポーネントを取得
        this.myRigidbody = GetComponent<Rigidbody>();

        //シーン中のstateTextオブジェクトを取得
        this.stateText = GameObject.Find("GameResultText");

        //シーンの中のscoreTextオブジェクトを取得
        this.scoreText = GameObject.Find("ScoreText");


    }

    // Update is called once per frame
    void Update()
    {

        //ゲーム終了ならUnityChanの動きを減衰する
        if (this.isEnd)
        {
            this.velocityZ *= this.coefficient;
            this.velocityX *= this.coefficient;
            this.velocityY *= this.coefficient;
            this.myAnimator.speed *= this.coefficient;
        }

        //横方向の入力による速度
        float inputVelocityX = 0;

        //上方向の入力による速度
        float inputVelocityY = 0;


        //矢印キーまたはボタンに応じて左右に移動
        if((Input.GetKey(KeyCode.LeftArrow)|| this.isLButtonDown) && -this.movableRange < this.transform.position.x){
            //左方向へ速度を代入
            inputVelocityX = -this.velocityX;
        }
        else if((Input.GetKey (KeyCode.RightArrow) || this.isRButtonDown) && this.transform.position.x < this.movableRange)
        {
            //右方向への速度を代入
            inputVelocityX = this.velocityX;
        }

        //ジャンプしていない時(transform.y が0.5以下)にスペースが押されてたらジャンプする
        if( (Input.GetKeyDown(KeyCode.Space) || this.isJButtonDown) && this.transform.position.y < 0.5f)
        {
            //ジャンプアニメを再生
            //SetBool関数（第一引数のAnimatorのパラメータに第二引数を代入する）
            this.myAnimator.SetBool("Jump", true);

            //上方向への速度を代入
            inputVelocityY = this.velocityY;
        }
        else
        {
            //現在のY軸の速度を代入
            //ここで現在のYを代入しないと　あまり高く飛ばない。
            //ジャンプ入力がない場合は　速度を維持する　設定がここ
            inputVelocityY = this.myRigidbody.velocity.y;

        }

        //Jumpステートの場合はJumpにfalseをセットする
        //Animatorクラスの「GetCurrentAnimatorStateInfo(0)」で現在のアニメーションの状態を取得
        if (this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            this.myAnimator.SetBool("Jump", false);
        }



        //UnityChanを矢印キーまたはボタンに応じて左右に移動
        if (Input.GetKey(KeyCode.LeftArrow) && -this.movableRange < this.transform.position.x)
        {
            //左方向への速度を代入
            inputVelocityX = -this.velocityX;

        }
        else if (Input.GetKey(KeyCode.RightArrow) && this.transform.position.x < this.movableRange)
        {
            //右方向への速度を代入
            inputVelocityX = this.velocityX;

        }

        //Unityちゃんに速度を与える(Vector3: X, Y, Z)
        //velocity（速度値変更での移動は非推奨らしい。Rigidbody.AddForceが今後のスタンダード？）
        this.myRigidbody.velocity = new Vector3(inputVelocityX, inputVelocityY, this.velocityZ);

    }

    //トリガーモードで他のオブジェクトと接触した場合の処理
    //colisionではなくトリガーとの衝突　OnTriggerEnter
    private void OnTriggerEnter(Collider other)
    {
        //障害物に衝突した場合
        if(other.gameObject.tag == "CarTag" || other.gameObject.tag == "TrafficConeTag")
        {
            this.isEnd = true;

            //stateTextにGAME OVERを表示
            this.stateText.GetComponent<Text>().text = "GAME OVWR";
        }

        //ゴール地点に到達した場合
        if(other.gameObject.tag == "GoalTag")
        {
            this.isEnd = true;

            //stateTextにGAME CLEARを表示
            this.stateText.GetComponent<Text>().text = "GAME CLEAR";

        }

        //コインに衝突した場合
        if(other.gameObject.tag == "CoinTag")
        {
            //スコアを加算
            this.score += 10;

            //ScoreTextに獲得した点数を表示
            this.scoreText.GetComponent<Text>().text = "Score " + this.score + "pt";

            //パーティクルの再生
            GetComponent<ParticleSystem>().Play();

            //接触したコインのオブジェクトを破棄
            Destroy(other.gameObject);
        }
    }

    //ジャンプボタンを押した場合の処理
    public void GetMyJumpButtonDown()
    {
        this.isJButtonDown = true;
    }

    //ジャンプボタンを離した場合の処理
    public void GetMyJumpButtonUp()
    {
        this.isJButtonDown = false;
    }

    //左ボタンをおし続けた場合の処理
    public void GetMyLeftButtonDown()
    {
        this.isLButtonDown = true;
    }

    //左ボタンを離した場合の処理
    public void GetMyLeftButtonUp()
    {
        this.isLButtonDown = false;
    }

    //右ボタンを押し続けた場合の処理
    public void GetMyRightButtonDown()
    {
        this.isRButtonDown = true;
    }

    //右ボタンを離した場合
    public void GetMyRightButtonUp()
    {
        this.isRButtonDown = false;
    }

}
