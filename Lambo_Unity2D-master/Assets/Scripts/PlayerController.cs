using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public LayerMask playerMask;
    public GameObject Sword,pos_sword;
    public bool canMoveInAir = true;
    // public GameObject gameOverScreen;
    // public GameManager theGameManager;


    float fireRate = 0;
    float nextfire = 0;
    // để đặt tốc độ khi Người chơi di chuyển
    [SerializeField] private float speed;
    // cho các thành phần komponen của Rigidbody2D
    private Rigidbody2D rigidBody;
    // Để lưu trữ giá trị điều kiện
    //Người chơi khi di chuyển sang phải hoặc sang trái
    private float moveInput;
    // Điều kiện đúng khi Người chơi quay mặt phải
    private bool facingRight;

    // chỉ định một giá trị cho mức độ cao mà Người chơi có thể nhảy
    [SerializeField] private float jumpForce;
    // cho biết true nếu người chơi chạm vào bậc thang hoặc mặt đất
    [SerializeField] private bool isGrounded;
    // đảm bảo rằng chân của Người chơi đang hạ xuống,
    //lòng bàn chân
    [SerializeField] private Transform feetPos;
    // Điều này được sử dụng để đặt bán kính bàn chân của Người chơi của bạn lớn như thế nào
    //"Nhiều hơn hoặc ít hơn :)"
    [SerializeField] private float circleRadius;
    // Điều này được sử dụng để bảo mật đối tượng
    //ai hành động / biến nó thành một mặt đất
    [SerializeField] private LayerMask whatIsGround;

    //chúng tôi gọi là biến này
    //để chạy hoạt ảnh nhàn rỗi, chay., va nhay?
    private Animator anim;

    private void Start()
    {
        //khởi tạo thành phần Rigidbody2D có trong Trình phát
        rigidBody = GetComponent<Rigidbody2D>();
        //chúng tôi đặt ở đầu TRUE vì người chơi phải đối mặt
        facingRight = true;
        //Khởi tạo thành phần Animator hiện có trên Trình phát
        anim = GetComponent<Animator>();
    }


    private void Update()
    {
        //Bằng cách gọi lớp Physics2D và hàm OverlapCircle
        //có 3 tham số này cho biết rằng
        //isGrounded sẽ đúng nếu cả ba tham số đều được đáp ứng
        isGrounded = Physics2D.OverlapCircle(feetPos.position, circleRadius, whatIsGround);

        //Chức năng cho người chơi khi nhảy
        CharacterJump();

        if(fireRate == 0)
        {

          if(Input.GetKeyDown(KeyCode.Space))
          {
            Shooting();
          }
          else
          {
            if(Input.GetKeyDown(KeyCode.Space) && Time.time > nextfire)
            {
              nextfire = Time.time + nextfire;
              Shooting();
            }
          }
        }
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
		if (screenPosition.y > Screen.height || screenPosition.y < 0){
            died();
        }

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
      if(coll.gameObject.tag == "Batas_Mati")
      {
        died();
      }
    }

    void Die(){
        Debug.Log("Game Over");
		SceneManager.LoadScene("Menu");
    }

    void died()
    {
      SceneManager.LoadScene("GameOver");
    }


    private void FixedUpdate()
    {
        // Các hàm quản lý đầu vào
        //khi người chơi di chuyển sang phải hoặc trái
        CharacterMovement();
        // Chức năng điều tiết
        //Chuyển đổi hoạt ảnh trình phát
        //khi nhàn rỗi, chạy hoặc nhảy
        CharacterAnimation();

    }
    public float Scale_karak;
     void Shooting(){
           if (Scale_karak == 1f){
            GetComponent<Rigidbody2D>().velocity = new Vector2(8f, GetComponent<Rigidbody2D>().velocity.y);
        }
        else{
            GetComponent<Rigidbody2D>().velocity = new Vector2(-8f, GetComponent<Rigidbody2D>().velocity.y);
        }
        Instantiate(Sword, pos_sword.transform.position, pos_sword.transform.rotation);
    }
    // void OnMousDown (){
	// 	Instantiate(Sword, pos_sword.transform.position, pos_sword.transform.rotation);

	// }

    private void CharacterMovement()
    {
        //Input.GetAxis adalah sebuah fungsi
        //đã được cung cấp bởi Unity
        //xem bàn phím nhập
        //mở menu chỉnh sửa và chọn Cài đặt dự án và chọn Đầu vào
        moveInput = Input.GetAxis("Horizontal");

        if (moveInput > 0 && facingRight == false)
        {

            Flip();
        }
        else if (moveInput < 0 && facingRight == true)
        {
            //Các chức năng hữu ích cho Player
            //có thể quay mặt sang phải hoặc sang trái
            Flip();
        }
        // giá trị trên trục X sẽ tăng theo tốc độ * moveInput
        rigidBody.velocity = new Vector2(speed * moveInput, rigidBody.velocity.y);
    }

    void CharacterJump()
    {

        if (isGrounded == true &&  Input.GetKeyDown(KeyCode.UpArrow))
        {
            //Cách gọi hoạt ảnh bằng
            //tham số của loại Trigger
            anim.SetTrigger("isJump");
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
        }
    }

    void CharacterAnimation()
    {
        if (moveInput != 0 && isGrounded == true)
        {
            //cách gọi hoạt ảnh bằng
            //tham số của loại BOOL
            anim.SetBool("isRun", true);

        }
        else if (moveInput == 0 && isGrounded == true)
        {
            anim.SetBool("isRun", false);
        }
    }

    private void Flip()
    {
        //Giá trị của đối mặt không giống như đối mặt với phải đối mặt
        facingRight = !facingRight;
        //tạo một biến kiểu Vector3
        //yang isinya = transform.localScale
        //(Scling pada sumbu x=1, y=1,z=1)
        Vector3 scaler = transform.localScale;
        //sau đó nhân trên trục x
        //với trừ lên đến trục x
        //sau này sẽ có giá trị trừ
        scaler.x *= -1;
        //và cuối cùng trục x trên Trình phát được đưa ra
        //giá trị trừ để khi Người chơi phải đối mặt
        //ở bên trái của trục x trên Trình phát sẽ là -1
        transform.localScale = scaler;

        //NOTE : Trục x là trục x hiện có
        //trên Quy mô hiện có trong thành phần Chuyển đổi
    }
}
