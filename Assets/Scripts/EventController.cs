using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//================================================================================
//	イベントブリッジクラス
//================================================================================
public class EventController : MonoBehaviour
{
	//--------------------------------------------------------------------------------
	//	静的プロパティ
	//--------------------------------------------------------------------------------
	//--------------------------------------------------------------------------------
	//	プロパティ
	//--------------------------------------------------------------------------------
	/// <summary>子イベントオブジェクトのリスト</summary>
	protected List<EventController> m_list_Child_EventController = null;
	/// <summary>親に投げるイベント</summary>
	public event InnerPostEventHandler InnerPostEvent;
	/// <summary>子供から投げられるイベントの関数テーブル</summary>
	protected Dictionary<object, InnerPostEventHandler> m_Dictionary_Event_Func;
	/// <summary>派生クラスから親にイベントを投げる際のブリッジ</summary>
	protected InnerPostEventArgs m_InnerPostEventArgs;
	/// <summary>派生クラスから親にイベントを投げる際のブリッジ</summary>
	public InnerPostEventArgs InnerPostEventArgs { set { m_InnerPostEventArgs = value; } }
	//--------------------------------------------------------------------------------
	//	プロパティ
	//--------------------------------------------------------------------------------
	/// <summary>親イベントオブジェクト</summary>
	protected EventController m_EventController_Parent = null;
	/// <summary>親イベントオブジェクト</summary>
	public EventController Drawable_Parent { set { m_EventController_Parent = value; } get { return m_EventController_Parent; } }
	//--------------------------------------------------------------------------------
	//	コンストラクタ
	//--------------------------------------------------------------------------------
	public EventController( )
	{
		//	子供リスト
		m_list_Child_EventController = new List<EventController>( );
		//	イベントハッシュ
		m_Dictionary_Event_Func = new Dictionary<object, InnerPostEventHandler>( );
	}
	static EventController( )
	{
	}
	//--------------------------------------------------------------------------------
	//	イベントハンドラにイベント別処理関数を追加
	//	サンプル
	//			AddEventFunc( EnumGUIEvent.TAMERU_ENERGY, ( InnerPostEventHandler )TAMERU_ENERGY );
	//	定義部
	//			private void TAMERU_ENERGY( object sender, InnerPostEventArgs e ) { …
	//--------------------------------------------------------------------------------
	protected void AddEventFunc(object oArgsObject, InnerPostEventHandler func)
	{
		if (m_Dictionary_Event_Func.ContainsKey(oArgsObject) == true) return;
		m_Dictionary_Event_Func.Add(oArgsObject, func);
	}
	//----------------------------------------------------------------------
	//	子供のイベントハンドラ
	//----------------------------------------------------------------------
	virtual public void Invoke_InnerPostEvent(object sender, InnerPostEventArgs e)
	{
		//	ここでハンドルするイベントかどうかチェック
		if (m_Dictionary_Event_Func.ContainsKey(e.oArgsObject) == true)
		{
			//	処理関数実行
			( (InnerPostEventHandler)m_Dictionary_Event_Func[e.oArgsObject] )(sender, e);
		}
		//	親（シーン）に渡す
		if (InnerPostEvent == null) return;
		InnerPostEvent(sender, e);
	}
	//----------------------------------------------------------------------
	/// <summary>派生先のクラスでイベントを起こすためのブリッジ。
	/// 使用の際はあらかじめ _InnerPostEventArgs を割り当てておくこと</summary>
	//----------------------------------------------------------------------
	protected void BridgeEvent( )
	{
		InnerPostEvent(this, m_InnerPostEventArgs);
	}
	//--------------------------------------------------------------------------------
	/// <summary>基底では子供のInitialize()を呼ぶだけ</summary>
	//--------------------------------------------------------------------------------
	public virtual void Initialize( )
	{
		for (int i = 0 ; i < m_list_Child_EventController.Count ; i++)
		{
			m_list_Child_EventController[i].Initialize( );
		}
	}
	//--------------------------------------------------------------------------------
	/// <summary>イベント子オブジェクトの追加。先に追加されたものから順にメッセージ伝播される</summary>
	//--------------------------------------------------------------------------------
	public virtual void AddChildEventController(EventController eventctlr, EventController parent)
	{
		//	すでに存在する子供かチェック
		if (m_list_Child_EventController.Contains(eventctlr) == true) return;
		//	親を設定
		eventctlr.m_EventController_Parent = parent;
		//	リストに追加
		m_list_Child_EventController.Add(eventctlr);
		//	追加した子ウィンドウのイベントハンドラを設定する
		eventctlr.InnerPostEvent += new InnerPostEventHandler(Invoke_InnerPostEvent);
	}
	//--------------------------------------------------------------------------------
	/// <summary>イベント子オブジェクトの削除</summary>
	//--------------------------------------------------------------------------------
	public virtual void RemoveChildEventController(EventController eventctlr)
	{
		//	存在チェック
		if (m_list_Child_EventController.Contains(eventctlr) == false) return;
		eventctlr.m_EventController_Parent = null;
		m_list_Child_EventController.Remove(eventctlr);
		eventctlr.InnerPostEvent -= new InnerPostEventHandler(Invoke_InnerPostEvent);
	}
}

