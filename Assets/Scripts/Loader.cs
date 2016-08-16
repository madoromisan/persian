using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour
{
	//	論理オブジェクトを顕現（ぶら下げる）させるためのGameObject
	public GameObject gameManager;          //GameManager prefab to instantiate.
	public GameObject soundManager;         //SoundManager prefab to instantiate.
	void Awake( )
	{
		//	論理オブジェクトのインスタンスが存在しなければぶら下がるGameObjectのインスタンスが存在しないということなので生成
		if (GameManager.instance == null) Instantiate(gameManager);
		if (SoundManager.instance == null) Instantiate(soundManager);
	}
}
