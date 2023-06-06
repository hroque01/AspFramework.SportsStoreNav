using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.Models;
using WebApplication1.Models.Repository;

namespace SportsStore.Pages
{
    public partial class Listing : System.Web.UI.Page
    {
        //facciamo riferiemnto alla classe Repository che gestisce tutto il DbContext
        private Repository repo = new Repository();
        private int pageSize = 4;
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        // Metodo che restituisce una collezione di oggetti Product
        protected IEnumerable<Product> GetProducts()
        {
            // Restituisce la proprietà Products del repository
            // Presumibilmente, il repository contiene la logica per recuperare la lista dei prodotti
            return FilterProducts()

                // I prodotti vengono ordinati per ProductID, quindi viene eseguita una paginazione
                .OrderBy(p => p.ProductID)

                // selezionando solo i prodotti nella pagina corrente utilizzando Skip e Take
                .Skip((CurrentPage - 1) * pageSize)
                .Take(pageSize);
        }

        protected int CurrentPage 
        { get 
            { 
                int page = GetPageFromRequest();

                // Questo permette che se viene ricarcato una pagine inesistente, ritorna all'ultima pagina
                return page > MaxPage ? MaxPage : page;
            } 
        }

        protected int MaxPage
        {
            get     
            {
                int prodCount = FilterProducts().Count();
                return (int)Math.Ceiling((decimal)repo.Products.Count() / pageSize);
            }

        }

        // Metodo che filtra i prodotti in base alla categoria selezionata
        private IEnumerable<Product> FilterProducts()
        {
            IEnumerable<Product> products = repo.Products;

            // Ottiene la categoria corrente dai parametri dell'URL utilizzando RouteData.Values e Request.QueryString
            string currentCategory = (string)RouteData.Values["category"] ?? Request.QueryString["category"];

            // Filtra i prodotti in base alla categoria corrente
            return currentCategory == null ? products : products.Where(p => p.Category == currentCategory);
        }

        // Metodo che ottiene il numero di pagina dalla richiesta dell'URL
        private int GetPageFromRequest()
        {
            int page;

            // Ottiene il valore del parametro "page" dall'URL utilizzando prima RouteData.Values e poi Request.QueryString
            string reqValue = (string)RouteData.Values["page"] ?? Request.QueryString["page"];

            // Se il valore non è nullo e può essere convertito in un numero intero, restituisce il numero di pagina
            // Altrimenti, restituisce 1 come valore predefinito
            return reqValue != null && int.TryParse(reqValue, out page) ? page : 1;
        }
    }
}