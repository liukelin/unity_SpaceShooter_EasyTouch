using UnityEngine;
using System.Collections;

/**
 * 动作类(沿着Z轴运动)
 * 用于：子弹运动,行星运动
 * 
 * 
 * 子弹动作Prefabs创建注释
 * 创建空的GameObject 然后在下面创建 Quad作为子弹效果-拖入创建好的 Material （跟背景材质一样类型）
 * 创建材质 Materials文件夹下 Create->Material  选中材质 select 让子弹纹理与其关联
 * 子弹控制类 (出现在场景时候 自动向前)
 * 胶囊碰撞器 capsule Collider  设置 Is Trigger (触发器)
 *	
 * 
*/
public class Mover : MonoBehaviour {

	public float speed;//速度 倍数

	//实例化自动运行 Use this for initialization
	void Start () {
	
		//控制速度 延Z轴运动:transform.forward  (transform : 控制对象的位置，尺寸，旋转)  沿X轴:transform.right   沿Y轴:transform.up 
		GetComponent<Rigidbody>().velocity = transform.forward * speed;
	}

	/**
	// Update is called once per frame
	void Update () {
	
	}
	**/
}
