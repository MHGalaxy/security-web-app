using Microsoft.AspNetCore.Mvc;
using System.Security;
using System.Xml;
using System.Xml.Schema;

namespace Cyber_Security_App.Controllers
{
    public class XmlController : Controller
    {
        [HttpPost]
        public IActionResult ParseXml(string userInput)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(userInput); // ورودی کاربر به طور مستقیم بارگذاری می‌شود

            // پردازش XML
            var schemas = new XmlSchemaSet();
            schemas.Add("", "schema.xsd");
            xmlDoc.Schemas.Add(schemas);
            xmlDoc.LoadXml(userInput);
            xmlDoc.Validate(null);

            var settings = new XmlReaderSettings
            {
                DtdProcessing = DtdProcessing.Prohibit
            };
            using (var reader = XmlReader.Create(new StringReader(userInput), settings))
            {
                xmlDoc.Load(reader);
            }

            return View();
        }


        [HttpPost]
        public IActionResult Search(string username)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load("users.xml");
            var usernameSafe = SecurityElement.Escape(username); // پاکسازی ورودی کاربر
            var xpath = $"/Users/User[Username='{usernameSafe}']";
            var node = xmlDoc.SelectSingleNode(xpath);
            // پردازش نتیجه
            return View();
        }
    }
}
