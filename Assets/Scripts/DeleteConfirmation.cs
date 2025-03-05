using Models;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeleteConfirmation : MonoBehaviour
{
    public GetRoomsResponseDto Room;
    public bool canEdit;
        
    public async void DeleteRoom()
    {
        if (canEdit)
        {
            await ApiClientRoomChoiceSingleton.Instance.DeleteRoom(Room.roomId);
        }
        else
        {
            await ApiClientRoomChoiceSingleton.Instance.DeleteUserRoom(UserSingleton.Instance.Name, Room.roomId);
        }

        await SceneManager.LoadSceneAsync("Scenes/RoomChoice");
    }

    public void Cancel()
    {
        Destroy(gameObject);
    }
}