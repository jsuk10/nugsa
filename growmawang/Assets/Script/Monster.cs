using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
	public Tile currentTile;
	[SerializeField] Animator anim;
    //펄스면 1소환 트루면 2소환
    public bool mobstate = false;

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
        mobstate = false;
    }
    public void Grow2()
    {
        anim.SetTrigger("Grow2");
        mobstate = true;
    }
    public void Harvest()
    {
        anim.SetTrigger("Harvest");
        mobstate = false;
    }
    public void Harvest2()
    {
        anim.SetTrigger("Harvest2");
        mobstate = false;
    }
    public void Mob2ready()
    {
        anim.SetTrigger("Mob2ready");
    }
}
