using Microsoft.AspNetCore.Components;

namespace TabiTravel.UI.Shared.Component
{
    public partial class BtnAddAndCancel
    {
        [Parameter]
        public string AddLink { get; set; }

        [Parameter]
        public string ReturnLink { get; set; }        

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        #region Add
        protected void Add()
        {
            NavigationManager.NavigateTo($"{AddLink}");
        }

        #endregion        
        
        #region BackTo...
        protected void BackTo()
        {
            NavigationManager.NavigateTo($"{ReturnLink}");
        }

        #endregion
    }
}
