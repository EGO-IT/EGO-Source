using System.Net.Mail;
using System.Text;
using System.Web.Helpers;
using EGO.Model;

namespace EGO.BusinessLogic
{
    public class Email
    {
        public void SendEmail(string message, RegisterViewModel model, string callbackUrl)
        {
            var boddy = new StringBuilder();
                var boddyConf = new StringBuilder();

                boddy.Append(message);
                string confirmMessage = "Hi! " + model.Email + "<br/>"
                      + "Click the link to confirm your account " +
                      callbackUrl;
                boddyConf.Append(confirmMessage);

                string bodyForConf = boddyConf.ToString();

                string bodyFor = boddy.ToString();
                string subjectFor = "Registration";
                string subjectForConf = "Confirm Email";

                string toFor = model.Email;
                var mail = new MailAddress("21217040@dut4life.ac.za", "EGO-Administrators");
                WebMail.SmtpServer = "pod51014.outlook.com";
                WebMail.SmtpPort = 587;
                WebMail.UserName = "21217040@dut4life.ac.za";
                WebMail.Password = "Dut921208";
                WebMail.From = mail.ToString();
                WebMail.EnableSsl = true;

                try { WebMail.Send(toFor, subjectFor, bodyFor); }
                catch
                {
                    // ignored
                }
                try { WebMail.Send(toFor, subjectForConf, bodyForConf); }
                catch
                {
                    //null
                }
        }
    }
}
