using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private UIManager uiManager;
    [SerializeField] private PlayerView playerView1;
    [SerializeField] private PlayerView2 playerView2;
    [SerializeField] private float jumpForce = 5f;

    private PlayerModel playerModel;
    private PlayerInput playerInput;
    private Rigidbody2D rb;
    private float input;


    private void Awake()
    {
        // 컴포넌트 추가
        playerModel = GetComponent<PlayerModel>();
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();

        // 각 매서드 별로 구독 설정
        playerModel.OnInit += uiManager.UIInit;
        playerModel.OncurrentHpChanged += playerView1.UpdateCurrentHpUI;
        playerModel.OnCurrentCoinChanged += playerView1.UpdateCoinUI;
        playerModel.OnCurrentCheeseChanged += playerView1.UpdateCheeseUI;

        playerModel.OnCurrentCoinChanged += playerView2.UpdateCoinUI;
        playerModel.OnCurrentCheeseChanged += playerView2.UpdateCheeseUI;
        playerModel.OnEnd += uiManager.ResultUI;
        
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
        playerView1.UpdateCurrentHpUI(playerModel.CurrentHp, GameManager.Instance.tempHpLevel);
        playerView1.UpdateCoinUI(playerModel.CurrentCoin);
        playerView1.UpdateCheeseUI(playerModel.CurrentCheese);
    }

    // 물리 충돌 발생하면 실행할 코드들
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 코인일 경우 코인 저장하는 매서드 발동
        if (collision.CompareTag("Coin")) { playerModel.GetCoin(); }
        // 치즈일 경우 점수 저장하는 매서드 발동
        else if (collision.CompareTag("Cheese")) { playerModel.GetCheese(); }
        // 장애물일 경우 체력 1씩 감소
        else if (collision.CompareTag("Obstacle")) { playerModel.ChangeHp(-1); }
        // 낙사 지점일 경우 즉사
        else if (collision.CompareTag("DeathPoint")) { playerModel.ChangeHp(-10); }
        // 일부러 제외하는 충돌
        else if (collision.CompareTag("Exception")) return;
        // 위에 해당 사항 없을 경우 로그 출력
        else { Debug.Log($"예상치 못한 충돌 발생 : {collision.name}-{collision.tag}"); }
    }

    private void OnDestroy()
    {
        // 각 매서드 별로 구독 해지
        playerModel.OncurrentHpChanged -= playerView1.UpdateCurrentHpUI;
        playerModel.OnCurrentCoinChanged -= playerView1.UpdateCoinUI;
        playerModel.OnCurrentCheeseChanged -= playerView1.UpdateCheeseUI;

        playerModel.OnCurrentCoinChanged -= playerView2.UpdateCoinUI;
        playerModel.OnCurrentCheeseChanged -= playerView2.UpdateCheeseUI;
        playerModel.OnEnd -= uiManager.ResultUI;

        playerInput.actions["Jump"].started -= OnJump;
        playerInput.actions["Sliding"].started -= OnSliding;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        Debug.Log("Jump 발동");
    }

    public void OnSliding(InputAction.CallbackContext context)
    {
        Debug.Log("Sliding 발동");
    }
}
