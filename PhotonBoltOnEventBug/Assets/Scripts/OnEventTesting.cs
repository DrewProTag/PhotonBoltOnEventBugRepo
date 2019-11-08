using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Bolt;
using Bolt.Matchmaking;
using UdpKit;

public class OnEventTesting : Bolt.GlobalEventListener
{
    public Text statusText;
    private string _matchName;

    private void Awake()
    {
    }

    public void ButtonPressOnHost()
    {
        BoltLauncher.StartServer();
        statusText.text = "Status: StartingServer";
    }

    public void ButtonPressOnJoin()
    {
        BoltLauncher.StartClient();
        statusText.text = "Status: StartingClient";
    }

    public void ButtonPressOnShutdown()
    {
        BoltLauncher.Shutdown();
        statusText.text = "Status: Shutting Down";
    }

    public void ButtonPressOnSendEvent()
    {
        EventTest message = EventTest.Create();
        message.Message = string.Format("RandomNum:{0}", Random.Range(-10.0f, 10.0f));
        message.Send();
        statusText.text = string.Format("Status: Sending Message \"{0}\"", message.Message);
    }

    public override void OnEvent(EventTest evnt)
    {
        statusText.text = string.Format("Status: Received Message \"{0}\"", evnt.Message);
    }

    public override void BoltStartDone()
    {
        statusText.text = string.Format("Status: BoltStartDone \"{0}\"", BoltNetwork.IsServer ? "Server" : "Client");

        if ( BoltNetwork.IsServer )
        {
            _matchName = System.Guid.NewGuid().ToString();
            BoltMatchmaking.CreateSession(sessionID: _matchName);
        }
    }

    public override void SessionCreated(UdpSession session)
    {
        if ( BoltNetwork.IsServer )
        {
            statusText.text = string.Format("Status: SessionCreated matchName:{0}", _matchName);
        }
    }

    public override void SessionConnected(UdpSession session, IProtocolToken token)
    {
        statusText.text = string.Format("Status: SessionConnected Id:{0}", session.Id);
    }

    public override void SessionListUpdated(Map<System.Guid, UdpSession> sessionList)
    {
        if ( BoltNetwork.IsClient )
        {
            foreach ( var pair in sessionList )
            {
                UdpSession session = pair.Value;
                BoltNetwork.Connect(session);
                statusText.text = string.Format("Status: Connecting to Id:{0}", session.Id);
            }
        }
    }

    public override void BoltShutdownBegin(AddCallback registerDoneCallback)
    {
        statusText.text = "Status: Shut Down Began";
        registerDoneCallback(() =>
        {
            statusText.text = "Status: Shut Down Complete";
        });
    }
}
