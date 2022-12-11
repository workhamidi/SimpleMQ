using System.Text;
using Dto.Dto;
using Dto.Dto.Message;
using Dto.Dto.Queue;
using Dto.Enums;
using RabbiMQHttpClientApi.Interface;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Serilog;
using Utility.ConfigurationManager;
using IBasicProperties = RabbitMQ.Client.IBasicProperties;


namespace RabbitMQPackageApi
{
    public class MessagePackageApi : Communication
    {
        
        private readonly IRabbitMQHttpApi _rabbitMqHttpApi;

        public MessagePackageApi(
            IRabbitMQHttpApi rabbitMQHttpApi,
            SimpleMQConfigurationManager configuration)
            : base(configuration)
        {
            _rabbitMqHttpApi = rabbitMQHttpApi;
        }

        public ResultDto<PublishMessageDto> PublishMessage(PublishMessageDto publishMessage)
        {
            

            var channel = base.CreateCommunication();

            if (channel.StatusCode == ResultStatusCodeEnum.Success)
            {

                var body = Encoding.UTF8.GetBytes(publishMessage.Message);


                IBasicProperties props = channel.Data.CreateBasicProperties();


                props.Headers = publishMessage.PropertiesHeaders;

                channel.Data.BasicPublish(
                    exchange: publishMessage.ExchangeName,
                    routingKey: publishMessage.RoutingKey,
                    basicProperties: props,
                    body: body);


                Log.Information("Publish the message was successfully");

                base.DisposingCommunication();

                return new ResultDto<PublishMessageDto>()
                {
                    StatusCode = ResultStatusCodeEnum.Success
                };

            }

            Log.Error("can not connect to broker");

            return new ResultDto<PublishMessageDto>()
            {
                StatusCode = ResultStatusCodeEnum.Failed
            };

        }


        public ResultDto<GetMessageDto> PushSubscribing(GetMessageDto getMessage)
        {

            var channel = base.CreateCommunication();



            if (channel.StatusCode == ResultStatusCodeEnum.Success)
            {

                var consumer = new EventingBasicConsumer(channel.Data);


                consumer.Received += (model, basicDeliverEventArgs) =>
                {
                    var body = basicDeliverEventArgs.Body.ToArray();

                    var message = Encoding.UTF8.GetString(body);

                    Console.WriteLine(message);

                    //Console.WriteLine("resive messeage -->  channelId : {0} | publisherId : {1} " +
                    //                  "| messagId : {2} | content : {3}",
                    //    basicDeliverEventArgs.BasicProperties.Headers["channelId"],
                    //    basicDeliverEventArgs.BasicProperties.Headers["publisherId"],
                    //    basicDeliverEventArgs.BasicProperties.MessageId, );
                };


                channel.Data.BasicConsume(
                    queue: getMessage.QueueName,
                    autoAck: getMessage.AutoAck,
                    consumer: consumer);

                Log.Information("getting message was successfully");

                base.DisposingCommunication();

                Console.ReadLine();


                return new ResultDto<GetMessageDto>()
                {
                    StatusCode = ResultStatusCodeEnum.Success
                };

            }

            Log.Error("can not connect to broker");

            return new ResultDto<GetMessageDto>()
            {
                StatusCode = ResultStatusCodeEnum.Failed
            };

        }


        public ResultDto<List<string>> PullSubscribing(PullGetMessageDto getPullMessage)
        {


            var channel = base.CreateCommunication();


            if (channel.StatusCode == ResultStatusCodeEnum.Success)
            {
                var mesList = new List<string>();

                BasicGetResult result = channel.Data.BasicGet(
                   getPullMessage.QueueName,
                   getPullMessage.AutoAck
               );

                foreach (var _ in Enumerable.Range(0,
                             getPullMessage.MessageNo))
                {

                    if (result is null)
                    {
                        Log.Information("the message in the queue not exist");
                    }
                    else
                    {
                        ReadOnlyMemory<byte> body = result.Body;


                        mesList.Add(Encoding.UTF8.GetString(body.ToArray()));

                        channel.Data.BasicAck(result.DeliveryTag, false);

                    }
                }


                Log.Information("getting message was successfully");

                base.DisposingCommunication();

                return new ResultDto<List<string>>()
                {
                    StatusCode = ResultStatusCodeEnum.Success,
                    Data = mesList
                };

            }

            Log.Error("can not connect to broker");

            return new ResultDto<List<string>>()
            {
                StatusCode = ResultStatusCodeEnum.Failed
            };

        }


        public ResultDto<UInt32> GetQueueMessageCount(QueueDto queue)
        {




            var channel = base.CreateCommunication();


            if (channel.StatusCode == ResultStatusCodeEnum.Success)
            {

                var mesCount = channel.Data.MessageCount(queue.Name);

                Log.Information("getting message count was successfully");

                base.DisposingCommunication();

                return new ResultDto<uint>()
                {
                    StatusCode = ResultStatusCodeEnum.Success,
                    Data = mesCount
                };

            }

            Log.Error("can not connect to broker");

            return new ResultDto<uint>()
            {
                StatusCode = ResultStatusCodeEnum.Failed
            };

        }


    }
}
