using System;
using Models;
using UnityEngine;

public class RoomList : MonoBehaviour
{
    private const int EntrySpacing = 135;
    
    public GameObject editRoomPrefab;
    public GameObject previousButton;
    public GameObject nextButton;
    public int currentPage;
    
    public ApiClientRoomChoice apiClientRoomChoice;
    
    private GetRoomsResponseDto[] _entries;
    
    private async void Start()
    {
        _entries = await apiClientRoomChoice.GetRoomsList();
    }

    private void LoadCurrentPage()
    {
        var pageEntries = _entries[(currentPage * 5)..Mathf.Min(currentPage * 5 + 5, _entries.Length)];
        
    }
}
