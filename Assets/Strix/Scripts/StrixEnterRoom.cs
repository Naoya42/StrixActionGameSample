using System.Collections.Generic;
using SoftGear.Strix.Unity.Runtime;
using UnityEngine;
using UnityEngine.Events;

public class StrixEnterRoom : MonoBehaviour {
    [Header("ルームに参加可能な最大人数")]
    /// <summary>
    /// ルームに参加可能な最大人数
    /// </summary>
    public int capacity = 4;

    [Header("ルーム名")]
    /// <summary>
    /// ルーム名
    /// </summary>
    public string roomName = "New Room";

    [Header("ルーム入室完了時イベント")]
    /// <summary>
    /// ルーム入室完了時イベント
    /// </summary>
    public UnityEvent onRoomEntered;

    [Header("ルーム入室失敗時イベント")]
    /// <summary>
    /// ルーム入室失敗時イベント
    /// </summary>
    public UnityEvent onRoomEnterFailed;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    /// <summary>
    /// ランダムなルームへの接続を試みる
    /// </summary>
    public void EnterRoom() {
        //解説↓
        #region
        //この呼び出しは、以前に設定されたプレイヤー名に加え、
        //成功と失敗のハンドラーを受け取ります。
        //成功するとonRoomEnteredUnityEventを起動します。
        //失敗するとCreateRoomメソッドを呼び出します。
        //https://www.strixengine.com/doc/unity/guide/ja/unitysdk/matchmaking/gameloop/joinroom.html
        #endregion

        StrixNetwork.instance.JoinRandomRoom(StrixNetwork.instance.playerName, args => {
            onRoomEntered.Invoke();
        }, args => {
            CreateRoom();
        });
    }

    private void CreateRoom() {
        
        //Strixでルームを作成するために必要なプロパティを作成

        //指定された定員とルーム名でRoomPropertiesオブジェクトを作成
        RoomProperties roomProperties = new RoomProperties {
            capacity = capacity,
            name = roomName
        };

        //プレイヤーの名前でRoomMemberPropertiesオブジェクトを作成
        RoomMemberProperties memberProperties = new RoomMemberProperties {
            name = StrixNetwork.instance.playerName
        };


        StrixNetwork.instance.CreateRoom(roomProperties, memberProperties, args => {
            onRoomEntered.Invoke();
        }, args => {
            onRoomEnterFailed.Invoke();
        });
    }
}
