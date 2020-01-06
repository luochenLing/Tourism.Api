using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tourism.Model
{
    [Table("ApiConfig")]
    public class ApiConfig
    {
        /// <summary>
        /// id
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// ApiKey
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// ApiValue
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// ApiScopes
        /// </summary>
        public string scopes { get; set; }


    }
}
