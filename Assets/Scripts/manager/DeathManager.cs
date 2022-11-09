using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathManager : MonoBehaviour
{
    [SerializeField] GameObject _ragdoll;
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

    public void SpawnRagdoll()
    {
        GameObject rd = GameObject.Instantiate(_ragdoll, _player.Player.transform.position, _player.Player.transform.rotation);
        rd.GetComponent<RagdollDeath>().EnableRagdoll();
    }
}
