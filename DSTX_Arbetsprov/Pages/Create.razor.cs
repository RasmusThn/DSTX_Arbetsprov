using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using ServiceContracts;

namespace PresentationBlazor.Pages
{
    public partial class Create
    {
        [Inject]
        private ITimeReportService _timeReportService { get; set; }
        private TimeReport timeReport { get; set; } = new();
        private List<Workplace> workplaces;
        private bool validEntry { get; set; } = false;
        private bool unvalidEntry { get; set; } = false;
        private int selectedId;
        private IBrowserFile imageFile;
        private ElementReference imageInputRef;


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync("import", "/js/uploadImage.js");
            }
        }
        protected override async Task OnInitializedAsync()
        {
            timeReport.Date = DateTime.Now;
            workplaces = await _timeReportService.GetAllWorkplacesAsync();
            base.OnInitialized();
        }
        private async Task HandleCreateReportAsync()
        {
            validEntry = false;
            unvalidEntry = false;
            var response = await JSRuntime.InvokeAsync<TransferTimeReportDto>("handleImageUpload", imageInputRef);

            if (response != null && !string.IsNullOrEmpty(response.Id.ToString()))
            {
                timeReport.Id = Convert.ToInt32(response.Id);
                timeReport.WorkplaceId = response.WorkplaceId;
                timeReport.Date = DateTime.Parse(response.Date);
                timeReport.Hours = float.Parse(response.Hours);
                timeReport.Info = response.Info;

                validEntry = true;
            }
            else
            {
                unvalidEntry = true;
            }

        }
        private void SetSelectedId(ChangeEventArgs e)
        {
            selectedId = Convert.ToInt32(e.Value);

        }
    }
}
