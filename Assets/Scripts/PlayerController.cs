using UnityEngine;
using System.Collections;

/**
 * 飞船控制类 (方向键控制 X Z)
 * 给飞船添加physics->Rigidbody组件（碰撞体） ，有这个组件才能使用Unity内置物理引擎
 * 类的公共属性Public 在 Inspector界面设置
 * X轴  飞船左右
 * z轴   前后
 * y轴  不需要变
 *  Instantiate:实例化克隆对象 (目标物体：gameObject,位置：Vector3类型, 旋转:Quaternion类型)
 * 搜索关键字文档 选中关键字 “Ctrl+'”
**/

//序列化(这样可以把 文件里面的其他类 显示在 Inspector界面：Boundary)
[System.Serializable]

//公共类 不继承任何类，可供其他类调用  public值也可在unity Inspector界面设置赋值
public class Boundary {
	//允许 物体 定义 x,z 轴的最小 最大值 数值在unity界面参照移动在取
	public float xMin;// = -6.0f;
	public float xMax;// = 6.0f;
	public float zMin;// = -4.0f;
	public float zMax;// = 8.0f;
}

/**
 * 控制类
 * gameObject :当前对象
 * transform :当前对象物体 等同gameObject。transform
 * 
 * */
public class PlayerController : MonoBehaviour {

	//飞船设置
	public float speed=10;//移动速度 倍数， public值也可在unity Inspector界面设置复制
	public Boundary boundary;//获取配置
	public float tilt; //飞船角度 倾斜 倍数

	//子弹设置
	public GameObject shot;		//目标物体(子弹1) 在unity Inspector界面 中把 Prefabs->的子弹元素 拖入到 shot栏
	public GameObject shot2;	//(子弹2)
	public GameObject shot3;	//(子弹3)
	public Transform shotSpawn; //飞船的子弹发射参照物（枪的位置） （Shot Spawn 在飞船下创建一个空的GameObject，然后Inspector界面拖入到shotSpawn）  位置和方向 如果是GameObject类型 设置位置:shotSpawn.transform.position,   shotSpawn.transform.rotation 角度也一样 。也可以为Transform 类型 shotSpawn。position
	
	public float fireRate = 0.25F;   //发射间隔s
	private float nextFire = 0.0F; //上一次发射时间

	//当加载新场景的时候，使游戏物体和它所有的transform子物体存活下来（重新启用其他地方执行销毁操作的动作物体，比如销毁飞船） 为了避免 EasyTouch 找不到已经销毁的物体而报错
	void Awake () {
		//transform.gameObject.SetActive (true);
		//把隐藏的物体删除


		//相当于新建了player
		DontDestroyOnLoad (transform.gameObject);
	}


	//发射子弹  每帧刷新前执行 Update is called once per frame
	void Update () {

		if (transform == null) {  //当飞船未被销毁
			return;
		}
		int ret = 0;//是否射中目标

		//GetButton 可一直按着, GetKeyDown 为按下,GetKeyDown 为按起,GetKey 按着 
		if (Input.GetKey (KeyCode.J) && Time.time > nextFire) {//Fire1 预设按钮（ctrl） ，Time.time 游戏运行时间
			nextFire = Time.time + fireRate;
			//GameObject clone = Instantiate(projectile, transform.position, transform.rotation) as GameObject;

			//实例化子弹类(创建对象)  Instantiate:实例化对象 (目标物体：original,位置：Position, 发射方向:rotation)
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
			ret = 1;
		}
		//z键 子弹   
		if (Input.GetKey (KeyCode.K) && Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			Instantiate (shot2, shotSpawn.position, shotSpawn.rotation);
			//Debug.Log("您按下了Z键");
			ret = 2;
		}
		//F键 子弹   
		if (Input.GetKey (KeyCode.L) && Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			Instantiate (shot3, shotSpawn.position, shotSpawn.rotation);
			ret = 3;
		}

		//播放发射音效（触发音效，音效文件已经拖入到Inspector,取消勾选Play On Awake(自动播放)）
		if(ret > 0){
			GetComponent<AudioSource>().Play();
		}
	}


	/**
	//物理移动控制  键盘控制  函数会在每个固定的物理步骤前自动调用
	void FixedUpdate(){
		//获取用户输入  控制默认为 1
		float moveHorizontal = Input.GetAxis ("Horizontal"); //输入管理器内置轴 X  左右
		float moveVertical = Input.GetAxis ("Vertical");	 //输入管理器内置轴 Z	 前后
		//Y移动0 ,


		//物理移动控制   调用Rigidbody组件  ,vector3(x,y,z) 控制移动 方向/速度（矢量/大小）
		Vector3 movement = new Vector3 (moveHorizontal,0.0f,moveVertical);//f 标识这个小数 是浮点型
		rigidbody.velocity = movement * speed;//用法 rigidbody.velocity = some Vector3 value;

		Debug.Log ("X:"+moveHorizontal+",Y:"+moveVertical);


		//限制移动范围 （为摄像机范围内）  Mathf 数值控制 ，  Clamp 最大值，最小值  position:位置
		rigidbody.position = new Vector3 
			(
				Mathf.Clamp(rigidbody.position.x, boundary.xMin, boundary.xMax), //x 
				0.0f,										   //y
				Mathf.Clamp(rigidbody.position.z, boundary.zMin, boundary.zMax)  //z
			);

		//飞船左右移动 时 倾斜(旋转)  rotation:角度 ,Quaternion 使用元素来设置旋转,.Euler 为.Rotation简化, ，z 此处根据左右移动 速度来 计算倾斜角度 rigidbody.velocity.x * tilt
		rigidbody.rotation = Quaternion.Euler (0.0f, 0.0f, rigidbody.velocity.x * -tilt);

	}
**/

	/**
	 * EasyTouch 控制飞机
	 * 
	 * */

	void OnEnable()  
	{  
		if (transform == null) {
			return;
		}
		EasyJoystick.On_JoystickMove += OnJoystickMove;  
		EasyJoystick.On_JoystickMoveEnd += OnJoystickMoveEnd;
	}  
	
	//Move_joystick 自定义 name
	//移动摇杆结束  
	void OnJoystickMoveEnd(MovingJoystick move)  
	{  
		if( transform == null){
			return;
		}

		//停止时，停止移动 
		if (move.joystickName == "Move_joystick")  
		{  
			Vector3 movement = new Vector3 (0.0f,0.0f,0.0f);
			GetComponent<Rigidbody>().velocity = movement;
			GetComponent<Rigidbody>().rotation = Quaternion.Euler (0.0f, 0.0f, 0.0f);
		}
	}  
	
	
	//移动摇杆中  
	void OnJoystickMove(MovingJoystick move)  
	{  
		if( transform == null){
			return;
		}

		if (move.joystickName != "Move_joystick" )
		{  
			return;  
		}
		
		//获取摇杆中心偏移的坐标  
		float joyPositionX = move.joystickAxis.x;  
		float joyPositionY = move.joystickAxis.y;  

		if (joyPositionY != 0 || joyPositionX != 0 )  //当飞船未被销毁
		{  

			/**
			//设置角色的朝向（朝向当前坐标+摇杆偏移量）  
			transform.LookAt(new Vector3(transform.position.x + joyPositionX, 0.0f, transform.position.z + joyPositionY));  
			//移动玩家的位置（按朝向位置移动）  
			transform.Translate(Vector3.forward * Time.deltaTime * 5);
			**/


			//物理移动控制   调用Rigidbody组件  ,vector3(x,y,z) 控制移动 方向/速度（矢量/大小）
			Vector3 movement = new Vector3 (joyPositionX,0.0f,joyPositionY);//f 标识这个小数 是浮点型
			GetComponent<Rigidbody>().velocity = movement * speed;//用法 rigidbody.velocity = some Vector3 value;

			//限制移动范围 （为摄像机范围内）  Mathf 数值控制 ，  Clamp 最大值，最小值  position:位置
			GetComponent<Rigidbody>().position = new Vector3 
				(
					Mathf.Clamp(GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax), //x 
					0.0f,										   //y
					Mathf.Clamp(GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)  //z
					);
			
			//飞船左右移动 时 倾斜(旋转)  rotation:角度 ,Quaternion 使用元素来设置旋转,.Euler 为.Rotation简化, ，z 此处根据左右移动 速度来 计算倾斜角度 rigidbody.velocity.x * tilt
			GetComponent<Rigidbody>().rotation = Quaternion.Euler (0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);

		}
	}

	//开火按钮  jump_button 为自定义 (将 player 拖入)
	void jump_button(){
		if( transform == null){
			return;
		}
		if (Time.time > nextFire) {//Fire1 预设按钮（ctrl） ，Time.time 游戏运行时间
			nextFire = Time.time + fireRate;
			//实例化子弹类(创建对象)  Instantiate:实例化对象 (目标物体：original,位置：Position, 发射方向:rotation)
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation);

			GetComponent<AudioSource>().Play();
		}

	}
	void jump_button1(){
		if( transform == null){
			return;
		}
		if (Time.time > nextFire) {//Fire1 预设按钮（ctrl） ，Time.time 游戏运行时间
			nextFire = Time.time + fireRate;
			//实例化子弹类(创建对象)  Instantiate:实例化对象 (目标物体：original,位置：Position, 发射方向:rotation)
			Instantiate (shot2, shotSpawn.position, shotSpawn.rotation);
			
			GetComponent<AudioSource>().Play();
		}
	}
	void jump_button2(){
		if( transform == null){
			return;
		}
		if (Time.time > nextFire) {//Fire1 预设按钮（ctrl） ，Time.time 游戏运行时间
			nextFire = Time.time + fireRate;
			//实例化子弹类(创建对象)  Instantiate:实例化对象 (目标物体：original,位置：Position, 发射方向:rotation)
			Instantiate (shot3, shotSpawn.position, shotSpawn.rotation);
			
			GetComponent<AudioSource>().Play();
		}
	}
	
}







