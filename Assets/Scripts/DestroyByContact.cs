using UnityEngine;
using System.Collections;

/**
 * 行星碰撞类
 * 
 * 当碰撞体进入触发器触发（销毁物体和自生）
 * */
public class DestroyByContact : MonoBehaviour {

	public GameObject explosion;//射击爆炸效果（Inspector界面拖入explosion_asteroid,如需要设置爆炸音效，将音效素材拖入到explosion_asteroid素材 的 Inspector栏并勾选 Play On Awake(自动播放)）
	public GameObject playerExplosion;//飞船爆炸效果

	public int scoreValue; //单个行星分数
	private GameController gameController;//GameController类的实例变量，因为需要调用到此类 次对象在本类使用，可设置为 private

	// Use this for initialization
	void Start () {

		/**
		 * 引入积分函数所在类 GameController.cs 
		 * 根据他的场景控件引入
		 * */
		// 引入Tag=GameController 的Game Controller 
		GameObject gameControllerObject = GameObject.FindWithTag("GameController");
		if(gameControllerObject != null){
			//引入 Game Controller 的脚本 GameController.cs 
			gameController = gameControllerObject.GetComponent<GameController>();
		}
		if(gameController == null){
			//Debug.Log("没有找到GameController.cs");
		}
	}

	//trigger
	/**
	 * 当碰撞体进入触发器触发（销毁所有物体）
	 **/
	void OnTriggerEnter (Collider other) {

		//但之前设置的Boundary(场景容器)也是触发器，场景里的物体肯定算是容器内，所以也会销毁 所以需要排除他, 给 Boundary Add 一个Tag（命名） 命名为 Boundary
		if(other.tag=="Boundary"){
			return;
		}

		//Debug.Log (other.name);//打印碰撞过来物体name

		//实例化创建对象(子弹打行星效果) （爆炸效果素材，行星坐标，行星角度）  transform 为当前对象(行星)
		Instantiate (explosion, transform.position, transform.rotation);

		//飞船撞击行星效果
		if (other.tag == "Player") {//当碰撞过来物体为飞船 （设置飞船的Tag为Player）
			Instantiate (playerExplosion, other.transform.position, other.transform.rotation);

			other.gameObject.SetActive(false);//隐藏物体，不销毁 （避免EasyTouch找不到对象） SetActive(bool) 

			//结束游戏
			gameController.GemaOver ();
		} else {//不销毁飞船
			Destroy(other.gameObject);//销毁过来的碰撞体（子弹等物体）	
		}

		//Destroy 不会立即销毁物体，而是先标记，然后在每帧结束时销毁

		Destroy(gameObject);//销毁本物体

		//计算。显示分数
		gameController.AddScore (scoreValue);//调用 GameController.cs 的公共方法更新分数
	}

	/**

	
	// Update is called once per frame
	void Update () {
	
	}
	**/

}
