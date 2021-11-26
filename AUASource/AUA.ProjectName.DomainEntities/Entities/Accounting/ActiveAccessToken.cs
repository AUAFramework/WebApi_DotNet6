using System.ComponentModel.DataAnnotations.Schema;
using AUA.ProjectName.Common.Consts;
using AUA.ProjectName.DomainEntities.BaseEntities;

namespace AUA.ProjectName.DomainEntities.Entities.Accounting
{
    [Table("ActiveAccessToken", Schema = SchemaConsts.Accounting)]
    public class ActiveAccessToken : DomainEntity<long>
    {
        public long UserId { get; set; }

        public DateTime ExpirationDate { get; set; }

        public string RefreshToken { get; set; }

        public string AccessToken { get; set; }


    }
}
