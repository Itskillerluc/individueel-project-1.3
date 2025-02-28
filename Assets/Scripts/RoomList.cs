using System;
using System.Threading.Tasks;
using Models;
using UnityEngine;
using UnityEngine.Serialization;

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
        var pageEntries = _entries[(currentPage * 5)..Mathf.Min(currentPage * 5 + 5, _entries.Length)];
        
        if (pageEntries.Length < 5)
        {
            nextButton.SetActive(false);
            previousButton.SetActive(false);
        }
        else
        {
            nextButton.SetActive(true);
            previousButton.SetActive(true);
        }

        for (var index = 0; index < pageEntries.Length; index++)
        {
            var entry = pageEntries[index];
            var isOwner = entry.users.Find(user => user.user.Equals(UserSingleton.Instance.Name)).isOwner;
            var roomEntry = Instantiate(isOwner ? editRoomPrefab : viewRoomPrefab,
                new Vector3(EntryX, EntryStart + index * (EntrySpacing + EntryHeight)), Quaternion.identity, roomList);
            roomEntry.text.text = entry.name;
            roomEntry.Room = entry;
            var rectangleTransform = roomEntry.GetComponent<RectTransform>();
            rectangleTransform.anchoredPosition = new Vector3(EntryX,  EntryStart - index * (EntrySpacing));
            rectangleTransform.sizeDelta = new Vector2(EntryWidth, EntryHeight);
        }
    }
}
