using Dto.Dto;
using Dto.Dto.Message;
using Dto.Dto.Queue;
using Dto.Enums;
using RabbitMQPackageApi;
using SimpleMQ.Validation;

namespace SimpleMQ
{
    public class MessageManager
    {
        private readonly MessagePackageApi _messagePackageApi;

        public MessageManager(MessagePackageApi messagePackageApi)
        {
            _messagePackageApi = messagePackageApi;
        }

        public ResultDto<PublishMessageDto> PublishMessage(PublishMessageDto publishMessage)
        {
            if (publishMessage.PublishMessageDtoIsValid() is false)
            {
                return new ResultDto<PublishMessageDto>()
                {
                    StatusCode = ResultStatusCodeEnum.Failed
                };
            }

            return _messagePackageApi.PublishMessage(publishMessage);
        }


        public ResultDto<GetMessageDto> PushSubscribing(GetMessageDto getMessage)
        {
            if (getMessage.GetMessageDtoIsValid() is false)
            {
                return new ResultDto<GetMessageDto>()
                {
                    StatusCode = ResultStatusCodeEnum.Failed
                };
            }

            return _messagePackageApi.PushSubscribing(getMessage);
        }


        public ResultDto<List<string>> PullSubscribing(PullGetMessageDto getPullMessage)
        {

            if (getPullMessage.PullGetMessageDtoIsValid() is false)
            {
                return new ResultDto<List<string>>()
                {
                    StatusCode = ResultStatusCodeEnum.Failed
                };
            }

            return _messagePackageApi.PullSubscribing(getPullMessage);
        }


        public ResultDto<UInt32> GetQueueMessageCount(QueueDto queue)
        {

            if (queue.QueueDtoIsValid() is false)
            {
                return new ResultDto<UInt32>()
                {
                    StatusCode = ResultStatusCodeEnum.Failed
                };
            }

            return _messagePackageApi.GetQueueMessageCount(queue);
        }


    }
}
