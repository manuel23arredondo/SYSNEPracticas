using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Input;

using Microsoft.EntityFrameworkCore;
using MAUI.DataAccess;
using MAUI.DTOs;
using MAUI.Utilidades;
using MAUI.Models;

namespace MAUI.ViewsModels
{
    public partial class VideoGameVModel : ObservableObject, IQueryAttributable
    {
        private readonly VideoGMDbContext _dbContext;

        [ObservableProperty]
        private VideogameDTO videogameDto = new VideogameDTO();

        [ObservableProperty]
        private string tituloPagina;

        private int IdVideoGame;

        [ObservableProperty]
        private bool loadingesVisible= false;

        public VideoGameVModel(VideoGMDbContext context)
        {
            _dbContext= context;
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            //var id = int.Parse(query["id"].ToString());
            var id = int.Parse(query["id"].ToString());
            IdVideoGame = id;

            if (IdVideoGame == 0)
            {
                TituloPagina = "Nuevo Video Game";
            }
            else
            {
                TituloPagina = "Editar Game";
                LoadingesVisible = true;
                await Task.Run(async() =>
                {
                    var encontrado = await _dbContext.Videogames.FirstAsync(e => e.IdVideoGame == IdVideoGame);
                    VideogameDto.IdVideoGame = encontrado.IdVideoGame;
                    VideogameDto.Name = encontrado.Name;
                    VideogameDto.Description = encontrado.Description;
                    VideogameDto.Price = encontrado.Price;
                    VideogameDto.Company= encontrado.Company;
                    VideogameDto.Gender = encontrado.Gender;

                    MainThread.BeginInvokeOnMainThread(()=> { LoadingesVisible= false; });
                });
            }
        }
        [RelayCommand]
        private async Task Guardar()
        {
            LoadingesVisible = true;
            VideoGMensaje mensaje = new VideoGMensaje();

            await Task.Run(async () =>
            {
                if (IdVideoGame == 0)
                {
                    var tbVideoGame = new Videogame
                    {
                        Name = VideogameDto.Name,
                        Description = VideogameDto.Description,
                        Price = VideogameDto.Price,
                        Company = VideogameDto.Company,
                        Gender = VideogameDto.Gender,
                    };

                    _dbContext.Videogames.Add(tbVideoGame);
                    await _dbContext.SaveChangesAsync();

                    VideogameDto.IdVideoGame = tbVideoGame.IdVideoGame;
                    mensaje = new VideoGMensaje()
                    {
                        EsCrear = true,
                        VideogameDto = VideogameDto
                    };

                }
                else
                {
                    var encontrado = await _dbContext.Videogames.FirstAsync(e => e.IdVideoGame == IdVideoGame);
                    encontrado.Name = VideogameDto.Name;
                    encontrado.Description = VideogameDto.Description;
                    encontrado.Price= VideogameDto.Price;
                    encontrado.Company = VideogameDto.Company;
                    encontrado.Gender = VideogameDto.Gender;

                    await _dbContext.SaveChangesAsync();

                    mensaje = new VideoGMensaje()
                    {
                        EsCrear = false,
                        VideogameDto = VideogameDto
                    };

                }

                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    LoadingesVisible = false;
                    WeakReferenceMessenger.Default.Send(new VideoGMensajeria(mensaje));
                    await Shell.Current.Navigation.PopAsync();
                });

            });
        }

    }
}
