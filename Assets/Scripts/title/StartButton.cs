using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class StartButton : EventController
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
		//	SendMessageUpwards("CLICK_START",null,SendMessageOptions.DontRequireReceiver);
		//ExecuteEvents.Execute<IRecieveMessage>(
		//	target: GameManager.go, // 呼び出す対象のオブジェクト
		//	eventData: null,  // イベントデータ（モジュール等の情報）
		//	functor: (recieveTarget, y) => recieveTarget.OnRecieve());
		m_InnerPostEventArgs = new InnerPostEventArgs(EnumGUIEvent.TAMERU_ENERGY);
		BridgeEvent( );
		Debug.Log("push");
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