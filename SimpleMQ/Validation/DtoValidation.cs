using Dto.Dto.Bind;
using Dto.Dto.Director;
using Dto.Dto.Exchange;
using Dto.Dto.Message;
using Dto.Dto.Queue;
using Serilog;

namespace SimpleMQ.Validation
{
    public static class DtoValidation
    {
        public static bool DirectorDtoIsValid(this DirectorDto directorDto)
        {
            if (
                string.IsNullOrEmpty(directorDto.QueueName) ||
                string.IsNullOrEmpty(directorDto.ExchangeName) ||
                string.IsNullOrEmpty(directorDto.ExchangeType) ||
                string.IsNullOrEmpty(directorDto.BindRoutingKey)
            )
            {

                Log.Error("QueueName or ExchangeName or ExchangeType" +
                          " or RoutingKey is empty");
                return false;
            }

            Log.Information("The DirectorDto is valid");

            return true;

        }



        public static bool CreateQueueDtoIsValid(this CreateQueueDto queue)
        {

            if (queue?.Name is null)
            {
                Log.Error("queue name is empty");
                return false;
            }


            Log.Information("The CreateQueueDto is valid");

            return true;
        }

        public static bool DeleteQueueDtoIsValid(this RemoveQueueDto queue)
        {

            if (queue?.Name is null)
            {
                Log.Error("queue name is empty");
                return false;
            }


            Log.Information("The RemoveQueueDto is valid");

            return true;
        }

        public static bool QueueDtoIsValid(this QueueDto queue)
        {

            if (queue?.Name is null)
            {
                Log.Error("queue name is empty");
                return false;
            }

            Log.Information("The QueueDto is valid");

            return true;
        }




        public static bool PublishMessageDtoIsValid(this PublishMessageDto publishMessage)
        {

            if (publishMessage?.ExchangeName is null)
            {
                Log.Error("Exchange name is empty");
                return false;
            }

            if (publishMessage?.RoutingKey is null)
            {
                Log.Error("RoutingKey is empty");
                return false;
            }

            if (publishMessage?.Message is null)
            {
                Log.Error("Message cannot be null");
                return false;
            }
            

            Log.Information("The PublishMessageDto is valid");

            return true;
        }


        public static bool GetMessageDtoIsValid(this GetMessageDto message)
        {

            if (message?.QueueName is null)
            {
                Log.Error("QueueName name is empty");
                return false;
            }

            
            Log.Information("The GetMessageDto is valid");

            return true;
        }


        public static bool PullGetMessageDtoIsValid(this PullGetMessageDto message)
        {

            if (message?.MessageNo is null)
            {
                Log.Error("QueueName name is empty");
                return false;
            }
            
            Log.Information("The GetMessageDto is valid");

            return true;
        }





        public static bool CreateExchangeDtoIsValid(this CreateExchangeDto exchange)
        {

            if (exchange?.Name is null)
            {
                Log.Error("exchange name is empty");
                return false;
            }

            if (exchange?.Type is null)
            {
                Log.Error("exchange Type is empty");
                return false;
            }
            
            Log.Information("The CreateExchangeDto is valid");

            return true;
        }

        public static bool DeleteExchangeDtoIsValid(this DeleteExchangeDto exchange)
        {

            if (exchange?.Name is null)
            {
                Log.Error("exchange name is empty");
                return false;
            }
            
            Log.Information("The DeleteExchangeDto is valid");

            return true;
        }


        public static bool ExchangeDtoIsValid(this ExchangeDto exchange)
        {

            if (exchange?.Name is null)
            {
                Log.Error("exchange name is empty");
                return false;
            }

            Log.Information("The ExchangeDto is valid");

            return true;
        }

        



        public static bool BindExchangeToExchangeDtoIsValid(this BindExchangeToExchangeDto exchange)
        {

            if (exchange?.SourceExchangeName is null)
            {
                Log.Error("exchange Source Exchange Name is empty");
                return false;
            }

            if (exchange?.DestinationExchangeName is null)
            {
                Log.Error("exchange Destination Exchange Name is empty");
                return false;
            }

            if (exchange?.RoutingKey is null)
            {
                Log.Error("exchange RoutingKey is empty");
                return false;
            }

            Log.Information("The BindExchangeToExchangeDto is valid");

            return true;
        }



        public static bool BindQueueToExchangeDtoIsValid(this BindQueueToExchangeDto? queueToExchangeDto)
        {

            if (queueToExchangeDto?.QueueName is null)
            {
                Log.Error("the Queue Name is empty");
                return false;
            }

            if (queueToExchangeDto?.ExchangeName is null)
            {
                Log.Error("the Exchange Name is empty");
                return false;
            }

            if (queueToExchangeDto?.RoutingKey is null)
            {
                Log.Error("exchange RoutingKey is empty");
                return false;
            }

            Log.Information("The BindQueueToExchangeDto is valid");

            return true;
        }




    }
}
