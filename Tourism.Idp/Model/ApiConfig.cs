namespace Tourism.Idp.Model
{
    public class ApiConfig
    {
        /// <summary>
        /// API的key，这要和被管理的API的配置Key一致包括大小写
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// API的value，推荐写一个API的真实名称
        /// </summary>
        public string ApiValue { get; set; }
    }
}
