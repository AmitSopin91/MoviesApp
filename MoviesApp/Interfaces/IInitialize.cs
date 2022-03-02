using System;
using Prism.Navigation;

namespace MoviesApp.Interfaces
{
    public interface IInitialize
    {
        void Initialize(INavigationParameters parameters);
    }
}
