using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
	public Tile currentTile;
	[SerializeField] Character player;
	[SerializeField] Animator anim;

	private void Start()
	{
		transform.position = currentTile.transform.position;
	}
	public void leftSpawn()
	{
		int spawnIndex = Random.Range(0, Manager.manager.Mob_R.currentTile.index - 2);
		transform.position = Map.map.tiles[spawnIndex].transform.position;
		currentTile = Map.map.tiles[spawnIndex];
	}
	public void rightSpawn()
	{
		int spawnIndex = Random.Range(Manager.manager.Mob_L.currentTile.index + 3, 6);
		transform.position = Map.map.tiles[spawnIndex].transform.position;
		currentTile = Map.map.tiles[spawnIndex];
	}
	public void Grow()
	{
		anim.SetTrigger("Grow");
	}
	public void Harvest()
	{
		anim.SetTrigger("Harvest");
	}

}
