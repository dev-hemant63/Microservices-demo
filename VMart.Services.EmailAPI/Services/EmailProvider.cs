using System.Net;
using System.Net.Mail;
using VMart.Services.EmailAPI.Models.DTOs;
using VMart.Services.EmailAPI.Services.IService;

namespace VMart.Services.EmailAPI.Services
{
    public class EmailProvider : IEmailProvider
    {
        public async Task<ResponseDto> send(SendDto sendDto)
        {
            var res = new ResponseDto
            {
                IsSuccess = true,
                Message = "Email send successfully!"
            };
            try
            {
                MailMessage massage = new MailMessage();
                massage.From = new MailAddress("jlnp.mmb222503@gmail.com");
                massage.Subject = sendDto.Subject;
                foreach (string recipient in sendDto.to.Split(","))
                {
                    massage.To.Add(new MailAddress(recipient));
                }
                massage.Body = sendDto.Body;
                massage.IsBodyHtml = false;
                var smtpclient = new SmtpClient("smtp.gmail.com")
                {
                    Port = Convert.ToInt32(587),
                    Credentials = new NetworkCredential("jlnp.mmb222503@gmail.com", "suebgqbqjhdkxnsp"),
                    EnableSsl = true
                };
                smtpclient.Send(massage);
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
            }
            return res;
        }
    }
}
