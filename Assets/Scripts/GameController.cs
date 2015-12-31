using UnityEngine;
using System.Collections;

/**
 * 游戏控制器类
 * 
 *  Instantiate:实例化克隆对象 (目标物体：gameObject,位置：Vector3类型, 旋转:Quaternion类型)
 * */
public class GameController : MonoBehaviour {

	public GameObject hazard;//行星对象 直接拖入
	public Vector3 spawnValues;//位置对象 行星生成位置的 X Y Z

	public int hazardCount;//生成的行星数量
	public float spawnWait;//生成行星间隔时间
	public float startWait;//开始等待时间

	public int score;//获得分数 	此对象在本类使用 可设置为private
	public int scoreNum;//击毁数量

	public GUIText scoreText;//分数显示控件 将 scoreText控件拖入
	public GUIText restartText;//游戏开始
	public GUIText gameOverText;//游戏结束

	private bool gameOver;	//是否游戏结束
	private bool restart;	//是否重新开始


	// Use this for initialization
	void Start () {

		//初始化 游戏

		gameOver = false;
		restart  = false;
		gameOverText.text = "Liukelin  Hollo World!";
		restartText.text = "";

		score = 0;
		scoreNum = 0;
		UpdateScore ();

		//生成行星 
		StartCoroutine( SpawnWaves ());//StartCoroutine 开始协同程序
	}

	void Update(){
		int ss = 0;
		if(restart){
			if (Input.GetKeyDown (KeyCode.R)) {
				Application.LoadLevel(Application.loadedLevel);//重新加载场景（Application.LoadLevel()加载场景，Application.loadedLevel当前场景编号）
			}

			//启用 
			//player.SetActive(true);
			//点击屏幕
			if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) {
				Application.LoadLevel(Application.loadedLevel);

				ss = 1;
			}
			//Debug.Log("ss:"+ss);
		}
	}
	

	//自定义函数 - 生成随机位置的行星对象 ,C#不支持直接程序等待，所以需要使用 IEnumerator 协同程序，用于使WaitForSeconds作用
	IEnumerator SpawnWaves(){

		yield return new WaitForSeconds(startWait);//游戏等待
		gameOverText.text = "";
		//
		while (true){//游戏死循环

			for(int i=0; i<hazardCount; i++){
				//生成行星 （位置：x：在场景左右边界范围随机Random.Range取范围随机数 , y：与飞船一个平面为0, Z：为场景顶部）
				Vector3 spawnPosition = new Vector3 (Random.Range(-spawnValues.x, spawnValues.x), 0, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;//不设置同一性旋转（只读）。该四元数，相当于"无旋转"：这个物体完全对齐于世界或父轴。
				Instantiate (hazard, spawnPosition, spawnRotation);//实例化障碍物
				
				yield return new WaitForSeconds(spawnWait);//程序等待秒数
			}

			if(gameOver){//当结束
				restart  = true;//重新开始
				restartText.text = "滑动屏幕重新开始！";
				break;
			}
		}

	}
	

	//显示分数分数
	void UpdateScore(){
		scoreText.text = "分数: " + score + "  数量： " + scoreNum;
	}

	//更新分数 公共函数
	public void AddScore(int newScoreValue){
		score += newScoreValue;
		scoreNum ++;
		UpdateScore ();
	}

	//公共函数
	public void GemaOver(){
		gameOver = true;
		gameOverText.text = "游戏结束！";
	}

}
