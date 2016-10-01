using System;

namespace NgTodoList.Api.Models.Account
{
    public class DetailUserViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
    }
}