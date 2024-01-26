using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Input;

using Microsoft.EntityFrameworkCore;
using MAUI.DataAccess;
using MAUI.DTOs;
using MAUI.Utilidades;
using MAUI.Models;
using System.Collections.ObjectModel;
using MAUI.Views;

namespace MAUI.ViewsModels
{
    public partial class MainViewModel: ObservableObject
    {
        private readonly VideoGMDbContext _dbContext;
        [ObservableProperty]
        private ObservableCollection<VideogameDTO> listaVideoGames = new ObservableCollection<VideogameDTO>();

        public MainViewModel(VideoGMDbContext context)
        {
            _dbContext = context;

            MainThread.BeginInvokeOnMainThread(new Action(async () => await Obtener()));

            WeakReferenceMessenger.Default.Register<VideoGMensajeria>(this, (r, m) =>
            {
                VideoGMensajeRecibido(m.Value);
            });
        }

        public async Task Obtener()
        {
            var lista = await _dbContext.Videogames.ToListAsync();
            if (lista.Any())
            {
                foreach (var item in lista)
                {
                    ListaVideoGames.Add(new VideogameDTO
                    {
                        IdVideoGame= item.IdVideoGame,
                        Name= item.Name,
                        Description= item.Description,
                        Price= item.Price,
                        Company= item.Company,
                        Gender= item.Gender,
                    });
                }
            }
        }

        private void VideoGMensajeRecibido(VideoGMensaje videoGameMensaje)
        {
            var videoGameDto = videoGameMensaje.VideogameDto;

            if (videoGameMensaje.EsCrear)
            {
                ListaVideoGames.Add(videoGameDto);
            }
            else
            {
                var encontrado = ListaVideoGames
                    .First(e => e.IdVideoGame == videoGameDto.IdVideoGame);
                encontrado.Name = videoGameDto.Name;
                encontrado.Description = videoGameDto.Description;
                encontrado.Price = videoGameDto.Price;
                encontrado.Company = videoGameDto.Company;
                encontrado.Gender = videoGameDto.Gender;

            }

        }

        [RelayCommand]
        private async Task Crear()
        {
            var uri = $"{nameof(VideoGamePage)}?id=0";
            await Shell.Current.GoToAsync(uri);
        }

        [RelayCommand]
        private async Task Editar(VideogameDTO videoGameDto)
        {
            var uri = $"{nameof(VideoGamePage)}?id={videoGameDto.IdVideoGame}";
            await Shell.Current.GoToAsync(uri);
        }

        [RelayCommand]
        private async Task Eliminar(VideogameDTO videoGameDto)
        {
            bool answer = await Shell.Current.DisplayAlert("Mensaje", "Desea eliminar el Juego?", "Si", "No");

            if (answer)
            {
                var encontrado = await _dbContext.Videogames
                    .FirstAsync(e => e.IdVideoGame == videoGameDto.IdVideoGame);

                _dbContext.Videogames.Remove(encontrado);
                await _dbContext.SaveChangesAsync();
                ListaVideoGames.Remove(videoGameDto);

            }

        }
    }
}
