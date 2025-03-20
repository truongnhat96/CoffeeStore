using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.MailHelper
{
    public static class HtmlHelper
    {
        public static string Content(string username, string password)
        {
            return $@"<!DOCTYPE html>
<html lang=""vi"">
<head>
  <meta charset=""UTF-8"">
  <title>Cấp Lại Mật Khẩu</title>
</head>
<body style=""margin: 0; padding: 0; background-color: #f4f4f4;"">
  <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"">
    <tr>
      <td align=""center"" style=""padding: 20px 0;"">
        <!-- Main container -->
        <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""600"" style=""border: 1px solid #cccccc; border-radius: 5px; background-color: #ffffff;"">
          <!-- Header -->
          <tr>
            <td align=""center"" style=""padding: 40px 30px 20px 30px; background-color: #007BFF; border-radius: 5px 5px 0 0; color: #ffffff;"">
              <h1 style=""margin: 0; font-family: Arial, sans-serif;"">Yêu Cầu Cấp Lại Mật Khẩu</h1>
            </td>
          </tr>
          <!-- Content -->
          <tr>
            <td style=""padding: 30px; font-family: Arial, sans-serif; color: #333333;"">
              <p>Chào {username},</p>
              <p>Chúng tôi đã nhận được yêu cầu cấp lại mật khẩu cho tài khoản của bạn. Vui lòng đăng nhập lại vào tài khoản bằng mật khẩu bên dưới:</p>
              <div style=""font-size: 18px; color: #007bff; background-color: #f0f8ff; padding: 10px 20px; border-radius: 5px; display: inline-block; margin-bottom: 20px; border: 1px solid #007bff; font-weight: bold;"">{password}</div>
              <p>Đây chỉ là mật khẩu tạm thời, hãy thay đổi bằng mật khẩu mới của bạn sau khi đăng nhập lại nhằm bảo vệ tài khoản.</p>
              <p>Trân trọng,</p>
              <p><strong>Đội ngũ Hỗ trợ</strong></p>
            </td>
          </tr>
          <!-- Footer -->
          <tr>
            <td align=""center"" style=""padding: 20px; background-color: #f4f4f4; border-radius: 0 0 5px 5px; font-family: Arial, sans-serif; font-size: 12px; color: #888888;"">
              <p>Bạn nhận được email này vì đã yêu cầu cấp lại mật khẩu cho tài khoản của mình.</p>
            </td>
          </tr>
        </table>
      </td>
    </tr>
  </table>
</body>
</html>
";
        }

        public static string ContentConfirm(string pass)
        {
            return $@"<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Your Password</title>
    <style>
        body {{
            font-family: Arial, sans-serif;
            background-color: #f4f4f9;
            margin: 0;
            padding: 0;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
        }}

        .container {{
            background-color: #ffffff;
            width: 100%;
            max-width: 400px;
            padding: 30px;
            border-radius: 10px;
            box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
            text-align: center;
        }}

        .container h1 {{
            font-size: 28px;
            color: #333333;
            margin-bottom: 20px;
        }}

        .container p {{
            font-size: 16px;
            color: #666666;
            margin-bottom: 20px;
        }}

        .password-box {{
            font-size: 18px;
            color: #007bff;
            background-color: #f0f8ff;
            padding: 10px 20px;
            border-radius: 5px;
            display: inline-block;
            margin-bottom: 20px;
            border: 1px solid #007bff;
        }}

        .footer {{
            font-size: 12px;
            color: #999999;
            margin-top: 20px;
        }}
    </style>
</head>
<body>
    <div class=""container"">
        <h1>Không Cung Cấp Mã Xác Nhận Này Cho Người Khác</h1>
        <p>Vui lòng sử dụng mã OTP dưới đây để xác thực thông tin mật khẩu mới của bạn</p>
        <div class=""password-box"">{pass}</div>
              <p>Trân trọng,</p>
              <p><strong>Đội ngũ Hỗ trợ</strong></p>
        <div class=""footer"">
            &copy; 2025 Secure Service. All rights reserved.
        </div>
    </div>
</body>
</html>
";
        }
    }
}
