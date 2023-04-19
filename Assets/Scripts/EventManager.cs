using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EventManager : MonoBehaviour
{
    #region  свойства
    //общий доступ к экземпляру 
    public static EventManager Instance
    {
        get
        {
            return instance;
        }
        set { }
    }
    #endregion
    #region Переменные
    private static EventManager instance = null;


    private Dictionary<EVENT_TYPE, List<IListener>> Listeners = new Dictionary<EVENT_TYPE, List<IListener>>();
    #endregion

    #region методы
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
    /// Функция добавления получателя в массив
    /// </summary>
    /// <param name="Event_Type">Событие ожидаемое получателем</param>
    /// <param name="Listener">Объект, ожидающий событие</param>
    public void AddListener(EVENT_TYPE Event_Type, IListener Listener)
    {
        //Список получателей данного события
        List<IListener> ListenList = null;

        //Проверить тип события. Если существует  - добавить в список
        if (Listeners.TryGetValue(Event_Type, out ListenList))
        {
            //Список существует, добавить новый элемент
            ListenList.Add(Listener);
            return;
        }
        //Иначе создать список как ключ словаря
        ListenList = new List<IListener>();
        ListenList.Add(Listener);
        Listeners.Add(Event_Type, ListenList);
    }

    /// <summary>
    /// Посылает события получателям
    /// </summary>
    /// <param name="Event_Type">Событие для вызова</param>
    /// <param name="Sender">Выщываемый объект</param>
    /// <param name="Param">Необязательный аргумент</param>
    public void PostNotification(EVENT_TYPE Event_Type, Component Sender, Object Param = null)
    {
        //Послать событие всем получателям
        //Список получателей только для данного события
        List<IListener> ListenList = null;
        //Если получателей нет - выйти
        if (!Listeners.TryGetValue(Event_Type, out ListenList)) return;

        //Получатели есть. Послать им событие
        for (int i = 0; i < ListenList.Count; i++)
        {
            if (!ListenList[i].Equals(null))
                ListenList[i].OnEvent(Event_Type, Sender, Param);
        }
    }

    public void RemoveEvent(EVENT_TYPE Event_Type)
    {
        //Удалить запись из словаря
        Listeners.Remove(Event_Type);
    }

    //Удаляет все избыточные записи из словаря
    public void RemoveRedundancies()
    {
        //Создать новый словарь
        Dictionary<EVENT_TYPE, List<IListener>> TmpListeners = new Dictionary<EVENT_TYPE, List<IListener>>();
        //Обойти все записи в словаре
        foreach (KeyValuePair<EVENT_TYPE, List<IListener>> Item in Listeners)
        {
            //Обойти всех получателей, удалить пустые ссылки
            for (int i = Item.Value.Count - 1; i >= 0; i--)
                //Если ссылка пустая, удалить элемент
                if (Item.Value[i].Equals(null))
                    Item.Value.RemoveAt(i);
            //Если вы списке остались элементы, добавить его в словарь tmp
            if (Item.Value.Count > 0)
                TmpListeners.Add(Item.Key, Item.Value);
        }
        //Заменить объект Listeners новым словарем
        Listeners = TmpListeners;
    }

    //Вызывается при смене сцены.  Очищает словарь
    private void OnLevelWasLoaded()
    {
        RemoveRedundancies();
    }
    #endregion

}

