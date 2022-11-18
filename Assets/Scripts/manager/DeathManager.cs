using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DeathManager : MonoBehaviour
{
    [SerializeField] GameObject _ragdoll;
    [SerializeField] PlayerSO _player;
    [SerializeField] SpawnSO _spawnpoint;
    GameObject _model;

    void Start()
    {
        _player.OnPlayerChanged += _OnPlayerChanged;
    }

    void _OnPlayerChanged()
    {
        _model = _player.Player.GetComponentInChildren<PlayerModelTag>().gameObject;
    }

    public void LastCheckPoint()
    {
        _player.Player.transform.position = _spawnpoint.Spawn.transform.TransformDirection(_spawnpoint.Spawn.transform.position);
    }

    public void ReloadLevel()
    {
        _player.Player.transform.position = _spawnpoint.Spawn.transform.TransformDirection(_spawnpoint.Spawn.transform.position);
        GameManager.GM._lm.ReloadLevel();
    }

    public void SpawnRagdoll()
    {
        GameObject rd = GameObject.Instantiate(_ragdoll, _player.Player.transform.position, _player.Player.transform.rotation);
        rd.GetComponent<RagdollDeath>().EnableRagdoll();
    }

    public void DeathWithRagdoll(float _deathAnimTime = 1f, bool spawnRagdoll = true)
    {
        StartCoroutine(DeathAnim(_deathAnimTime, spawnRagdoll));
    }

    IEnumerator DeathAnim(float _deathAnimTime, bool spawnRagdoll)
    {
        // death anim
        if (spawnRagdoll) SpawnRagdoll();
        _player.Player.GetComponent<PlayerInput>().enabled = false;
        _model.SetActive(false);

        yield return new WaitForSeconds(_deathAnimTime);

        // respawn
        LastCheckPoint();
        _model.SetActive(true);
        _player.Player.GetComponent<PlayerInput>().enabled = true;
    }
}
