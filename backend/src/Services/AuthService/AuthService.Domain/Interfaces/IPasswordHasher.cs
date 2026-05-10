namespace AuthService.Domain.Interfaces;

public interface IPasswordHasher
{
    // Hàm này sẽ nhận vào mật khẩu gốc (plaintext) và trả về Hash
    string Hash(string password);
    
    // Hàm này dùng khi Login để kiểm tra mật khẩu
    bool Verify(string password, string hash);
}