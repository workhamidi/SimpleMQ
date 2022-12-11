using System.Text;
using System.Web;
using Dto.Dto.RabbitMQHttp;

namespace Utility.UrlManager
{
    public class UrlFactory
    {

        public static string Protocol { get; set; } = "http";

        public static string Host { get; set; } = "localhost";
        public static int Port { get; set; } = 15672;
        public static string BaseApiEndPoint { get; set; } = "api";

        // Query string
        public static string Page { get; set; } = null!;
        public static int PageSize { get; set; }
        public static string Name { get; set; } = null!;
        public static bool UseRegex { get; set; }


        private static string GetApiUrl(List<string> pathList)
        {

            var url = new StringBuilder();

            url.Append(Protocol);

            url.Append("://");

            url.Append(Host);

            url.Append(':');

            url.Append(Port);

            url.Append("/");

            url.Append(BaseApiEndPoint);

            url.Append("/");

            pathList.ForEach(i =>
            {
                url.Append(i);

                url.Append("/");
            });

            return url.ToString();

        }

        private static string GetApiUrlWithQueryString(
            List<string> pathList,
            int page,
            int pageSize,
            string name,
            bool useRegex)
        {

            var url = new StringBuilder();

            url.Append(
                GetApiUrl(pathList)
            );
            
            url.Append("?");


            url.Append("page");
            url.Append("=");
            url.Append(Page);
            url.Append("&");


            url.Append("page_size");
            url.Append("=");
            url.Append(PageSize);
            url.Append("&");

            url.Append("name");
            url.Append("=");
            url.Append(HttpUtility.UrlEncode(Name));
            url.Append("&");


            url.Append("use_regex");
            url.Append("=");
            url.Append(UseRegex);
            url.Append("&");

            url.Append("pagination=true");
            

            return url.ToString();

        }

        public static string GetApiUrl(
                List<string> pathList,
                StringQueryPaginationDto stringQueryPaginationDto)
        {

            if (stringQueryPaginationDto is not null)
            {
                return GetApiUrlWithQueryString(
                    pathList,
                    stringQueryPaginationDto.Page,
                    stringQueryPaginationDto.PageSize,
                    stringQueryPaginationDto.Name,
                    stringQueryPaginationDto.UseRegex);
            }

            return GetApiUrl(pathList);

        }

    }

}
