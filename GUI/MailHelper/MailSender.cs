using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GUI.MailHelper
{
    public class MailSender
    {
        public void Send(string to, string subject, string body)
        {
            // Cấu hình email
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("luongnhattruong2004@gmail.com", "Coffee Store App"); // địa chỉ email người gửi
            mail.To.Add(to);               // địa chỉ email người nhận
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true; // Nếu bạn muốn gửi nội dung HTML, đặt là true

            // Cấu hình SMTP client
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587); // Thay đổi host và port theo dịch vụ email bạn sử dụng
            smtpClient.Credentials = new NetworkCredential("luongnhattruong2004@gmail.com", "srbg nffv wqcz spdq"); // Tên đăng nhập và mật khẩu của tài khoản email
            smtpClient.EnableSsl = true; // Bật SSL nếu máy chủ yêu cầu

            // Gửi email
            smtpClient.Send(mail);
        }
    }
}
