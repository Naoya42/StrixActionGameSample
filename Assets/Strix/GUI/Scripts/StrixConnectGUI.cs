using SoftGear.Strix.Unity.Runtime;
using SoftGear.Strix.Net.Logging;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using SoftGear.Strix.Unity.Runtime.Event;

public class StrixConnectGUI : MonoBehaviour {
    [Header("ダッシュボードのマスター ホスト名")]
    public string host = "127.0.0.1";
    [Header("Strix Cloudでは常に9122")]
    public int port = 9122;
    [Header("ダッシュボードの情報タブから確認できる")]
    public string applicationId = "00000000-0000-0000-0000-000000000000";
    public Level logLevel = Level.INFO;
    public InputField playerNameInputField;
    public Text statusText;
    public Button connectButton;
    public UnityEvent OnConnect;

    void OnEnable()
    {
        statusText.text = "";
        connectButton.interactable = true;
    }

    //UIのConnectButtonから呼び出し
    public void Connect() {
        LogManager.Instance.Filter = logLevel;

        //StrixNetworkにapplicationIdとplayerNameの値を設定
        StrixNetwork.instance.applicationId = applicationId;
        StrixNetwork.instance.playerName = playerNameInputField.text;

        //hostとportを引数にアプリケーションのマスターサーバーへ接続
        StrixNetwork.instance.ConnectMasterServer(host, port, OnConnectCallback, OnConnectFailedCallback);

        statusText.text = "Connecting MasterServer " + host + ":" + port;

        connectButton.interactable = false;
    }

    /// <summary>
    /// 接続成功時に呼び出されるコールバック
    /// </summary>
    /// <param name="args"></param>
    private void OnConnectCallback(StrixNetworkConnectEventArgs args)
    {
        statusText.text = "Connection established";

        OnConnect.Invoke();

        gameObject.SetActive(false);
    }

    /// <summary>
    /// 接続失敗時に呼び出されるコールバック
    /// </summary>
    /// <param name="args"></param>
    private void OnConnectFailedCallback(StrixNetworkConnectFailedEventArgs args) {
        string error = "";

        if (args.cause != null) {
            error = args.cause.Message;
        }

        statusText.text = "Connect " + host + ":" + port + " failed. " + error;
        connectButton.interactable = true;
    }
}
