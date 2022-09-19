using Microsoft.AspNetCore.Components;

namespace TabiTravel.UI.Shared.Component
{
    public partial class BtnSaveAndCancel
    {
        [Parameter]
        public string ReturnLink { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        #region BackTo...
        protected void BackTo()
        {
            NavigationManager.NavigateTo($"{ReturnLink}");
        }

        #endregion
    }
}
