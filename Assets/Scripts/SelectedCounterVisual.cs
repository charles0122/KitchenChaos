using System.Collections;
using System.Collections.Generic;
using 
    Engine;

public class SelectedCounterVisual : MonoBehaviour {
    [SerializeField] private ClearCounter clearCounter;
    [SerializeField] private GameObject visualGameObject;
    private void Start() {
        // Player单例中的 选中Counter改变 事件
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e) {
        if (e.selectedCounter==clearCounter) {
            Show();
        } else {
            Hide();
        }
    }

    private void Show() {
        visualGameObject.SetActive(true);
    }
    private void Hide() {
        visualGameObject.SetActive(false);
    }
}
