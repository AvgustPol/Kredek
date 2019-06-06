using Kredek.Data.ViewModels;
using System.Threading.Tasks;

namespace Kredek.Logic.Factories
{
    public interface IPageElementViewModelsFactory
    {
        Task<PageElementViewModel> CreatePageElementViewModel();
    }
}