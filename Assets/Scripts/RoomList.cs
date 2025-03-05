using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class RoomList : MonoBehaviour
{
    private const int EntrySpacing = 135;
    private const int EntryStart = 585;
    private const int EntryHeight = 115;
    private const int EntryX = 105;
    private const int EntryWidth = 1400;

    public UnityEngine.Transform roomList;
    
    public RoomEntry editRoomPrefab;
    public RoomEntry viewRoomPrefab;
    
    public GameObject previousButton;
    public GameObject nextButton;
    
    private GetRoomsResponseDto[] _entries;
    private readonly List<GameObject> _loaded = new();
    private int maxPages;
    private int currentPage;
    
    private async void Start()
    {
        await GetRooms();
        LoadCurrentPage();
    }

    private async Task GetRooms()
    {
        _entries = await ApiClientRoomChoiceSingleton.Instance.GetRoomsList();
        maxPages = Mathf.CeilToInt(_entries.Length / 5f);
    }

    public void NextPage()
    {
        currentPage = Mathf.Min(currentPage + 1, maxPages);
        LoadCurrentPage();
    }

    public void PreviousPage()
    {
        currentPage = Mathf.Max(0, currentPage - 1);
        LoadCurrentPage();
    }

    private void LoadCurrentPage()
    {
        foreach (var obj in _loaded)
        {
            Destroy(obj);
        }
        var pageEntries = _entries[(currentPage * 5)..Mathf.Min(currentPage * 5 + 5, _entries.Length)];

        previousButton.GetComponent<Button>().interactable = currentPage > 0;
        nextButton.GetComponent<Button>().interactable = currentPage < maxPages - 1;
        
        if (_entries.Length > 5)
        {
            nextButton.SetActive(true);
            previousButton.SetActive(true);

            
        }
        else
        {
            nextButton.SetActive(false);
            previousButton.SetActive(false);
        }

        for (var index = 0; index < pageEntries.Length; index++)
        {
            var entry = pageEntries[index];
            var isOwner = entry.isOwner;
            var roomEntry = Instantiate(isOwner ? editRoomPrefab : viewRoomPrefab,
                new Vector3(EntryX, EntryStart + index * (EntrySpacing + EntryHeight)), Quaternion.identity, roomList);
            roomEntry.text.text = entry.name;
            roomEntry.Room = entry;
            var rectangleTransform = roomEntry.GetComponent<RectTransform>();
            rectangleTransform.anchoredPosition = new Vector3(EntryX,  EntryStart - index * (EntrySpacing));
            rectangleTransform.sizeDelta = new Vector2(EntryWidth, EntryHeight);
            _loaded.Add(roomEntry.gameObject);
        }
    }
}
