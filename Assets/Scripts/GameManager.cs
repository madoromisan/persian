using UnityEngine;
using System.Collections;

using System.Collections.Generic;       //Allows us to use Lists. 
using UnityEngine.UI;                   //Allows us to use UI.

public enum EnumGUIEvent
{
	TAMERU_ENERGY
}

public class GameManager : EventController, IRecieveMessage
{
	public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
	public static GameObject go = null;
	public DynamicData dd;

	//==================================================================================
	//	Awake is always called before any Start functions
	//==================================================================================
	void Awake( )
	{
		if (instance == null) instance = this;          //	nullなら自分自身を保持
		else if (instance != this) Destroy(gameObject); //	自分以外のインスタンスがすでにあるなら自分が偽物なのでぶら下がり元を破壊
		DontDestroyOnLoad(gameObject);
		go = gameObject;
		Debug.Log("GameManager Awake!");
		//	ゲーム初期化
		InitGame( );
		//	イベント登録
		AddEventFunc(EnumGUIEvent.TAMERU_ENERGY, (InnerPostEventHandler)TAMERU_ENERGY);
	}
	//==================================================================================
	//	Initializes the game for each level.
	//==================================================================================
	void InitGame( )
	{
		//	マスターデータ初期化
		MD_monstars md_monsters = new MD_monstars( );
		md_monsters.Load( );
		//foreach (var m in md_monsters.All) Debug.Log(m.name);
		//	ユーザーデータ初期化
		dd = new DynamicData( );
		dd.nCount = 0;
	}
	//==================================================================================
	//	Update is called every frame.
	//==================================================================================
	void Update( )
	{
	}
	//==================================================================================
	//	
	//==================================================================================
	private void TAMERU_ENERGY(object sender, InnerPostEventArgs e)
	{
		dd.nCount++;
		Debug.Log(dd.nCount);
		SaveData.SetClass<DynamicData>("p1", dd);
		SaveData.Save( );
	}
	public void OnRecieve( )
	{
		Debug.Log("I got a Message!");
	}
}

