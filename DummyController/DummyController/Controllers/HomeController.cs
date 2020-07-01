using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace DummyController.Controllers
{
    public class HomeController : Controller
    {
        //建立一般方法ShowAry()-計算陣列總合
        public string ShowAry()
        {
            int[] scores = { 78, 88, 96, 100, 60, 74 };

            string show = "";
            int sum = 0;
            foreach (var s in scores)
            {
                show += s + ", ";
                sum += s;
            }

            show += "<br>";
            show += "總計:" + sum;

            return show;
        }

        // 建立一般方法ShowImages()-傳回顯示images資料夾裡1.jpg~8.jpg的HTML字串
        [HttpGet]
        public ContentResult ShowImages()
        {
            string show = "";

            for (int i = 1; i <= 8; i++)
            {
                show += "<img src='../images/" + i + ".jpg' width='150' />";
            }

            return new ContentResult
            {
                ContentType = "text/html",
                StatusCode = (int)HttpStatusCode.OK,
                Content = show
            };
        }

        // 建立一般方法ShowImageIndex()-依index參數取得對應圖示與說明
        [HttpGet]
        public ContentResult ShowImageIndex(int index)
        {
            string show = "";

            string[] name = { "櫻桃鴨", "鴨油高麗菜", "鴨油麻婆豆腐", "櫻桃鴨握壽司", "片皮鴨捲三星蔥", "三杯鴨", "櫻桃鴨片肉", "慢火白菜燉鴨湯" };

            show = string.Format("<p align='center'><img src='../images/{0}.jpg' /><br>{1}</p>", index, name[index - 1]);

            return new ContentResult
            {
                ContentType = "text/html",
                StatusCode = (int)HttpStatusCode.OK,
                Content = show
            };
        }
    }
}
