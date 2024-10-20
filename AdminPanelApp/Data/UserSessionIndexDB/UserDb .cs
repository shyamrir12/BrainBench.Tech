using BlazorDB;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminPanelApp.Data.UserSessionIndexDB
{
    public class UserDb : IndexedDBRepository<Customer>
    {
        public UserDb(IBlazorDbFactory dbFactory)
        : base("BrainBanchDB", "Id", true, dbFactory)
        {
        }
    }
    [Table("Customer")]
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
    }
}
