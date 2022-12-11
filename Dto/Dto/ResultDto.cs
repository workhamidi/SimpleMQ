using Dto.Enums;

namespace Dto.Dto
{
    public class ResultDto<T>
    {
        public T Data { get; set; } = default!;
        public string Description { get; set; } = String.Empty;
        public ResultStatusCodeEnum StatusCode { get; set; } = ResultStatusCodeEnum.Success;
    }
}
