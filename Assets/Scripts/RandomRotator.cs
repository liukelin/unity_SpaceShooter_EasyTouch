using UnityEngine;
using System.Collections;

/**
 * 动作类 （物体旋转 翻滚）
 * 用于：行星翻滚
 * 
 * 设置 Rigidbody 两个阻力: Drag=0 ,Anguar Drag=0 不然物体会慢慢停下
 * */
public class RandomRotator : MonoBehaviour {

	public float tumble;//最大翻滚值 速率

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * tumble;//angularVelocity 刚体的角速度(旋转速率),Random 随机数,insideUnitSphere 属于Vector3 值 可对 X,Y,Z 单独随机
		//rigidbody.velocity = transform.forward * -tumble;//沿Z轴
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
