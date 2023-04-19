using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EventManager : MonoBehaviour
{
    #region  ��������
    //����� ������ � ���������� 
    public static EventManager Instance
    {
        get
        {
            return instance;
        }
        set { }
    }
    #endregion
    #region ����������
    private static EventManager instance = null;


    private Dictionary<EVENT_TYPE, List<IListener>> Listeners = new Dictionary<EVENT_TYPE, List<IListener>>();
    #endregion

    #region ������
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            DestroyImmediate(this);
    }
    /// <summary>
    /// ������� ���������� ���������� � ������
    /// </summary>
    /// <param name="Event_Type">������� ��������� �����������</param>
    /// <param name="Listener">������, ��������� �������</param>
    public void AddListener(EVENT_TYPE Event_Type, IListener Listener)
    {
        //������ ����������� ������� �������
        List<IListener> ListenList = null;

        //��������� ��� �������. ���� ����������  - �������� � ������
        if (Listeners.TryGetValue(Event_Type, out ListenList))
        {
            //������ ����������, �������� ����� �������
            ListenList.Add(Listener);
            return;
        }
        //����� ������� ������ ��� ���� �������
        ListenList = new List<IListener>();
        ListenList.Add(Listener);
        Listeners.Add(Event_Type, ListenList);
    }

    /// <summary>
    /// �������� ������� �����������
    /// </summary>
    /// <param name="Event_Type">������� ��� ������</param>
    /// <param name="Sender">���������� ������</param>
    /// <param name="Param">�������������� ��������</param>
    public void PostNotification(EVENT_TYPE Event_Type, Component Sender, Object Param = null)
    {
        //������� ������� ���� �����������
        //������ ����������� ������ ��� ������� �������
        List<IListener> ListenList = null;
        //���� ����������� ��� - �����
        if (!Listeners.TryGetValue(Event_Type, out ListenList)) return;

        //���������� ����. ������� �� �������
        for (int i = 0; i < ListenList.Count; i++)
        {
            if (!ListenList[i].Equals(null))
                ListenList[i].OnEvent(Event_Type, Sender, Param);
        }
    }

    public void RemoveEvent(EVENT_TYPE Event_Type)
    {
        //������� ������ �� �������
        Listeners.Remove(Event_Type);
    }

    //������� ��� ���������� ������ �� �������
    public void RemoveRedundancies()
    {
        //������� ����� �������
        Dictionary<EVENT_TYPE, List<IListener>> TmpListeners = new Dictionary<EVENT_TYPE, List<IListener>>();
        //������ ��� ������ � �������
        foreach (KeyValuePair<EVENT_TYPE, List<IListener>> Item in Listeners)
        {
            //������ ���� �����������, ������� ������ ������
            for (int i = Item.Value.Count - 1; i >= 0; i--)
                //���� ������ ������, ������� �������
                if (Item.Value[i].Equals(null))
                    Item.Value.RemoveAt(i);
            //���� �� ������ �������� ��������, �������� ��� � ������� tmp
            if (Item.Value.Count > 0)
                TmpListeners.Add(Item.Key, Item.Value);
        }
        //�������� ������ Listeners ����� ��������
        Listeners = TmpListeners;
    }

    //���������� ��� ����� �����.  ������� �������
    private void OnLevelWasLoaded()
    {
        RemoveRedundancies();
    }
    #endregion

}

