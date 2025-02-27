using UnityEngine;

public class RoomList : MonoBehaviour
{
    public ApiClientRoomChoice apiClientRoomChoice;
    
    void Start()
    {
        apiClientRoomChoice.GetRoomsList();
    }
}
