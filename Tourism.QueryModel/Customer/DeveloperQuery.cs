namespace Tourism.QueryModel
{
    /// <summary>
    /// 开发者查询条件
    /// </summary>
    public class DeveloperQuery
    {
        /// <summary>
        /// 账号名
        /// </summary>
        public string ClientName { get; set; }

        /// <summary>
        /// 账号密码
        /// </summary>
        public string ClientSecret { get; set; }
    }
}
