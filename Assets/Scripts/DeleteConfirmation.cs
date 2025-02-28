using Models;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeleteConfirmation : MonoBehaviour
{
    public GetRoomsResponseDto Room;
        
    public async void DeleteRoom()
    {
        await ApiClientRoomChoiceSingleton.Instance.DeleteRoom(Room.roomId);
        await SceneManager.LoadSceneAsync("Scenes/RoomChoice");
    }

    public void Cancel()
    {
        Destroy(gameObject);
    }
}