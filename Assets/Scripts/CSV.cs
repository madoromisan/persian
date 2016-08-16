using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

//================================================================================
/// <summary>CSVパーサ</summary>
//================================================================================
public class CSV<T> where T : MasterBase, new()
{
	protected List<T> masters;
	public List<T> All { get { return masters; } }

	/// <summary>データテンポラリ</summary>
	private List<string[]> m_list_rows = new List<string[]>( );
	private List<string> m_list_lines = new List<string>( );
	/// <summary>カラムヘッダ</summary>
	private string[] m_array_szColumnHeader;
	/// <summary>カラム数</summary>
	private int m_nColumns = 0;
	/// <summary>行数</summary>
	public int nRows { get { return m_list_rows.Count; } }
	//================================================================================
	//	
	//================================================================================
	public CSV( )
	{

	}
	//================================================================================
	//
	//================================================================================
	protected bool ImportCSV(string szFilePath)
	{
		//	読み込み
		string szRawText = ( (TextAsset)Resources.Load(szFilePath, typeof(TextAsset)) ).text;
		szRawText = szRawText.Trim( ).Replace("\r", "") + "\n";
		List<string> RawLines = szRawText.Split('\n').ToList( );

		//	読み込み準備
		string[] szField;
		bool bHead = false;
		m_nColumns = 0;
		m_list_rows.Clear( );
		//	読み込み実行
		foreach (string szLine in RawLines)
		{
			//	行を分割
			szField = szLine.Split(',');
			//	先頭カラムが#ならその行は無視する
			if (szField[0] == "#") continue;
			//	先頭カラムが0ならその行は無視する
			if (szField[0] == "0") continue;
			//	長さがおかしければスキップ
			if (bHead == true && szField.Length != m_nColumns) continue;
			//	ヘッダ処理
			if (bHead == false)
			{
				bHead = true;
				m_nColumns = szField.Length;
				m_array_szColumnHeader = szField;
				continue;
			}
			m_list_rows.Add(szField);
			m_list_lines.Add(szLine);
		}
		masters = new List<T>( );
		foreach (var line in m_list_lines) ParseLine(line, m_array_szColumnHeader);
		return true;
	}
	private void ParseLine(string line, string[] headerElements)
	{
		var elements = line.Split(',');
		if (elements.Length == 1) return;
		if (elements.Length != headerElements.Length)
		{
			Debug.LogWarning(string.Format("can't load: {0}", line));
			return;
		}

		var param = new Dictionary<string, string>( );
		for (int i = 0 ; i < elements.Length ; ++i) param.Add(headerElements[i], elements[i]);
		var master = new T( );
		master.Load(param);
		masters.Add(master);
	}

}
public class MasterBase
{
	public void Load(Dictionary<string, string> param)
	{
		foreach (string key in param.Keys) SetField(key, param[key]);
	}
	private void SetField(string key, string value)
	{
		PropertyInfo propertyInfo = this.GetType( ).GetProperty(key, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
		if (propertyInfo.PropertyType == typeof(int)) propertyInfo.SetValue(this, int.Parse(value), null);
		else if (propertyInfo.PropertyType == typeof(string)) propertyInfo.SetValue(this, value, null);
		else if (propertyInfo.PropertyType == typeof(double)) propertyInfo.SetValue(this, double.Parse(value), null);
		else if (propertyInfo.PropertyType == typeof(float)) propertyInfo.SetValue(this, float.Parse(value), null);
	}
}
