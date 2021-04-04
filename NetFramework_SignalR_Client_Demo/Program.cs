using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetFramework_SignalR_Client_Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            //// 設定SignalR伺服器連線
            var hubConnection = new Microsoft.AspNet.SignalR.Client.HubConnection("http://localhost:8080");

            //// 這邊的 MyHub 要對應Server的Hub名稱
            var hub = hubConnection.CreateHubProxy("MyHub");

            //// 編寫Client端的函式
            hub.On("SayHello",
                new Action<string, string>((user, message) => 
                {
                    Console.WriteLine($"{user}:{message}");
                }));


            //// 開始連接SignalR伺服器
            hubConnection.Start()
                //// 連接成功後要執行的動作
                .ContinueWith(task =>
                {
                    if (!task.IsFaulted)
                    {
                        Console.WriteLine("hub connected");

                        //// 呼叫伺服器端的Send函式
                        hub.Invoke("Send", "user1", "Hello");
                    }
                });

            Console.Read();
        }
    }
}
