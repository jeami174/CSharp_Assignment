
using CommunityToolkit.Mvvm.ComponentModel;

namespace Presentation_WPF_MainApplication.ViewModels;

public partial class ContactAddViewModel : ObservableObject
{
    [ObservableProperty]
    private string _title = "Add new Contact";
}