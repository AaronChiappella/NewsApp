using Microsoft.AspNetCore.Mvc.ApplicationModels;
using NewsAPI;
using NewsAPI.Constants;
using NewsAPI.Models;
using NewsApp.Articles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OpenIddict.Abstractions.OpenIddictConstants;
using Statuses = NewsAPI.Constants.Statuses;

namespace NewsApp.News
{
    public class NewsApiService : INewsService
    {
        public async Task<ICollection<ArticleDto>> GetNewsAsync(string query)
        {
            ICollection<ArticleDto> responseList = new List<ArticleDto>();

            try
            {// init with your API key
                NewsApiClient newsApiClient = new NewsApiClient("34223fc9494d461385d9098b1bcf960a");
                var articlesResponse = await newsApiClient.GetEverythingAsync(new EverythingRequest
                {
                    Q = query,
                    SortBy = SortBys.Popularity,
                    Language = Languages.EN,
                    // consultamos de un mes para atras ya que es lo que permite la api gratis
                    From = DateTime.Now.AddMonths(-1)
                });

                //TODO: se deberia lanzar una excepcion si la consulta a la api da error.
                if (articlesResponse.Status == Statuses.Ok)
                {
                    articlesResponse.Articles.ForEach(t => responseList.Add(new ArticleDto
                    {
                        Author = t.Author,
                        Title = t.Title,
                        Description = t.Description,
                        Url = t.Url,
                        PublishedAt = (DateTime)t.PublishedAt,
                        UrlToImage = t.UrlToImage,
                        Content = t.Content
                    }));
                    if (responseList.Count == 0)
                    {
                        // No se devolvieron noticias
                        Console.WriteLine("La API no devolvió noticias para la consulta: " + query);
                    }

                    return responseList;
                }
                else if (articlesResponse.Status == Statuses.Error)
                {
                    // La solicitud no es válida según la API, lanzar una excepción específica
                    throw new Exception("La solicitud a la API no es válida. Detalles: " + articlesResponse.Error.Message);
                }
                else
                {
                    // Otros casos de error, lanzar una excepción general
                    throw new Exception("La solicitud de la API no fue exitosa. Status: " + articlesResponse.Status);
                }
            }
            catch (Exception ex)
            {
                // Captura cualquier excepción no manejada
                Console.WriteLine("Error al obtener noticias: " + ex.Message);
                throw;
            }



        }

    }
}