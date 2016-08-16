using UnityEngine;
using System.Collections;

public class MD_monstars : CSV<DD_monsters>
{
	private static readonly string FilePath = "csv/monsters";
	public void Load( ) { ImportCSV(FilePath); }
}

public class DD_monsters : MasterBase
{
	public int enable { get; private set; }
	public int id { get; private set; }
	public string name { get; private set; }
	public string image { get; private set; }
	public int hp { get; private set; }
	public int speed { get; private set; }
	public int damage { get; private set; }
	public int atk { get; private set; }
	public int def { get; private set; }
	public int magic_damage { get; private set; }
	public int magic_atk { get; private set; }
	public int magic_def { get; private set; }
	public int action_id_1 { get; private set; }
	public int action_id_2 { get; private set; }
	public int action_id_3 { get; private set; }
	public int action_id_4 { get; private set; }
	public int drop_item_id { get; private set; }
	public int drop_item_prob { get; private set; }
	public int drop_gold { get; private set; }
	public int exp { get; private set; }
	public int attack_times { get; private set; }
}