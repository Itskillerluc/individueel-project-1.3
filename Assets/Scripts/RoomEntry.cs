using Models;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomEntry : MonoBehaviour
{
    private const int confirmationPositionX = 105;
    private const int confirmationPositionY = 350;
    private const int confirmationWidth = 820;
    private const int confirmationHeight = 450;

    public TextMeshProUGUI text;
    public GetRoomsResponseDto Room;
    public GameObject deleteRoomConfirmation;

    public async void ViewRoom(bool canEdit)
    {
        RoomSingleton.Instance.Room = Room;
        RoomSingleton.Instance.CanEdit = canEdit;
        await SceneManager.LoadSceneAsync("EnvironmentBuilder");
    }

    public void DeleteRoom(bool canEdit)
    {
        var confirmation = Instantiate(deleteRoomConfirmation, transform.parent);
        var rectTransform = confirmation.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(confirmationPositionX, confirmationPositionY);
        rectTransform.sizeDelta = new Vector2(confirmationWidth, confirmationHeight);
        var deleteConfirmation = confirmation.GetComponent<DeleteConfirmation>();
        deleteConfirmation.Room = Room;
        deleteConfirmation.canEdit = canEdit;
    }

    public async void ShareRoom()
    {
        RoomSingleton.Instance.Room = Room;
        await SceneManager.LoadSceneAsync("RoomShare");
    }
}