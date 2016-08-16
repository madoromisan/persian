using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class LoadButton : EventController
{
	//	インスペクターから設定
	public Sprite DefaultSprite;
	public Sprite OverSprite;
	private Image buttonimage;
	void Start( )
	{
		//	ボタンイメージ変更用にコンポーネント取得
		buttonimage = gameObject.GetComponent<Image>( );
		//	イベント接続
		GameManager.instance.AddChildEventController(this, GameManager.instance);
	}
	public void ButtonPush( )
	{
		SaveData.Load( );
		DynamicData d = SaveData.GetClass<DynamicData>("p1", null);
		if (d != null) Debug.Log(d.nCount);
	}
	public void MouseOver()
	{
		//	Debug.Log("Button Mouse Over !!");
		buttonimage.sprite = OverSprite;
	}
	public void MouseExit()
	{
		buttonimage.sprite = DefaultSprite;

	}

}