using System;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerItem : MonoBehaviourPunCallbacks
{
    public TMP_Text playerName;
    private Player _player;
    private ExitGames.Client.Photon.Hashtable _playerProperties;

    private Image _backgroundImage;
    public Color highlightColor;
    public Sprite[] avatars;
    public Image playerAvatar;
    public GameObject leftArrowB;
    public GameObject rightArrowB;

    private PhotonView _view;
    private void Awake()
    {
        _playerProperties = new ExitGames.Client.Photon.Hashtable();
        _backgroundImage = GetComponent<Image>();
        _view = GetComponent<PhotonView>();
    }

    public void SetPlayerInfo(Player player)
    {
        if (player == null) return;
        playerName.text = player.NickName;
        _player = player;
        UpdatePlayerItem(player);
    }

    public void ApplyLocalChanges()
    {
        _backgroundImage.color = highlightColor;
        leftArrowB.SetActive(true);
        rightArrowB.SetActive(true);
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable plProperties)
    {
        if (_player==targetPlayer)
        {
            UpdatePlayerItem(targetPlayer);
        }
    }

    private void UpdatePlayerItem(Player player)
    {
        if (player.CustomProperties.ContainsKey(nameof(playerAvatar)))
        {
            playerAvatar.sprite = avatars[(int)player.CustomProperties[nameof(playerAvatar)]];
            // Para guardar el avatar si sale del room y vuelve a entrar
            _playerProperties[nameof(playerAvatar)] = (int)player.CustomProperties[nameof(playerAvatar)];
        }
        else
        {//Evita error si no tenia datos antes y rompe el alma si tocas flechitas
            _playerProperties[nameof(playerAvatar)] = 0;
        }
    }

    public void OnClickLeftArrow()
    {
        _playerProperties[nameof(playerAvatar)] = (int)_playerProperties[nameof(playerAvatar)] == 0
            ? avatars.Length - 1
            : (int)_playerProperties[nameof(playerAvatar)] - 1;
        PhotonNetwork.SetPlayerCustomProperties(_playerProperties);
    }
    public void OnClickRighttArrow()
    {
        _playerProperties[nameof(playerAvatar)] = (int)_playerProperties[nameof(playerAvatar)] == avatars.Length - 1
            ? 0
            : (int)_playerProperties[nameof(playerAvatar)] + 1;
        PhotonNetwork.SetPlayerCustomProperties(_playerProperties);
    }
}