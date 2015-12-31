using UnityEngine;
using System.Collections;

/**
 * 触发容器 Cube
 * 使用 当容器内物体离开此容器时触发
 * 场景盒子，容器触发器，跟场景一样尺寸 允许场景物体的范围控制
 * （在子弹离开场景后，销毁子弹）当其他碰撞器停止与触发容器的容量接触（离开了触发器的容量时）时，销毁这个物体
 * 给 Boundary Add 一个Tag 命名为 Boundary
 * */

public class DestroyByBoundary : MonoBehaviour {

	//trigger

	////离开触发器时，销毁所有物体,当其他碰撞器停止与触发容器的容量接触时(离开此容器时)，销毁这个物体
	void OnTriggerExit(Collider other) {
		// Destroy everything that leaves the trigger
		Destroy(other.gameObject);
	}

	/**
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	**/
}
