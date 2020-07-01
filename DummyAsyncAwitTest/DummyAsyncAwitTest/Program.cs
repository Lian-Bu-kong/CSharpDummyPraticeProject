using System;
using System.Net;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DummyAsyncAwitTest
{
    class Program
    {
        static void Main(string[] args)
        {

            //DemoAsync();
            //DemoRx();
            //Log("End");
            //while (true) ;

        }

    
        static async void DemoAsync()
        {
            /**
             遇到 awiat 目前的 thread就彈去外面，去做其他事情，這件事情，交給其他執行續等候完成
             
             * */

            var x1 = Excute1();
            var x2 = Excute2();

            string x = string.Empty;
            x = await x1;
            Log("X1 網頁內容總共為 {0} 個字元。", x.Length);

            x = await x2;
            Log("X2 網頁內容總共為 {0} 個字元。", x.Length);
            Log("Async Complelted");
        }

        static async Task<string> Excute1()
        {
            Log("Async-1 正要起始非同步工作 MyDownloadPageAsync()。");
            return await MyDownloadPageAsync("https://www.huanlintalk.com");
        }

        static async Task<string> Excute2()
        {
            Log("Async-2 正要起始非同步工作 MyDownloadPageAsync()。");
            return await MyDownloadPageAsync("https://columns.chicken-house.net/2019/07/06/pipeline-practices/#pr5-phoenixdemophoenixtaskrunner");
        }

        static void DemoRx()
        {

            //Observable.Merge(RxExcute1(), RxExcute2())
            //    .ObserveOn(Scheduler.CurrentThread)
            //    .Subscribe(x => Log("網頁內容總共為 {0} 個字元。", x.Length));

            Observable.Merge(RxExcute1(), RxExcute2())
             .ObserveOn(Scheduler.CurrentThread)    //觀察回流的 Thread
             .Subscribe(x => Log("網頁內容總共為 {0} 個字元。", x.Length));

            Log("Rx Complelted");
        }

        static IObservable<string> RxExcute1()
        {
            Log("Rx-1 正要起始非同步工作 MyDownloadPageAsync()。");
            return Observable.FromAsync(() => MyDownloadPageAsync("https://www.huanlintalk.com")); ;
        }

        static IObservable<string> RxExcute2()
        {
            Log("Rx-2 正要起始非同步工作 MyDownloadPageAsync()。");
            return Observable.FromAsync(() => MyDownloadPageAsync("https://columns.chicken-house.net/2019/07/06/pipeline-practices/#pr5-phoenixdemophoenixtaskrunner")); ;
        }

        static async Task<string> MyDownloadPageAsync(string url)
        {
            Log("2 正要呼叫 WebClient.DownloadStringTaskAsync()。");

            using (var webClient = new WebClient())
            {
                var task = webClient.DownloadStringTaskAsync(url);

                Log("3 已起始非同步工作 DownloadStringTaskAsync()。");

                string content = await task;

                Log("5 已經取得 DownloadStringTaskAsync() 的結果。");

                return content;
            }
        }


        static void Log(string message, params object[] args)
        {
            Console.WriteLine($"Thread Id({Thread.CurrentThread.ManagedThreadId}), {string.Format(message, args)}"); ;
        }
    }
}
