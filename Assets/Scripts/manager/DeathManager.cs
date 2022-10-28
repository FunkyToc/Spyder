using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathManager : MonoBehaviour
{
    [SerializeField] PlayerSO _player;
    [SerializeField] SpawnSO _spawnpoint;

    public void LastCheckPoint()
    {
        _player.Player.transform.position = _spawnpoint.Spawn.transform.TransformDirection(_spawnpoint.Spawn.transform.position);
    }

    public void Death()
    {
        _player.Player.transform.position = _spawnpoint.Spawn.transform.TransformDirection(_spawnpoint.Spawn.transform.position);
        GameManager.GM._lm.ReloadLevel();
    }
}
