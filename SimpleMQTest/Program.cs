using Dto.Dto.Queue;
using IoC;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using SimpleMQ;

namespace SimpleMQTest
{
    public class Program
    {

        public static void Main()
        {

            var host = HostBuilder();


            Console.WriteLine(
                ActivatorUtilities
                    .GetServiceOrCreateInstance<QueueManager>(host.Services)
                    .CreateQueue(new CreateQueueDto()
                    {
                        Name = "tyuk"
                    }).StatusCode
             );


            //Console.WriteLine(
            //    ActivatorUtilities
            //        .GetServiceOrCreateInstance<QueueManager>(host.Services)
            //        .RemoveQueue(new RemoveQueueDto()
            //        {
            //            Name = "first_fdgnQueue"
            //        }).StatusCode
            // );



            //var aaa = ActivatorUtilities
            //    .GetServiceOrCreateInstance<QueueManager>(host.Services);



            //Console.WriteLine(
            //    aaa.CreateQueue(new CreateQueueDto()
            //    {
            //        Name = "aaa4"
            //    }).Data
            //    );



            // client Library test

            // برای دیدن عملکرد اینا باید بریم توی  متد test 

            // QueueTest.Test();

            // ExchangeTest.Test();

            // BindTest.Test();

            // Director.Test();







            /* api test */

            // queue

            //var queueList =  QueueApi.GetAllQueuesList();

            //var queue = QueueApi.GetQueue("s5");

            // exchange

            //var exchangeList = ExchangeApi.GetAllExchangeList();


            //var exchange = ExchangeApi.GetExchange("exchange_first_Queue");
            //Console.WriteLine(exchange.Data.name);


            ////// message

            //var message = new MessagePackageApi();


            //var messageDto = new PublishMessageDto()
            //{
            //    ExchangeName = "exchange_first_Queue",
            //    RoutingKey = "routingKey1",
            //    Message = "hello",
            //    PropertiesHeaders = new Dictionary<string, object>()
            //    {
            //        {"asd" , "new object()"}
            //    }
            //};

            //Console.WriteLine(
            //    message.PublishMessage(messageDto)
            //);




            ////// PushSubscribing

            //var getMesPush = new MessagePackageApi();

            //getMesPush.PushSubscribing(new GetMessageDto()
            //{
            //    QueueName = "first_Queue"
            //});




            //PullSubscribing

            //var getMesPull = new MessageManager();

            //var messages = getMesPull.PullSubscribing(new PullGetMessageDto()
            //{
            //    QueueName = "first_Queue",
            //    MessageNo = 2
            //});


            //if (messages.StatusCode == ResultStatusCodeEnum.Success)
            //    foreach (var message in messages.Data)
            //    {
            //        Console.WriteLine(message);
            //    }




            // message Count


            //var mesCount = ActivatorUtilities
            //    .GetServiceOrCreateInstance<MessagePackageApi>(host.Services);

            //Console.WriteLine(
            //    mesCount.GetQueueMessageCount(new QueueDto()
            //    {
            //        Name = "first_Queue"
            //    }).Data
            //);







        }




        public static IHost HostBuilder()
        {

            var configuration = new ConfigurationBuilder()
                // POINT : reloadOnChange: true
                // در زمان اجرا اگر عوض شد تغییراتش اعامل شه 
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();


            var host = Host.CreateDefaultBuilder()


                .ConfigureServices((context, services) =>
                {
                    services.DependencyInjectionHandler();
                })
                .UseSerilog((ctx, lc) => lc
                    // .Enrich.FromLogContext
                    .ReadFrom.Configuration(configuration)
                )
                .Build();







            return host;
        }

    }
}