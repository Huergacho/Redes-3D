using Photon.Pun;
using Photon.Realtime;
using TMPro;
public class PlayerItem : MonoBehaviourPunCallbacks
{

    public TMP_Text playerName;
    private Player _player;
    private ExitGames.Client.Photon.Hashtable _playerProperties;

    private void Awake()
    {
        _playerProperties = new ExitGames.Client.Photon.Hashtable();
    }

    public void SetPlayerInfo(Player player)
    {
        if (player == null) return;
        playerName.text = player.NickName;
        player = _player;
        _playerProperties["name"] = _player.NickName;
        UpdatePlayerItem(player);
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable plProperties)
    {
        if (_player.Equals(targetPlayer))
        {
            UpdatePlayerItem(targetPlayer);
        }
    }

    private void UpdatePlayerItem(Player player)
    {
        if (player.CustomProperties.ContainsKey("name"))
        {
            _player.NickName = (string)player.CustomProperties["name"];
        }
    }
}
