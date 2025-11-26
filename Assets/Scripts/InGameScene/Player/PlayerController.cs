using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private PlayerView playerView1;
    [SerializeField] private PlayerView2 playerView2;
    private PlayerModel playerModel;
    private PlayerInput playerInput;
    private float input;

    private void Awake()
    {
        // 컴포넌트 추가
        playerModel = GetComponent<PlayerModel>();
        playerInput = GetComponent<PlayerInput>();

        // 각 매서드 별로 구독 설정
        playerModel.OnInit += uiManager.UIInit;
        playerModel.OncurrentHpChanged += playerView1.UpdateCurrentHpUI;
        playerModel.OnCurrentCoinChanged += playerView1.UpdateCoinUI;
        playerModel.OnCurrentCheeseChanged += playerView1.UpdateCheeseUI;
        playerModel.OnCurrentCoinChanged += playerView2.UpdateCoinUI;
        playerModel.OnCurrentCheeseChanged += playerView2.UpdateCheeseUI;
        playerModel.OnDead += uiManager.ResultUI;
        
        // 인풋 구독 설정
        playerInput.actions["Jump"].started += OnJump;
        playerInput.actions["Sliding"].started += OnSliding;
    }
    private void OnEnable()
    {
        // 활성화시 플레이어 초기화
        playerModel.InitPlayer();
    }
    private void Start()
    {
        // UI 매서드들 시작시 한 번 호출
        playerView1.UpdateCurrentHpUI(playerModel.CurrentHp, playerModel.MaxHPLevel);
        playerView1.UpdateCoinUI(playerModel.CurrentCoin);
        playerView1.UpdateCheeseUI(playerModel.CurrentCheese);
    }

    private void OnDestroy()
    {
        // 각 매서드 별로 구독 해지
        playerModel.OncurrentHpChanged -= playerView1.UpdateCurrentHpUI;
        playerModel.OnCurrentCoinChanged -= playerView1.UpdateCoinUI;
        playerModel.OnCurrentCheeseChanged -= playerView1.UpdateCheeseUI;
        playerInput.actions["Jump"].started -= OnJump;
        playerInput.actions["Sliding"].started -= OnSliding;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        input = context.ReadValue<float>();
        Debug.Log("Jump 발동");
    }

    public void OnSliding(InputAction.CallbackContext context)
    {
        Debug.Log("Sliding 발동");
    }
}
