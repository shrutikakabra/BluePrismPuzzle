using Microsoft.Extensions.DependencyInjection;
using System;
using WordLadderPuzzle.Types.Interfaces;
using WordLadderPuzzle.BL;
using WordLadderPuzzle.Types;
using System.IO;

namespace WordLadderPuzzle
{
    class Program
    {
        private static ServiceProvider _serviceProvider;

        static void Main(string[] args)
        {
            try
            {
                RegisterServices();

                IWordLadderPuzzle wordLadderPuzzle = _serviceProvider.GetService<IWordLadderPuzzle>();
                var msg = wordLadderPuzzle.CreateWordLadder(args);
                Console.WriteLine(msg);
                DisposeServices();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                DisposeServices();
            }
        }

        #region DI Members
        private static void RegisterServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton<WordDictionary>();
            services.AddSingleton<IBaseDictionary>(x=>x.GetService<WordDictionary>());
            services.AddSingleton<IBaseGraph, CreateBaseGraph>();
            services.AddSingleton<ISearchStrategy>(x => new SearchStrategy(x.GetService<IBaseGraph>()));
            services.AddSingleton<IPuzzleSolver>(x=>new PuzzleSolver(x.GetService<ISearchStrategy>()));           
            services.AddSingleton<IWordLadderPuzzle>(x=>new BL.WordLadderPuzzle(x.GetService<WordDictionary>(),x.GetService<IPuzzleSolver>()));
            
            _serviceProvider = services.BuildServiceProvider(true);
        }

        private static void DisposeServices()
        {
            if (_serviceProvider == null)
            {
                return;
            }
            if (_serviceProvider is IDisposable)
            {
                ((IDisposable)_serviceProvider).Dispose();
            }
        }
        #endregion


    }
}
