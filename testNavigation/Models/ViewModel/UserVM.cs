using Microsoft.AspNetCore.Mvc.Rendering;

namespace testNavigation.Models.ViewModel
{
    public class UserVM
    {
        public User oUser { get; set; }
        public List<SelectListItem> oListaActivo { get; set; }
    }
}
